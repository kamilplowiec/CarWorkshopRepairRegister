namespace CarWorkshopRepairRegister
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RejestrNaprawUslugi")]
    public partial class RejestrNaprawUslugi
    {
        public int Id { get; set; }

        public int RejestrNaprawId { get; set; }

        public int UslugaId { get; set; }
    }
}
