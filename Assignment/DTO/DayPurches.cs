using System;

namespace Assignment.DTO
{
    public class DayPurches
    {
        public int IntItemId { get; set; }
        public string StrItemName { get; set; }
        public decimal? NumItemQuantity { get; set; }
        public decimal? NumUnitPrice { get; set; }
        public string DtePurchaseDate { get; set; }

    }
}
