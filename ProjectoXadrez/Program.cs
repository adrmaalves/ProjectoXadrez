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

				Tela.imprimirTabuleiro(partida.tab);
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
