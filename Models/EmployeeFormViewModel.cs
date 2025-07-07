using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_EMS.Models
{
    public class EmployeeFormViewModel
    {
        public EmployeeMaster Employee { get; set; } = new EmployeeMaster();
        public List<SelectListItem> Countries { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> States { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Cities { get; set; } = new List<SelectListItem>();
    }
}