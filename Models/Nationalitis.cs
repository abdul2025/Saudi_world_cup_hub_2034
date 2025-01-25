using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SaudiWorldCupHub.Models
{
    public class Nationalitis
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
    }
}

