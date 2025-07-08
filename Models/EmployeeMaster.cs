using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_EMS.Models
{
    public class EmployeeMaster
    {
        [Key]
        public int Emp_Id { get; set; }

        [Required(ErrorMessage = "Employee code is required")]
        public int EmployeeCode { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        [Required(ErrorMessage = "State is required")]
        public int StateId { get; set; }
        public State? State { get; set; }

        [Required(ErrorMessage = "City is required")]
        public int CityId { get; set; }
        public City? City { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Mobile number is required")]
        public string MobileNumber { get; set; }

        [Required(ErrorMessage = "PAN number is required")]
        public string PanNumber { get; set; }

        [Required(ErrorMessage = "Passport number is required")]
        public string PassportNumber { get; set; }

        public string? ProfileImage { get; set; }

        public int? Gender { get; set; }

        [Required(ErrorMessage = "IsActive is required")]
        public bool IsActive { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfJoinee { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
