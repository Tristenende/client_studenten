using System.ComponentModel.DataAnnotations;

namespace ShowcaseAPI.Models
{
    public class Mail
    {
        [Required]
        [StringLength(60)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(60)]
        public string LastName { get; set; }
        public string? PhoneNumber {  get; set; }
        [StringLength(80)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [StringLength(200)]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
    }
}
