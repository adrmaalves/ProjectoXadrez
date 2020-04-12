using System;
using tabuleiro;
using xadrez;

namespace ProjectoXadrez
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				PartidaDeXadrez partida = new PartidaDeXadrez();

				while (!partida.terminada)
				{
					Console.Clear();
					Tela.imprimirTabuleiro(partida.tab);

					Console.WriteLine();
					Console.Write("Origem: ");
					Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
					Console.Write("Destino: ");
					Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
					partida.executaMovimento(origem, destino);


				}
			}
			catch (TabuleiroException t)
			{
				Console.WriteLine(t.Message);
			}
			catch (Exception e)
			{
				Console.WriteLine("Unexpected Error: " + e.Message);
			}
			finally
			{
				Console.ReadKey(true);
			}
		}
	}
}
