using System.ComponentModel.DataAnnotations;

namespace SaudiWorldCupHub.Models
{
    public class Cities
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Code { get; set; }
    }
}