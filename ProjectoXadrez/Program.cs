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
				Tabuleiro tab = new Tabuleiro(8, 8);


				tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(0, 0));
				tab.ColocarPeca(new Torre(Cor.Preta, tab), new Posicao(1, 9));
				tab.ColocarPeca(new Rei(Cor.Preta, tab), new Posicao(2, 4));
				tab.ColocarPeca(new Rei(Cor.Preta, tab), new Posicao(0, 2));

				Tela.imprimirTabuleiro(tab);
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
