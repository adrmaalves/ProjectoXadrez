namespace tabuleiro
{
	abstract class Peca
	{
		public Posicao Posicao { get; set; }
		public Cor Cor { get; protected set; }
		public int qteMovimentos { get; set; }
		public Tabuleiro Tab { get; set; }

		public Peca(Cor cor, Tabuleiro tab)
		{
			Posicao = null;
			Cor = cor;
			Tab = tab;
			qteMovimentos = 0;
		}

		public void IncrementarMovimentos()
		{
			qteMovimentos++;
		}

		public void DecrementarMovimentos()
		{
			qteMovimentos--;
		}

		public bool MovimentoPossivel(Posicao pos)
		{
			return movimentosPossiveis()[pos.Linha, pos.Coluna];
		}

		public abstract bool[,] movimentosPossiveis();
		public bool existeMoviementosPossiveis()
		{
			bool[,] mat = movimentosPossiveis();
			for (int i = 0; i < Tab.Linhas; i++)
			{
				for (int j = 0; j < Tab.Colunas; j++)
				{
					if (mat[i, j])
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
