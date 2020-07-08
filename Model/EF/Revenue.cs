namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Revenue")]
    public partial class Revenue
    {
        [Key]
        public DateTime CreateDate { get; set; }

        [StringLength(50)]
        public string Period { get; set; }

        [Column("Revenue")]
        public decimal? Revenue1 { get; set; }
    }
}
