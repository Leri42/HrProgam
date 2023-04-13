using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HrProgramWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime birthdate { get; set; }
        public Gender Gender { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}
