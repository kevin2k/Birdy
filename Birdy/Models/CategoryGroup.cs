using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Birdy.Models
{
    public class CategoryGroup
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name="Rubrik-Gruppe Name")]
        public string Name { get; set; }

        [Display(Name="Beschreibung")]
        public string Description { get; set; }

        [Required]
        [Display(Name="Ordnungsnummer")]
        public int DisplayOrder { get; set; }
    }
}
