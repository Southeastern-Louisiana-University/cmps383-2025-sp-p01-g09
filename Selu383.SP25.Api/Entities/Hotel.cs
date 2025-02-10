using System.ComponentModel.DataAnnotations;

namespace Selu383.SP25.Api.Entities
{
    public class Hotel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
