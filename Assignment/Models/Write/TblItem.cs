using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assignment.Models.Write
{
    public partial class TblItem
    {
        public int IntItemId { get; set; }
        public string StrItemName { get; set; }
        public decimal? NumStockQuantity { get; set; }
        public bool? IsActive { get; set; }
    }
}
