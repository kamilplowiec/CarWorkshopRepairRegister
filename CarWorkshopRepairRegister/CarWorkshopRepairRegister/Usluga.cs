namespace CarWorkshopRepairRegister
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Usluga")]
    public partial class Usluga
    {
        public int Id { get; set; }

        [Required]
        public string Nazwa { get; set; }

        public decimal Cena { get; set; }
    }
}
