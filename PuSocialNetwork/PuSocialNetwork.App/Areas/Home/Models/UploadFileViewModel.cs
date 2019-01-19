namespace PuSocialNetwork.App.Areas.Home.Models
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class UploadFileViewModel
    {
        public int UserId { get; set; }

        //[MaxLength(1024, ErrorMessage = "The image must be maximum 1MB")]
        public IFormFile File { get; set; }
    }
}
