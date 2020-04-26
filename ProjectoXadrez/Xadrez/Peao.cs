using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
	class Peao : Peca
	{
		private PartidaDeXadrez partida;
		public Peao(Cor cor, Tabuleiro tab, PartidaDeXadrez partida) : base(cor, tab)
		{
			this.partida = partida;
		}

		private bool ExisteInimigo(Posicao pos)
		{
			Peca p = Tab.GetPeca(pos);
			return p != null && p.Cor != this.Cor;
		}

		private bool Livre(Posicao pos)
		{
			return Tab.GetPeca(pos) == null;
		}

		public override bool[,] movimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);

			if (Cor == Cor.Branca)
			{
				pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && Livre(pos))
					mat[pos.Linha, pos.Coluna] = true;

				pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && Livre(pos) && qteMovimentos == 0)
					mat[pos.Linha, pos.Coluna] = true;

				pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
				if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
					mat[pos.Linha, pos.Coluna] = true;

				pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
				if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
					mat[pos.Linha, pos.Coluna] = true;

				// #jogadaespecial En Passant
				if (Posicao.Linha == 3)
				{
					Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
					if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.GetPeca(esquerda) == partida.vulneravelEnPassant)
					{
						mat[esquerda.Linha - 1, esquerda.Coluna] = true;
					}

					Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
					if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.GetPeca(direita) == partida.vulneravelEnPassant)
					{
						mat[direita.Linha - 1, direita.Coluna] = true;
					}

				}
			}
			else
			{
				pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && Livre(pos))
					mat[pos.Linha, pos.Coluna] = true;

				pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
				if (Tab.PosicaoValida(pos) && Livre(pos) && qteMovimentos == 0)
					mat[pos.Linha, pos.Coluna] = true;

				pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
				if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
					mat[pos.Linha, pos.Coluna] = true;

				pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
				if (Tab.PosicaoValida(pos) && ExisteInimigo(pos))
					mat[pos.Linha, pos.Coluna] = true;

				if (Posicao.Linha == 4)
				{
					Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
					if (Tab.PosicaoValida(esquerda) && ExisteInimigo(esquerda) && Tab.GetPeca(esquerda) == partida.vulneravelEnPassant)
					{
						mat[esquerda.Linha + 1, esquerda.Coluna] = true;
					}

					Posicao direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
					if (Tab.PosicaoValida(direita) && ExisteInimigo(direita) && Tab.GetPeca(direita) == partida.vulneravelEnPassant)
					{
						mat[direita.Linha + 1, direita.Coluna] = true;
					}
				}
			}

			return mat;
		}

		public override string ToString()
		{
			return "P";
		}
	}
}
