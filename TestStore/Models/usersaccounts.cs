using System.ComponentModel.DataAnnotations;

namespace TestStore.Models
{
    public class usersaccounts
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        [Required]
        [StringLength(50)]
        public string pass { get; set; }

        [Required]
        [StringLength(50)]
        public string role { get; set; }
    }
}
