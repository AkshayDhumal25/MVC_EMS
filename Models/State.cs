using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MVC_EMS.Models
{
    public class State
    {
        [Key]
        public int StateId { get; set; }

        [NotNull]
        public string StateName { get; set; }

        [ForeignKey("CountryId")]
        public int CountryId { get; set; }

        public Country Country { get; set; }

    }
}
