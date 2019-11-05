using Connect4.Models;
using System;
using Xunit;

namespace TesteTabuleiro
{
    public class UnitTestTabuleiro
    {
        [Fact]
        public void TestTudoOcupado()
        {

            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                { 0,0,0,0,0,0 },
                { 0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.False(t.isTudoOcupado());
            valor = new int[7, 6]{
                {1,2,1,1,2,1 },
                {1,2,1,1,2,1 },
                {2,1,2,2,1,2 },
                {2,1,2,2,1,2 },
                {1,2,1,1,2,1 },
                {1,2,1,1,2,1 },
                {2,1,2,2,1,2 },
                };
            t = new Tabuleiro(valor);
            Assert.True(t.isTudoOcupado());

        }

        [Fact]
        public void TestVencedorColuna()
        {
            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                { 0,0,0,0,0,0 },
                { 0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificarVencedorColuna());
            valor = new int[7, 6]{
                {1,2,1,1,2,1 },
                {1,2,1,1,2,1 },
                {2,1,2,2,1,2 },
                {2,1,2,2,1,2 },
                {1,2,1,1,2,1 },
                {1,2,1,1,1,1 },
                {2,1,2,2,1,2 },
                };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificarVencedorColuna());


            valor = new int[7, 6]{
                {1,2,1,1,2,1 },
                {1,2,1,1,2,1 },
                {2,1,2,2,1,2 },
                {2,1,2,2,1,2 },
                {1,2,1,1,2,1 },
                {1,2,1,2,1,1 },
                {2,2,2,2,1,2 },
                };
            t = new Tabuleiro(valor);
            Assert.Equal(2, t.VerificarVencedorColuna());

            valor = new int[7, 6]{
                {1,1,1,1,2,1 },
                {1,2,1,1,2,1 },
                {2,1,2,2,1,2 },
                {2,1,2,2,1,2 },
                {1,2,1,1,2,1 },
                {1,2,1,2,1,1 },
                {2,2,1,2,1,2 },
                };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificarVencedorColuna());
        }



        [Fact]
        public void TestVencedorLinha()
        {
            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                { 0,0,0,0,0,0 },
                { 0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificarVencedorLinha());
            valor = new int[7, 6]{
                {1,2,1,1,2,1 },
                {1,2,1,1,2,1 },
                {1,1,2,2,1,2 },
                {1,1,2,2,1,2 },
                {2,2,1,1,2,1 },
                {1,2,1,1,1,1 },
                {2,1,2,2,1,2 },
                };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificarVencedorLinha());
            valor = new int[7, 6]{
                {1,2,1,1,2,1 },
                {1,2,1,2,1,1 },
                {2,1,2,2,1,2 },
                {1,1,2,2,1,1 },
                {2,2,1,1,2,1 },
                {1,2,1,1,1,1 },
                {2,1,2,2,1,1 },
                };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificarVencedorLinha());
        }


        [Fact]
        public void TestVerificarVencedorDiagonal()
        {
            int[,] valor = new int[7, 6]{
                {0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                { 0,0,0,0,0,0 },
                { 0,0,0,0,0,0 },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                {0,0,0,0,0,0  },
                };
            Tabuleiro t = new Tabuleiro(valor);
            Assert.Equal(0, t.VerificarVencedorDiagonal());
            valor = new int[7, 6]{
                {1,2,1,1,2,1 },
                {1,2,1,1,2,1 },
                {2,1,2,2,1,2 },
                {2,1,2,2,1,2 },
                {1,2,1,1,2,1 },
                {1,2,1,1,1,1 },
                {2,1,2,2,1,2 },
                };
            t = new Tabuleiro(valor);
            Assert.Equal(1, t.VerificarVencedorDiagonal());
        }
    }
}
