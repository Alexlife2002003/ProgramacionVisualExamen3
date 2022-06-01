using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HarryPotter.Models
{
    public class Food
    {
        [Key]
        public int Id{get; set;}
        
        [Required]
        [Display(Name="Platillo")]
        public string Nombre{get; set;}



        [Display(Name ="Categoria")]
        public int? IdCategory{ get; set;}
        [ForeignKey("IdCategory")]
        public Category? Category { get; set;}

        [Required]
        [Display(Name="Descripcion")]
        public string Description{get; set;}

        [Required]
        [Display(Name ="Precio")]
        public int Precio{get; set;}
         [Display(Name ="Foto del platillo")]
        public string? UrlImage{get; set;}
        
    }
    
}