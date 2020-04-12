namespace tabuleiro
{
	class Peca
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
	}
}
