using System.ComponentModel.DataAnnotations;

namespace TestStore.Models
{
    public class customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        public string Job { get; set; }
        public string Gender { get; set; }


        public bool Married { get; set; } // True for married, false for single

        [StringLength(250)]
        public string Location { get; set; }
    }
}
