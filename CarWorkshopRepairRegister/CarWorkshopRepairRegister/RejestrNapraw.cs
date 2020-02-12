namespace CarWorkshopRepairRegister
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("RejestrNapraw")]
    public partial class RejestrNapraw
    {
        public int Id { get; set; }

        public int PojazdId { get; set; }

        public DateTime DataPrzyjecia { get; set; }

        public DateTime? DataOdbioru { get; set; }

        public bool CzyWykonane { get; set; }

        public int PrzyjmujacyPracownikId { get; set; }
        
        public int? OddajacyPracownikId { get; set; }
    }
}
