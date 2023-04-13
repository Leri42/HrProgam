using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace HrProgramWeb.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Range(10000000000, 99999999999)]
        [DisplayName("Personal Id")]
        public long PersonalIdentityNumber { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required]
        [DisplayName("Date Of Birth")]
        public DateTime BirthDate { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        public EmploymentStatus Status { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Release Date")]
        public DateTime? ReleaseDate { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "The PhoneNumber field is not a valid phone number")]
        [DisplayName("Phone Number")]
        public string MobileNumber { get; set; }

        public virtual ApplicationUser? ApplicationUser { get; set; }


        public string ApplicationUserId { get; set; }

    }
}
