using System.Linq;

namespace api.Models
{
    public class CidadesResponse
    {

        public CidadesResponse(Cidades cidade)
        {
            this.Nome = cidade.Name;
            this.Fronteiras = cidade.Fronteiras.Select(f => f.Cidade2.Name).ToArray();
        }

        public string Nome { get; set; }

        public string[] Fronteiras { get; set; }

    }

}