namespace Assignment.DTO
{
    public class ReportDto
    {
        //Monthname, year, total purchase amount, total sales amount, profit/loss status

        public string MonthName { get; set; }
        public string Year { get; set; }
        public decimal? TotalPurchesAmount { get; set; }
        public  decimal? TotalSalesAmount { get; set; }
        public string ProfitLoss { get; set; }
    }
}
