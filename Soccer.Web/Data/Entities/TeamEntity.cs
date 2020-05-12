using System.ComponentModel.DataAnnotations;

namespace Soccer.Web.Data.Entities
{
    public class TeamEntity
    {//  prop dar  depsues tab dos veces
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "The  field {0} can not have more than {1} characters.")]
        [Required(ErrorMessage = "The field {0} is mandatory.")]
        // cuando se aga  calse con ssus pareametros nos metoemes
        // en tolol  y de spues en nuget  de spues packet console
        // creamos una  clase (el omonre cualqueira) en data
       
        public string Name { get; set; }
        [Display(Name = "Logo")]
        public string LogoPath { get; set; }
    }
}
