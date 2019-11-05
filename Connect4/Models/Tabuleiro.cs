using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Tabuleiro
    {
        private static int NUMERO_COLUNAS = 7;
        private static int NUMERO_LINHAS = 6;
        public int[,] RepresentacaoTabuleiro { get; set; }

        public Tabuleiro()
        {

        }

        public Tabuleiro(int [,] repTabuleiro)
        {
            RepresentacaoTabuleiro = repTabuleiro;
        }

        public int Vencedor()
        {
            return 0;
            

        }

        public int VerificarVencedorColuna()
        {
            for (int coluna = 0; coluna < RepresentacaoTabuleiro.GetLength(0); coluna++)
            {
                int contador=1;
                for (int linha = 1; linha < 
                    RepresentacaoTabuleiro.GetLength(1); 
                    linha++) { 
                    if (RepresentacaoTabuleiro[coluna, linha-1] == 0) { break;}
                    if (RepresentacaoTabuleiro[coluna,linha] 
                        == RepresentacaoTabuleiro[coluna, linha-1])
                    {
                        if (++contador == 4){
                            return RepresentacaoTabuleiro[coluna,linha];
                        }
                    }else{
                        contador = 1;                    
                    }                    
                }
            }
            return 0;
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public Boolean isTudoOcupado()
        {
            for(int i=0; i<RepresentacaoTabuleiro.GetLength(0);i++) {
                if(RepresentacaoTabuleiro[i,
                    RepresentacaoTabuleiro.GetLength(1)-1] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public void Jogar(int Jogador, int Posicao)
        {
            if(Posicao < 0)
            {
                throw new ArgumentException("A posição não pode " +
                    "ser menor que 0.");
            }else if(Posicao > NUMERO_COLUNAS - 1)
            {
                throw new ArgumentException
                    ("A posição não pode ser " +
                    $"maior que {NUMERO_COLUNAS}.");
            }
            int linha = 0;
            do
            {
                if (RepresentacaoTabuleiro[Posicao, linha] == 0)
                {
                    RepresentacaoTabuleiro[Posicao, linha] 
                        = Jogador;
                    return;
                }
            } while (++linha < RepresentacaoTabuleiro.GetLength(1));
            throw new ArgumentException(
                $"A coluna {Posicao} está lotada.");
            
        }

        public int VerificarVencedorLinha()
        {
            return 0;
        }

        public int VerificarVencedorDiagonal()
        {
            return 0;
        }
    }
}
