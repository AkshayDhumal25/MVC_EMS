using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_EMS.Models
{
    public class EmployeeMaster
    {
        [Key]
        public int Emp_Id { get; set; }

        [Required]
        public int EmployeeCode { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        [Required]
        public int StateId { get; set; }
        public State? State { get; set; }

        [Required]
        public int CityId { get; set; }
        public City? City { get; set; }

        [Required, EmailAddress]
        public string EmailAddress { get; set; }

        [Required]
        public string MobileNumber { get; set; }

        [Required]
        public string PanNumber { get; set; }

        [Required]
        public string PassportNumber { get; set; }

        public string? ProfileImage { get; set; }

        public int? Gender { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public DateOnly? DateOfBirth { get; set; }

        public DateOnly? DateOfJoinee { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }
    }
}
