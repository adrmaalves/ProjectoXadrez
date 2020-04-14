using System;
using System.Net.NetworkInformation;
using tabuleiro;
using xadrez;
using System.Collections.Generic;

namespace ProjectoXadrez
{
	class Tela
	{

		public static void ImprimirPartida(PartidaDeXadrez partida)
		{
			imprimirTabuleiro(partida.tab);
			Console.WriteLine();
			ImprimirPecasCapturadas(partida);
			Console.WriteLine();
			Console.WriteLine("Turno: " + partida.Turno);
			Console.WriteLine("Aguarda jogada: " + partida.jogadorAtual);
		}


		public static void ImprimirPecasCapturadas(PartidaDeXadrez partida)
		{
			Console.WriteLine("Peças Capturadas:");
			Console.Write("Brancas: ");
			ImprimirConjunto(partida.pecasCapturadas(Cor.Branca));
			Console.WriteLine();
			ConsoleColor color = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.Write("Pretas: ");
			ImprimirConjunto(partida.pecasCapturadas(Cor.Preta));
			Console.ForegroundColor = color;
			Console.WriteLine();
		}


		public static void ImprimirConjunto(HashSet<Peca> conjunto)
		{
			Console.Write("[ ");
			foreach (Peca x in conjunto)
			{
				Console.Write(x + " ");
			}
			Console.Write("]");
		}
		public static void imprimirTabuleiro(Tabuleiro tab)
		{
			Console.WriteLine();
			for (int i = 0; i < tab.Linhas; i++)
			{
				Console.Write(8 - i + " ");

				for (int j = 0; j < tab.Colunas; j++)
				{
					imprimirPeca(tab.GetPeca(i, j));
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
		}


		public static void imprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
		{
			ConsoleColor fundoOriginal = Console.BackgroundColor;
			ConsoleColor fundoAlterado = ConsoleColor.DarkGray;
			Console.WriteLine();
			for (int i = 0; i < tab.Linhas; i++)
			{
				Console.Write(8 - i + " ");

				for (int j = 0; j < tab.Colunas; j++)
				{
					if (posicoesPossiveis[i, j] == true)
						Console.BackgroundColor = fundoAlterado;
					else
						Console.BackgroundColor = fundoOriginal;
					imprimirPeca(tab.GetPeca(i, j));
					Console.BackgroundColor = fundoOriginal;
				}
				Console.WriteLine();
			}
			Console.WriteLine("  a b c d e f g h");
			Console.BackgroundColor = fundoOriginal;
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
			if (peca == null)
				Console.Write("- ");
			else
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
				Console.Write(" ");
			}
		}

	}
}
