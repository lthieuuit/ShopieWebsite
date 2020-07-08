namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Bill")]
    public partial class Bill
    {
        [Key]
        public long BillCode { get; set; }

        public long? CustomerID { get; set; }

        public DateTime? Date { get; set; }

        public decimal? Sum { get; set; }
    }
}
