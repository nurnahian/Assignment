using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Assignment.Models.Read
{
    public partial class TblSales
    {
        public int IntSalesId { get; set; }
        public int? IntCustomerId { get; set; }
        public DateTime? DteSalesDate { get; set; }
        public bool? IsActive { get; set; }
    }
}
