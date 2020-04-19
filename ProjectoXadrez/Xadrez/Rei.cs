using tabuleiro;
namespace xadrez
{
	class Rei : Peca
	{
		private PartidaDeXadrez partida;
		public Rei(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
		{
			this.partida = partida;
		}

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.GetPeca(pos);
			return p == null || p.Cor != this.Cor;
		}


		private bool TesteTorreParaRoque(Posicao pos)
		{
			Peca p = Tab.GetPeca(pos);
			return p != null && p is Torre && p.Cor == Cor && p.qteMovimentos == 0;
		}

		public override bool[,] movimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);
			//Acima
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;
			//NE
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;
			//Direira
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;
			//SE
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;
			//Abaixo
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;
			//SO
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;
			//Esquerda
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;
			//NO
			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;


			// #jogadaespecial roque
			if (qteMovimentos == 0 && !partida.xeque)
			{
				// #jogadaespecial roque pequeno

				Posicao posT1 = new Posicao(Posicao.Linha, Posicao.Coluna + 3);
				if (TesteTorreParaRoque(posT1))
				{
					Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
					Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna + 2);

					if (Tab.GetPeca(p1) == null && Tab.GetPeca(p2) == null)
					{
						mat[Posicao.Linha, Posicao.Coluna + 2] = true;
					}
				}

				// #jogadaespecial roque grande

				Posicao posT2 = new Posicao(Posicao.Linha, Posicao.Coluna - 4);
				if (TesteTorreParaRoque(posT2))
				{
					Posicao p1 = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
					Posicao p2 = new Posicao(Posicao.Linha, Posicao.Coluna - 2);
					Posicao p3 = new Posicao(Posicao.Linha, Posicao.Coluna - 3);


					if (Tab.GetPeca(p1) == null && Tab.GetPeca(p2) == null && Tab.GetPeca(p3) == null)
					{
						mat[Posicao.Linha, Posicao.Coluna - 2] = true;
					}
				}
			}



			return mat;
		}

		public override string ToString()
		{
			return "R";
		}
	}
}
