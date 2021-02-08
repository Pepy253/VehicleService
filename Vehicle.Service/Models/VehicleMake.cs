using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Vehicle.Service.Models
{
    public class VehicleMake
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Index("VehicleMakeNameIndex", IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public string Abrv { get; set; }

        public List<VehicleModel> vehicleModels { get; set; }
    }
}
