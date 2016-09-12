namespace PriceCompareLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Item
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Item()
        {
            Carts = new HashSet<Cart>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string ChainId { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string ItemName { get; set; }

        [StringLength(50)]
        public string ManufactureCountry { get; set; }

        [StringLength(50)]
        public string UnitOfMeasure { get; set; }

        public float ItemPrice { get; set; }

        public bool ToShow { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Cart> Carts { get; set; }

        public virtual Store Store { get; set; }
    }
}
