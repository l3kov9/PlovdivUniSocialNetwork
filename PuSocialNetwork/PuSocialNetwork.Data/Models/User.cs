namespace PuSocialNetwork.Data.Models
{
    using System;
    using System.Collections.Generic;
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

        public DateTime BirthDate { get; set; }

        public string BirthPlace { get; set; }

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

        public List<Post> Posts { get; set; } = new List<Post>();

        public List<Article> Articles { get; set; } = new List<Article>();

        public List<Like> Likes { get; set; } = new List<Like>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
