using System.ComponentModel.DataAnnotations;

namespace contratosimples_api.Models.Domain
{
    public class CentroDeCusto
    {
        [Key]
        public int Cod { get; set; }
        public string Nome { get; set; }
        public string Obs { get; set; }

    }
}
