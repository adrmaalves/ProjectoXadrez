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
						Tela.imprimirTabuleiro(partida.tab);
						Console.WriteLine();
						Console.WriteLine("Turno: " + partida.Turno);
						Console.WriteLine("Aguarda jogada: " + partida.jogadorAtual);

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

						partida.RealizaJogada(origem, destino);
					}
					catch (TabuleiroException t)
					{
						Console.WriteLine(t.Message);
					}
					finally
					{
						Console.ReadKey(true);
					}
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
