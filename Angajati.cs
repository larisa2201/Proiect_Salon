namespace SalonModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Angajati")]
    public partial class Angajati
    {
        [Key]
        public int Id_angajat { get; set; }

        [StringLength(50)]
        public string Nume { get; set; }

        [StringLength(50)]
        public string Prenume { get; set; }

        [StringLength(50)]
        public string Telefon { get; set; }

        [Column("E-mail")]
        [StringLength(50)]
        public string E_mail { get; set; }

        public int? Id_serviciu { get; set; }
    }
}
