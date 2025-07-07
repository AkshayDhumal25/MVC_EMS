using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MVC_EMS.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }

        [NotNull]
        public string CityName { get; set; }

        [ForeignKey("StateId")]
        public int StateId { get; set; }
        public State State { get; set; }
    }
}
