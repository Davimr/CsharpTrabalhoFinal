using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Connect4.Models
{
    public class Torneio
    {
        public int Id { get; set; }

        [Display(Name ="Nome")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$")]
        public String NomeTorneio { get; set; }

        [Display(Name = "Quantidade de Jogadores")]
        [Range(2,32,
            ErrorMessage ="Quantidade de Jogadores do torneio deve ser entre 2 e 32 jogadores")]
        public int QuantidadeJogadores { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Início do Torneio")]
        [Range(typeof(DateTime),@"01/01/2001",@"01/01/2051",
            ErrorMessage = "A data de início do torneio deve ser entre {1} e {2}.")]
        public DateTime Inicio { get; set; }

        [Display(Name = "Criador do Torneio")]
        public String Dono { get; set; }

        [Display(Name = "Premiação")]
        [DataType(DataType.Currency)]
        public Decimal Premiacao { get; set; }
    }
}
