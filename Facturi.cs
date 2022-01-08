namespace SalonModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Facturi")]
    public partial class Facturi
    {
        [Key]
        public int Id_factura { get; set; }

        public int? Id_client { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        public int? Id_serviciu { get; set; }

        public decimal? Total { get; set; }
    }
}
