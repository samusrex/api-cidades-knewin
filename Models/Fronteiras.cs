using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{

    public class Fronteiras
    {

        [Column(Order = 0)]
        public int CidadesId1 { get; set; }

        [Column(Order = 1)]
        public int CidadesId2 { get; set; }

        [ForeignKey("CidadesId1")]
        public virtual Cidades Cidade1 { get; set; }

        [ForeignKey("CidadesId2")]
        public virtual Cidades Cidade2 { get; set; }

    }

}