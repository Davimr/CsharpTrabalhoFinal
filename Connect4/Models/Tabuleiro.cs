using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Tabuleiro
    {
        public static int NUMERO_COLUNAS = 7;
        public static int NUMERO_LINHAS = 6;
        private static int NUMERO_JOGADORES = 2;

        public int[,] RepresentacaoTabuleiro { get; set; }
        
        public int Turno { get; set; } = new Random().Next(1, 3);

        public void AlternaTurno()
        {
            this.Turno = (this.Turno % NUMERO_JOGADORES) + 1;
        }
        public Tabuleiro()
        {
            this.RepresentacaoTabuleiro = new int[NUMERO_COLUNAS, NUMERO_LINHAS];
        }

        public Tabuleiro(int [,] repTabuleiro)
        {
            RepresentacaoTabuleiro = repTabuleiro;
        }

        /// <summary>
        /// Verifica se existe um vencedor no jogo.
        /// </summary>
        /// <returns>Retorna 1 caso o jogador 1 vença,
        /// 2 caso o jogador 2 vença, -1 caso seja empate e 0
        /// caso o jogo ainda não tenha terminado.</returns>
        public int Vencedor()
        {
            int vencedor = 0;
            //Verifica se existe um vencedor em alguma Coluna.
            vencedor = VerificarVencedorColuna();
            //Caso exista retorne o vencedor. Caso contrário continue verificando.
            if (vencedor != 0)
                return vencedor;
            //Verificar se existe um vencedor em alguma linha.
            vencedor = VerificarVencedorLinha();
            if (vencedor != 0)
                return vencedor;
            vencedor = VerificarVencedorDiagonal();
            if (vencedor != 0)
                return vencedor;
            if (isTudoOcupado())
                return -1;
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
            if(Jogador!= this.Turno)
            {
                throw new ArgumentException($"Não é a vez do jogador {Jogador}");
            }
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
                    AlternaTurno();
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
