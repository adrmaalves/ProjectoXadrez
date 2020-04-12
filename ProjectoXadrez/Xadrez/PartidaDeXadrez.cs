using System;
using tabuleiro;
using xadrez;

namespace xadrez
{
	class PartidaDeXadrez
	{
		public Tabuleiro tab { get; private set; }
		public int Turno { get; private set; }
		public Cor jogadorAtual { get; private set; }
		public bool terminada { get; private set; }

		public PartidaDeXadrez()
		{
			tab = new Tabuleiro(8, 8);
			Turno = 1;
			jogadorAtual = Cor.Branca;
			terminada = false;
			colocarPecas();
		}

		public void executaMovimento(Posicao origem, Posicao destino)
		{
			Peca p = tab.RetirarPeca(origem);
			p.IncrementarMovimentos();
			Peca pecaCapturada = tab.RetirarPeca(destino);
			tab.ColocarPeca(p, destino);
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

		private void colocarPecas()
		{
			tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 1).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('c', 2).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('d', 2).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 2).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Branca, tab), new PosicaoXadrez('e', 1).ToPosicao());
			tab.ColocarPeca(new Rei(Cor.Branca, tab), new PosicaoXadrez('d', 1).ToPosicao());


			tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 7).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('c', 8).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('d', 7).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 7).ToPosicao());
			tab.ColocarPeca(new Torre(Cor.Preta, tab), new PosicaoXadrez('e', 8).ToPosicao());
			tab.ColocarPeca(new Rei(Cor.Preta, tab), new PosicaoXadrez('d', 8).ToPosicao());
		}
	}
}
