using System;
using tabuleiro;


namespace ProjectoXadrez
{
	class Tela
	{
		public static void imprimirTabuleiro(Tabuleiro tab)
		{
			Console.WriteLine();
			for (int i = 0; i < tab.Linhas; i++)
			{
				Console.Write("| ");

				for (int j = 0; j < tab.Colunas; j++)
				{
					if (tab.GetPeca(i, j) == null)
						Console.Write("- ");
					else
						Console.Write(tab.GetPeca(i, j) + " ");
				}
				Console.WriteLine("|");

			}
		}

	}
}
