using System;
using tabuleiro;

namespace ProjectoXadrez
{
	class Program
	{
		static void Main(string[] args)
		{

			Posicao p;
			p = new Posicao(3, 4);

			Console.WriteLine(p);


			Tabuleiro tab = new Tabuleiro(8, 8);



			Console.ReadKey(true);
		}
	}
}
