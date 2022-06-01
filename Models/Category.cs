using System.ComponentModel.DataAnnotations;

namespace HarryPotter.Models
{
    public class Category
    {
        [Key]
        public int Id{get; set;}

        [Required]
        [Display(Name="Categoria")]
        public string Nombre{get; set;}
        
    }
    
}