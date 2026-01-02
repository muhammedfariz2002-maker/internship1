using System.ComponentModel.DataAnnotations;

namespace internship1.Models
{
    public class Student
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        //test
        public string Email { get; set; }
        [Required(ErrorMessage = "Course is required")]
        public string Course { get; set; }

    }
}
