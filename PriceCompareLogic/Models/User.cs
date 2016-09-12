namespace PriceCompareLogic
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(20)]
        public string UserName { get; set; }

        public bool Master { get; set; }

        [StringLength(8)]
        public string Password { get; set; }

        public virtual Cart Cart { get; set; }
    }
}
