using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace upload_netcore_mvc2.Models
{
    [Table("Arquivos")]
    public class Arquivos
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string PontoAcesso { get; set; }
        public string TipoConteudo { get; set; }
        public DateTime DataExpiracao { get; set; }


    }
}
