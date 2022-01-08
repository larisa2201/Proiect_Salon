namespace SalonModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Programari")]
    public partial class Programari
    {
        [Key]
        public int Id_programare { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Data { get; set; }

        [StringLength(10)]
        public string Id_client { get; set; }

        [StringLength(10)]
        public string Id_angajat { get; set; }

        [StringLength(10)]
        public string Id_serviciu { get; set; }
    }
}
