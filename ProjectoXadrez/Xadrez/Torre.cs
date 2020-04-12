using tabuleiro;

namespace xadrez
{
	class Torre : Peca
	{
		public Torre(Cor cor, Tabuleiro tab) : base(cor, tab)
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
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.GetPeca(pos) != null && Tab.GetPeca(pos).Cor != Cor)
					break;
				pos.Linha = pos.Linha - 1;
			}

			//Abaixo
			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.GetPeca(pos) != null && Tab.GetPeca(pos).Cor != Cor)
					break;
				pos.Linha = pos.Linha + 1;
			}

			//Esquerda
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.GetPeca(pos) != null && Tab.GetPeca(pos).Cor != Cor)
					break;
				pos.Coluna = pos.Coluna - 1;
			}

			//Direita
			pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
			while (Tab.PosicaoValida(pos) && PodeMover(pos))
			{
				mat[pos.Linha, pos.Coluna] = true;
				if (Tab.GetPeca(pos) != null && Tab.GetPeca(pos).Cor != Cor)
					break;
				pos.Coluna = pos.Coluna + 1;
			}

			return mat;
		}

		public override string ToString()
		{
			return "T";
		}
	}
}
