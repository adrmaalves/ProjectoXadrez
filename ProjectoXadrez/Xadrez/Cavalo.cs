using System;
using System.Collections.Generic;
using System.Text;
using tabuleiro;

namespace xadrez
{
	class Cavalo : Peca
	{


		public Cavalo(Cor cor, Tabuleiro tab) : base(cor, tab)
		{
		}

		private bool PodeMover(Posicao pos)
		{
			Peca p = Tab.GetPeca(pos);
			return p == null || p.Cor != this.Cor;
		}

		public override bool[,] movimentosPossiveis()
		{
			bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

			Posicao pos = new Posicao(0, 0);

			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;

			pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;

			pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;

			pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;

			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;

			pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;

			pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;

			pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
			if (Tab.PosicaoValida(pos) && PodeMover(pos))
				mat[pos.Linha, pos.Coluna] = true;



			return mat;
		}

		public override string ToString()
		{
			return "C";
		}
	}
}
