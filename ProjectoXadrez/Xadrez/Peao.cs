﻿using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
	class Peao : Peca
	{
		public Peao(Cor cor, Tabuleiro tab) : base(cor, tab)
		{
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
			}

			return mat;
		}

		public override string ToString()
		{
			return "P";
		}
	}
}