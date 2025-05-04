using System.ComponentModel.DataAnnotations;

namespace eBookLoginHW1.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required, StringLength(9, MinimumLength = 9)]
        public string NationalId { get; set; }

        [Required, StringLength(19)]
        public string CreditCardNumber { get; set; }

        [Required, StringLength(5)]
        public string CreditCardExpiry { get; set; }

        [Required, StringLength(3)]
        public string CreditCardCVC { get; set; }
    }
}
