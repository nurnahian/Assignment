using Assignment.Models.Read;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;

namespace Assignment.DTO
{
    public class TblSalesDetailsDto
    {
        public int IntDetailsId { get; set; }
        public int? IntSalesId { get; set; }
        public int? IntItemId { get; set; }
        public decimal? NumItemQuantity { get; set; }
        public decimal? NumUnitPrice { get; set; }
        public bool? IsActive { get; set; }
        
    }
}
