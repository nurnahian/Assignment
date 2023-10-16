using Assignment.Models.Read;
using System;
using System.Collections.Generic;

namespace Assignment.DTO
{
    public class TblPurchaseDto
    {
        public int IntPurchaseId { get; set; }
        public int? IntSupplierId { get; set; }
        public DateTime? DtePurchaseDate { get; set; }
        public bool? IsActive { get; set; }

        public List<TblPurchaseDetailsDto> produc { get; set; }

    }
}
