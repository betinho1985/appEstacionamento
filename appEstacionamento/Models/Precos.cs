using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appEstacionamento.Models
{
    public class Precos
    {
        public int id { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data Inicial")]
        public DateTime dataIncial { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data Final")]
        public DateTime dataFinal { get; set; }

        [DisplayName("Valor da Hora")]
        public decimal valorHora { get; set; }

        [DisplayName("Valor da Hora Adicional")]
        public decimal valorHoraAdicional { get; set; }
    }
}