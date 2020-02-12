namespace CarWorkshopRepairRegister
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pojazd")]
    public partial class Pojazd
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Marka { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        public int RokProd { get; set; }

        [Required]
        [StringLength(10)]
        public string NrRej { get; set; }

        [Required]
        [StringLength(30)]
        public string VIN { get; set; }

        public int WlascicielId { get; set; }
    }
}
