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
					try
					{
						Console.Clear();
						Tela.ImprimirPartida(partida);

						Console.WriteLine();
						Console.Write("Origem: ");
						Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
						partida.ValidarPosicaoOrigem(origem);

						bool[,] posicoesPossiveis = partida.tab.GetPeca(origem).movimentosPossiveis();

						Console.Clear();
						Tela.imprimirTabuleiro(partida.tab, posicoesPossiveis);

						Console.WriteLine();
						Console.Write("Destino: ");
						Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
						partida.ValidarPosicaoDestino(origem, destino);
						string op = Tela.AskPromocao(origem, destino, partida).ToString().ToUpper();
						partida.RealizaJogada(origem, destino,op);
					}
					catch (TabuleiroException t)
					{
						ConsoleColor color = Console.ForegroundColor;
						Console.ForegroundColor = ConsoleColor.Red;
						Console.WriteLine(t.Message);
						Console.ForegroundColor = color;
						Console.ReadKey(true);
					}
				}
				Console.Clear();
				Tela.ImprimirPartida(partida);

			}
			catch (TabuleiroException t)
			{
				ConsoleColor color = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(t.Message);
			}
			catch (Exception e)
			{
				ConsoleColor color = Console.ForegroundColor;
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Unexpected Error: " + e.Message);
				Console.ForegroundColor = color;
			}
			finally
			{
				Console.ReadKey(true);
			}
		}
	}
}
