using tabuleiro;
namespace xadrez
{
	class Rei : Peca
	{
		public Rei(Cor cor, Tabuleiro tab) : base(cor, tab)
		{
		}

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.GetPeca(pos);
			return p == null || p.Cor != this.Cor;
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


			return mat;
		}

		public override string ToString()
		{
			return "R";
		}
	}
}
