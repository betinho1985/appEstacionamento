using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace appEstacionamento.Models
{
    public class Movimentacoes
    {
        public int id { get; set; }
        [DisplayName("Placa")]
        public string placa { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data de Entrada")]
        public DateTime dataEntrada { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Hora de Entrada")]
        public TimeSpan horaEntrada { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DisplayName("Data de Saida")]
        public DateTime dataSaida { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Hora de Saida")]
        public TimeSpan horaSaida { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        [DisplayName("Duração")]
        public TimeSpan duracao { get; set; }

        [DisplayName("Tempo Cobrado")]
        public String tempoCobrado { get; set; }

        [DisplayName("Preço")]
        public Decimal preco { get; set; }

        [DisplayName("Valor a Pagar")]
        public Decimal valorPagar { get; set; }

        [DisplayName("Pago")]
        public Boolean status { get; set; }
    }
}