namespace tabuleiro
{
	class Peca
	{
		public Posicao Posicao { get; set; }
		public Cor Cor { get; protected set; }
		public int qteMovimentos { get; set; }
		public Tabuleiro Tab { get; set; }

		public Peca(Posicao posicao, Cor cor, Tabuleiro tab)
		{
			Posicao = posicao;
			Cor = cor;
			Tab = tab;
			qteMovimentos = 0;
		}
	}
}
