using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Birdy.Models
{
    public class SearchEntry
    {
        [Required]
        public int Id { get; set; }

        public string SearchText { get; set; }

        public DateTime SearchDate { get; set; }
    }
}
