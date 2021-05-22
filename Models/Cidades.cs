using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{

    public class Cidades
    {

        public Cidades()
        {
            this.Fronteiras = new HashSet<Fronteiras>();
        }

        [Key(), Column(Order = 0)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Populacao { get; set; }

        public virtual ICollection<Fronteiras> Fronteiras { get; set; }

    }

}