using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Birdy.Models.CategoryViewModels
{
    public class CategoryAndCategoryGroupViewModel
    {
        public Category Category { get; set; }

        // Liste mit allen verfügbaren Rubrik-Gruppen
        public IEnumerable<CategoryGroup> CategoryGroupList { get; set; }

        // Liste mit allen Rubriken
        public List<string> CategoryList { get; set; }

        [Display(Name ="Neue Rubrik")]
        public bool isNew { get; set; }

        // String StatussMessage wird verwendet, um Fehlermeldungen anzuzeigen
        public String StatusMessage { get; set; }
    }
}
