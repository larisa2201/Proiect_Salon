namespace SalonModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Servicii")]
    public partial class Servicii
    {
        [Key]
        public int Id_serviciu { get; set; }

        [StringLength(10)]
        public string Denumire { get; set; }

        public decimal? Pret { get; set; }

        public TimeSpan? Timp_Executie { get; set; }

      /*  public static implicit operator Servicii(Servicii v)
        {
            throw new NotImplementedException();
         } */
    }
}
