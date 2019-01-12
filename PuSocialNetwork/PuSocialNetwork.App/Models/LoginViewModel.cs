using System.ComponentModel.DataAnnotations;

namespace PuSocialNetwork.App.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Факултетния номер трябва да е с дължина от 10 символа")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Факултетния номер трябва да е с дължина от 10 символа")]
        public string FacultyNumber { get; set; }

        [Required(ErrorMessage = "ЕГН-то трябва да е с дължина от 10 символа")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "ЕГН-то трябва да е с дължина от 10 символа")]
        public string Egn { get; set; }
    }
}
