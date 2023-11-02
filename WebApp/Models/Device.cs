using System.ComponentModel.DataAnnotations;

namespace WebApp.Models
{
    public class Device
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
