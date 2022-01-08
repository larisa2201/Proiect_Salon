namespace SalonModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Clienti")]
    public partial class Clienti
    {
        [Key]
        public int Id_client { get; set; }

        [Required]
        [StringLength(50)]
        public string Nume { get; set; }

        [Required]
        [StringLength(50)]
        public string Prenume { get; set; }

        [Required]
        [StringLength(50)]
        public string Telefon { get; set; }

        [Column("E-mail")]
        [Required]
        [StringLength(50)]
        public string E_mail { get; set; }

        [Required]
        [StringLength(50)]
        public string Parola { get; set; }
    }
    
}
