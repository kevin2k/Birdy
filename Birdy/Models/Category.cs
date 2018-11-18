using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Birdy.Models
{
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name ="Rubrik")]
        public string Name { get; set; }

        [Required]
        [Display(Name="Rubrik-Gruppe")]
        public int CategoryGroupId { get; set; }

        [ForeignKey("CategoryGroupId")]
        public virtual CategoryGroup CategoryGroup { get; set; }
    }
}
