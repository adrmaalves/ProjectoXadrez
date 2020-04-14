using System;
using tabuleiro;
using xadrez;
using System.Collections.Generic;

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

		public PartidaDeXadrez()
		{
			tab = new Tabuleiro(8, 8);
			Turno = 1;
			jogadorAtual = Cor.Branca;
			terminada = false;
			Pecas = new HashSet<Peca>();
			Capturadas = new HashSet<Peca>();
			colocarPecas();
		}

		public void executaMovimento(Posicao origem, Posicao destino)
		{
			Peca p = tab.RetirarPeca(origem);
			p.IncrementarMovimentos();
			Peca pecaCapturada = tab.RetirarPeca(destino);
			tab.ColocarPeca(p, destino);
			if (pecaCapturada != null)
				Capturadas.Add(pecaCapturada);
				
		}

		public void RealizaJogada(Posicao origem, Posicao destino)
		{
			executaMovimento(origem, destino);
			Turno++;
			mudaJogador();
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
			if (!tab.GetPeca(origem).PodeMoverPara(destino))
				throw new TabuleiroException("Posição de destino invalida!");
		}

		public HashSet<Peca> pecasCapturadas(Cor cor)
		{
			HashSet<Peca> aux = new HashSet<Peca>();

			foreach(Peca x in Capturadas)
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

		public void ColocarNovaPeca(char coluna,int linha,Peca peca)
		{
			tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
			Pecas.Add(peca);
		}

		private void colocarPecas()
		{
			//Brancas
			ColocarNovaPeca('c', 1, new Torre(Cor.Branca, tab));
			ColocarNovaPeca('c', 2, new Torre(Cor.Branca, tab));
			ColocarNovaPeca('d', 2, new Torre(Cor.Branca, tab));
			ColocarNovaPeca('e', 2, new Torre(Cor.Branca, tab));
			ColocarNovaPeca('e', 1, new Torre(Cor.Branca, tab));
			ColocarNovaPeca('d', 1, new Rei(Cor.Branca, tab));


			//Pretas
			ColocarNovaPeca('c', 7, new Torre(Cor.Preta, tab));
			ColocarNovaPeca('c', 8, new Torre(Cor.Preta, tab));
			ColocarNovaPeca('d', 7, new Torre(Cor.Preta, tab));
			ColocarNovaPeca('e', 7, new Torre(Cor.Preta, tab));
			ColocarNovaPeca('e', 8, new Torre(Cor.Preta, tab));
			ColocarNovaPeca('d', 8, new Rei(Cor.Preta, tab));

			
		}
	}
}
