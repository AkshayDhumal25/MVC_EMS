using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_EMS.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }

        //[NotNull]
        public string CountryName { get; set; }

        public List<SelectListItem> countries { get; set; }
    }
}
