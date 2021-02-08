using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vehicle.Service.Models
{
    public class VehicleModel
    {
        public int Id { get; set; }

        [Required]
        public int MakeId { get; set; }

        [Required]
        [StringLength(100)]
        [Index("VehicleModelNameIndex", IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        [ForeignKey("MakeId")]
        public VehicleMake VehicleMake { get; set; }
    }
}