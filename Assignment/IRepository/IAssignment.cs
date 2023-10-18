using Assignment.Helper;
using Assignment.Models.Read;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Assignment.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Principal;

namespace Assignment.IRepository
{
    public interface IAssignment
    {
      //public Task<List<TblItemDto>> GetItem();
      //public Task<AcceptedResult> CreateItem();
      //bool CreateItem([FromBody] TblItemDto tblItem);

      //public Task<string>CreateItem(List<TblItemDto> tblitem);
      //bool ItemIsExist(int IntItemId);

      public Task<string> CreatePartnerType(TblPartnerTypeDto PartnerT);
      public Task<string> CreateCustomerAndSupplier(TblPartnerDto Partner);
      public Task<string> CreateItems(List<TblItemDto> items);
      public Task<string> EditItems(List<TblItemDto> edititem);
      public Task<string> PurchaseItem(TblPurchaseDto Purchas);
      //public Task<string> InserDatasheetToDB(List<DtoGlocation> SheetData);
      public Task<string> CustomerSalesItem(TblSalesDto SalesItem);
      public Task<List<DayPurches>> FindItemDaily(DateTime days);
      public Task<List<DayPurches>> FindItemMonthly(DateTime months);
      public Task<List<DayPurches>> ItemDailyPurchasVsSalesReport(DateTime days);
      public Task<string> TotalReport(DateTime monthlyreport);
      public Task<UserTokenDto> LogIn(UserLoginDto user);
      public Task<bool> CheckTimeExpire(  ClaimsIdentity UserIdentity);
      public Task<string> dataCheck(string data);

    }
}
