using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assignment.Models.Write
{
    public partial class TblPurchase
    {
        public int IntPurchaseId { get; set; }
        public int? IntSupplierId { get; set; }
        public DateTime? DtePurchaseDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
