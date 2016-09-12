namespace PriceCompareLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Cart
    {
        public int Id { get; set; }

        public int ItemId { get; set; }

        public float Quantity { get; set; }

        public int UserId { get; set; }

        public virtual Item Item { get; set; }

        public virtual User User { get; set; }
    }
}
