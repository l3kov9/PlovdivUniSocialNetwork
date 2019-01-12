namespace PuSocialNetwork.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using static Common.DataConstants;

    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(EmailRegex)]
        public string Email { get; set; }

        [Required]
        [StringLength(IdLength, MinimumLength = IdLength)]
        public string FacultyNumber { get; set; }

        [Required]
        [StringLength(IdLength, MinimumLength = IdLength)]
        public string Egn { get; set; }

        public byte[] ProfileImage { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}
