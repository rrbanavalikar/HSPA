using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs
{
    public class CityDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a mandatory field")]
        [StringLength(50, MinimumLength = 2)]
        [RegularExpression(".*[a-zA-Z]+.*", ErrorMessage = "Numbers are not allowed in Name")]
        public string Name { get; set; }

        [Required]
        public string Country  { get; set; }
    }
}
