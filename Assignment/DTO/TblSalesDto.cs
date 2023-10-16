using System;
using System.Collections.Generic;

namespace Assignment.DTO
{
    public class TblSalesDto
    {
        public int IntSalesId { get; set; }
        public int? IntCustomerId { get; set; }
        public DateTime? DteSalesDate { get; set; }
        public bool? IsActive { get; set; }
        public List<TblSalesDetailsDto>ItemSales {  get; set; }
    }
}
