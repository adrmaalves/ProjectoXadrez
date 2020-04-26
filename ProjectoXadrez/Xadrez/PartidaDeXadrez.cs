using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace xadrez
{
	class PartidaDeXadrez
	{
		public Tabuleiro tab { get; private set; }
		public int Turno { get; private set; }
		public Cor jogadorAtual { get; private set; }
		public bool terminada { get; private set; }
		private HashSet<Peca> Pecas;
		private HashSet<Peca> Capturadas;
		public Peca vulneravelEnPassant { get; private set; } //#jogadaespecial En Passant

		public bool xeque { get; private set; }

		public PartidaDeXadrez()
		{
			tab = new Tabuleiro(8, 8);
			Turno = 1;
			jogadorAtual = Cor.Branca;
			terminada = false;
			Pecas = new HashSet<Peca>();
			Capturadas = new HashSet<Peca>();
			xeque = false;
			vulneravelEnPassant = null;
			colocarPecas();
		}

		public Peca executaMovimento(Posicao origem, Posicao destino)
		{
			Peca p = tab.RetirarPeca(origem);
			p.IncrementarMovimentos();
			Peca pecaCapturada = tab.RetirarPeca(destino);
			tab.ColocarPeca(p, destino);
			if (pecaCapturada != null)
				Capturadas.Add(pecaCapturada);

			// #jogadaespecial roque pequeno
			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
				Peca T = tab.RetirarPeca(origemT);
				T.IncrementarMovimentos();
				tab.ColocarPeca(T, destinoT);
			}

			// #jogadaespecial roque grande
			if (p is Rei && destino.Coluna == origem.Coluna - 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
				Peca T = tab.RetirarPeca(origemT);
				T.IncrementarMovimentos();
				tab.ColocarPeca(T, destinoT);
			}

			// #jogadaespecial En Passant
			if (p is Peao)
			{
				if (origem.Coluna != destino.Coluna && pecaCapturada is null)
				{
					Posicao posP;
					if (p.Cor == Cor.Branca)
					{
						posP = new Posicao(destino.Linha + 1, destino.Coluna);
					}
					else
					{
						posP = new Posicao(destino.Linha - 1, destino.Coluna);
					}

					pecaCapturada = tab.RetirarPeca(posP);
					Capturadas.Add(pecaCapturada);
				}
			}

			return pecaCapturada;

		}

		public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
		{
			Peca p = tab.RetirarPeca(destino);
			p.DecrementarMovimentos();
			if (pecaCapturada != null)
			{
				tab.ColocarPeca(pecaCapturada, destino);
				Capturadas.Remove(pecaCapturada);
			}
			tab.ColocarPeca(p, origem);

			// #jogadaespecial roque pequeno
			if (p is Rei && destino.Coluna == origem.Coluna + 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
				Peca T = tab.RetirarPeca(destinoT);
				T.DecrementarMovimentos();
				tab.ColocarPeca(T, origemT);
			}

			// #jogadaespecial roque grande
			if (p is Rei && destino.Coluna == origem.Coluna - 2)
			{
				Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
				Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
				Peca T = tab.RetirarPeca(destinoT);
				T.DecrementarMovimentos();
				tab.ColocarPeca(T, origemT);
			}

			// #jogadaespecial En Passant
			if (p is Peao)
			{
				if (origem.Coluna != destino.Coluna && pecaCapturada == vulneravelEnPassant)
				{
					Peca peao = tab.RetirarPeca(destino);
					Posicao posP;
					if (p.Cor == Cor.Branca)
					{
						posP = new Posicao(3, destino.Coluna);
					}
					else
					{
						posP = new Posicao(4, destino.Coluna);
					}
					tab.ColocarPeca(peao, posP);
				}
			}
		}

		public void RealizaJogada(Posicao origem, Posicao destino, string opPromocao)
		{
			Peca pecaCapturada = executaMovimento(origem, destino);
			if (EstaEmXeque(jogadorAtual))
			{
				DesfazMovimento(origem, destino, pecaCapturada);
				throw new TabuleiroException("Não podes colocar-te em xeque!");
			}

			Peca p = tab.GetPeca(destino);

			// #jogadaespecial Promocao
			if (p is Peao)
			{
				if ((p.Cor == Cor.Branca && destino.Linha == 0) || (p.Cor == Cor.Preta && destino.Linha == 7))
				{
					p = tab.RetirarPeca(destino);
					Pecas.Remove(p);
					if (opPromocao != null && opPromocao != "N")
					{
						switch (opPromocao)
						{
							case "D":
								{
									Peca dama = new Dama(p.Cor, tab);
									tab.ColocarPeca(dama, destino);
									Pecas.Add(dama);
									break;
								}
							case "B":
								{
									Peca bispo = new Bispo(p.Cor, tab);
									tab.ColocarPeca(bispo, destino);
									Pecas.Add(bispo);
									break;
								}
							case "C":
								{
									Peca cavalo = new Cavalo(p.Cor, tab);
									tab.ColocarPeca(cavalo, destino);
									Pecas.Add(cavalo);
									break;
								}
							case "T":
								{
									Peca torre = new Torre(p.Cor, tab);
									tab.ColocarPeca(torre, destino);
									Pecas.Add(torre);
									break;
								}
							default:
								{
									throw new TabuleiroException("Peça errada para Promocao!");
								}
						}
					}
				}
			}

			if (EstaEmXeque(Adversaria(jogadorAtual)))
				xeque = true;
			else
				xeque = false;

			if (TesteXequeMate(Adversaria(jogadorAtual)))
				terminada = true;
			else
			{
				Turno++;
				mudaJogador();
			}


			//#jogadaespecial En Passant
			if (p is Peao && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
			{
				vulneravelEnPassant = p;
			}
			else
			{
				vulneravelEnPassant = null;
			}

		}

		private void mudaJogador()
		{
			if (jogadorAtual == Cor.Branca)
				jogadorAtual = Cor.Preta;
			else
				jogadorAtual = Cor.Branca;
		}

		public void ValidarPosicaoOrigem(Posicao pos)
		{
			if (tab.GetPeca(pos) == null)
				throw new TabuleiroException("Não existe peça na posição de origem escolhida!");

			if (jogadorAtual != tab.GetPeca(pos).Cor)
				throw new TabuleiroException("A peça de origem escolhida não é a sua!");

			if (!tab.GetPeca(pos).existeMoviementosPossiveis())
				throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");
		}

		public void ValidarPosicaoDestino(Posicao origem, Posicao destino)
		{
			if (!tab.GetPeca(origem).MovimentoPossivel(destino))
				throw new TabuleiroException("Posição de destino invalida!");
		}

		public HashSet<Peca> pecasCapturadas(Cor cor)
		{
			HashSet<Peca> aux = new HashSet<Peca>();

			foreach (Peca x in Capturadas)
			{
				if (x.Cor == cor)
				{
					aux.Add(x);
				}
			}

			return aux;
		}

		public HashSet<Peca> PecasEmJogo(Cor cor)
		{
			HashSet<Peca> aux = new HashSet<Peca>();

			foreach (Peca x in Pecas)
			{
				if (x.Cor == cor)
				{
					aux.Add(x);
				}
			}

			aux.ExceptWith(pecasCapturadas(cor));

			return aux;
		}


		private Cor Adversaria(Cor cor)
		{
			if (cor == Cor.Branca)
				return Cor.Preta;
			else
				return Cor.Branca;
		}

		private Peca Rei(Cor cor)
		{
			foreach (Peca x in PecasEmJogo(cor))
			{
				if (x is Rei)
				{
					return x;
				}
			}

			return null;
		}

		public bool EstaEmXeque(Cor cor)
		{
			Peca R = Rei(cor);

			if (R == null)
				throw new TabuleiroException("Não tem rei da cor " + cor + " no tabuleiro!");

			foreach (Peca x in PecasEmJogo(Adversaria(cor)))
			{
				bool[,] mat = x.movimentosPossiveis();
				if (mat[R.Posicao.Linha, R.Posicao.Coluna])
					return true;
			}

			return false;
		}

		public bool TesteXequeMate(Cor cor)
		{
			if (!EstaEmXeque(cor))
				return false;

			foreach (Peca x in PecasEmJogo(cor))
			{
				bool[,] mat = x.movimentosPossiveis();
				for (int i = 0; i < tab.Linhas; i++)
				{
					for (int j = 0; j < tab.Colunas; j++)
					{
						if (mat[i, j])
						{
							Posicao origem = x.Posicao;
							Posicao destino = new Posicao(i, j);
							Peca pecaCapturada = executaMovimento(origem, destino);
							bool testeXeque = EstaEmXeque(cor);
							DesfazMovimento(origem, destino, pecaCapturada);
							if (!testeXeque)
								return false;
						}
					}
				}
			}

			return true;
		}

		public void ColocarNovaPeca(char coluna, int linha, Peca peca)
		{
			tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
			Pecas.Add(peca);
		}

		private void colocarPecas()
		{
			//Brancas
			ColocarNovaPeca('a', 1, new Torre(Cor.Branca, tab));
			ColocarNovaPeca('b', 1, new Cavalo(Cor.Branca, tab));
			ColocarNovaPeca('c', 1, new Bispo(Cor.Branca, tab));
			ColocarNovaPeca('d', 1, new Dama(Cor.Branca, tab));
			ColocarNovaPeca('e', 1, new Rei(Cor.Branca, tab, this));
			ColocarNovaPeca('f', 1, new Bispo(Cor.Branca, tab));
			ColocarNovaPeca('g', 1, new Cavalo(Cor.Branca, tab));
			ColocarNovaPeca('h', 1, new Torre(Cor.Branca, tab));
			ColocarNovaPeca('a', 2, new Peao(Cor.Branca, tab, this));
			ColocarNovaPeca('b', 2, new Peao(Cor.Branca, tab, this));
			ColocarNovaPeca('c', 2, new Peao(Cor.Branca, tab, this));
			ColocarNovaPeca('d', 2, new Peao(Cor.Branca, tab, this));
			ColocarNovaPeca('e', 2, new Peao(Cor.Branca, tab, this));
			ColocarNovaPeca('f', 2, new Peao(Cor.Branca, tab, this));
			ColocarNovaPeca('g', 2, new Peao(Cor.Branca, tab, this));
			ColocarNovaPeca('h', 2, new Peao(Cor.Branca, tab, this));

			//Pretas
			ColocarNovaPeca('a', 8, new Torre(Cor.Preta, tab));
			ColocarNovaPeca('b', 8, new Cavalo(Cor.Preta, tab));
			ColocarNovaPeca('c', 8, new Bispo(Cor.Preta, tab));
			ColocarNovaPeca('d', 8, new Dama(Cor.Preta, tab));
			ColocarNovaPeca('e', 8, new Rei(Cor.Preta, tab, this));
			ColocarNovaPeca('f', 8, new Bispo(Cor.Preta, tab));
			ColocarNovaPeca('g', 8, new Cavalo(Cor.Preta, tab));
			ColocarNovaPeca('h', 8, new Torre(Cor.Preta, tab));
			ColocarNovaPeca('a', 7, new Peao(Cor.Preta, tab, this));
			ColocarNovaPeca('b', 7, new Peao(Cor.Preta, tab, this));
			ColocarNovaPeca('c', 7, new Peao(Cor.Preta, tab, this));
			ColocarNovaPeca('d', 7, new Peao(Cor.Preta, tab, this));
			ColocarNovaPeca('e', 7, new Peao(Cor.Preta, tab, this));
			ColocarNovaPeca('f', 7, new Peao(Cor.Preta, tab, this));
			ColocarNovaPeca('g', 7, new Peao(Cor.Preta, tab, this));
			ColocarNovaPeca('h', 7, new Peao(Cor.Preta, tab, this));


		}
	}
}
