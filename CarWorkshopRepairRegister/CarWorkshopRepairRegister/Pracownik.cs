namespace CarWorkshopRepairRegister
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Pracownik")]
    public partial class Pracownik
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nazwa { get; set; }

        [Required]
        [StringLength(200)]
        public string Login { get; set; }

        [Required]
        [StringLength(200)]
        public string Haslo { get; set; }
    }
}
