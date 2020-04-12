using System;
using tabuleiro;
using xadrez;

namespace ProjectoXadrez
{
	class Tela
	{
		public static void imprimirTabuleiro(Tabuleiro tab)
		{
			Console.WriteLine();
			for (int i = 0; i < tab.Linhas; i++)
			{
				Console.Write(8 - i + " ");

				for (int j = 0; j < tab.Colunas; j++)
				{
					if (tab.GetPeca(i, j) == null)
						Console.Write("- ");
					else
					{
						imprimirPeca(tab.GetPeca(i, j));
						Console.Write(" ");
					}
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
		}

		public static PosicaoXadrez LerPosicaoXadrez()
		{
			string s = Console.ReadLine();
			char coluna = s[0];
			int linha = int.Parse(s[1] + "");

			return new PosicaoXadrez(coluna, linha);
		}

		public static void imprimirPeca(Peca peca)
		{
			if (peca.Cor == Cor.Branca)
			{
				Console.Write(peca);
			}
			else
			{
				ConsoleColor aux = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.Write(peca);
				Console.ForegroundColor = aux;
			}
		}

	}
}
