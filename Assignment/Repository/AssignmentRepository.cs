﻿using Assignment.DbContexts;
using Assignment.DTO;
using Assignment.IRepository;
using Assignment.Models.Read;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#pragma warning disable
namespace Assignment.Repository
{
    public class AssignmentRepository : IAssignment
    {
        private readonly WriteDbContext _contextW;
        private readonly ReadDbContext _contextR;
        private IConfiguration _config;
        public AssignmentRepository(WriteDbContext contextW, ReadDbContext contextR,IConfiguration configuration)
        {
            _contextW = contextW;
            _contextR = contextR;
            _config = configuration;
        }

        //1# Create partner type [Customer, Supplier]
        public async Task<string> CreatePartnerType(TblPartnerTypeDto PartnerT)
        {
            try
            {
                var isPresent = await _contextR.TblPartnerType.Where(p=>p.StrPartnerTypeName == PartnerT.StrPartnerTypeName).FirstOrDefaultAsync();
                
                if(isPresent == null)
                {
                    var PartnerTypes = new Models.Write.TblPartnerType()
                    {
                        StrPartnerTypeName = PartnerT.StrPartnerTypeName,
                        IsActive = true
                    };
                    await _contextW.AddAsync(PartnerTypes);
                    await _contextW.SaveChangesAsync();
                    return "Partner Type  Add Successfully";
                }
                else
                    return $"{PartnerT.StrPartnerTypeName} Alrady exist";
            }
            catch (Exception ex)
            {
                throw new Exception("Not Valid Data");
            }
            
        }
        //2# Create some customer and supplier [Single]
        public async Task<string> CreateCustomerAndSupplier(TblPartnerDto Partner)
        {
            try
            {
                var isPresent = await _contextR.TblPartner.Where(p => p.StrPartnerName == Partner.StrPartnerName).FirstOrDefaultAsync();

                if (isPresent == null)
                {
                    var PartnerA = new Models.Write.TblPartner()
                    {
                        StrPartnerName = Partner.StrPartnerName,
                        IntPartnerTypeId = Partner.IntPartnerTypeId,
                        IsActive = true
                    };
                    await _contextW.AddAsync(PartnerA);
                    await _contextW.SaveChangesAsync();
                    return "Partner  Add Successfully";
                }
                else
                    return $"{Partner.StrPartnerName} Alrady exist";
            }
            catch (Exception ex)
            {
                throw new Exception("Not Valid Data");
            }

        }


        //3# Create Some items [List of item, don’t allow duplicates]

        public async Task<string> CreateItems(List<TblItemDto> items)
        {
            try
            {
                string mes = "";

                foreach (var item in items)
                {
                    
                    var isItem = await _contextR.TblItem.Where(n => n.StrItemName == item.StrItemName).AnyAsync();
                    if (isItem == true)
                    {
                        mes = mes+" "+ item.StrItemName;
                    }
                    else
                    {
                        var itemSave = new Models.Write.TblItem()
                        {

                            StrItemName = item.StrItemName,
                            NumStockQuantity = item.NumStockQuantity,
                            IsActive = true
                        };
                        await _contextW.TblItem.AddAsync(itemSave);
                        await _contextW.SaveChangesAsync();
                    }
                }

                if (mes!="")
                {
                    return $"{mes} items are exist in database";
                }
                else
                {
                    return "Succesfully Save!!!!";
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Not Valid Data");
            }
        }


        //4# Edit some items [List of item, don’t allow duplicates]
        public async Task<string> EditItems(List<TblItemDto> edititem )
        {
            try
            {
                List<string> mes = new List<string>();

                foreach (var item in edititem)
                {
                    //var isItem = (from e in _contextR.TblItem
                    //              where e.IntItemId == item.IntItemId
                    //              select e).FirstOrDefault();
                    var isItem = await _contextW.TblItem.Where(n => n.IntItemId == item.IntItemId).FirstOrDefaultAsync();
                    if (isItem != null)
                    {
                        isItem.StrItemName = item.StrItemName;
                        isItem.NumStockQuantity = item.NumStockQuantity;
                        isItem.IsActive = item.IsActive;
                        
                        _contextW.TblItem.Update(isItem);
                        await _contextW.SaveChangesAsync();
                    }
                    else
                    {
                        mes.Add(item.StrItemName);
                        //mes = mes + " " + item.StrItemName;
                    }
                }

                if (mes.Count() > 0)
                {
                    return $"{string.Join(", ", mes)} items are mot exist in database";
                }
                else
                {
                    return "Succesfully Edit!!!!";
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Not Valid Data");
            }
        }



        //5# Purchase Some item from a supplier
        public async Task<string> PurchaseItem(TblPurchaseDto Purchas)
        {
            try
            {
                    var purchaseList = Purchas;
                    var isItemPresent = await _contextR.TblPartner.Where(i => i.IntPartnerId == purchaseList.IntSupplierId).AnyAsync();
                    if (isItemPresent)
                    {
                        var supplierPurchas = new Models.Write.TblPurchase
                        {
                            IntSupplierId = purchaseList.IntSupplierId,
                            DtePurchaseDate = purchaseList.DtePurchaseDate,
                            IsActive = isItemPresent
                        };
                        await _contextW.TblPurchase.AddAsync(supplierPurchas);
                        await _contextW.SaveChangesAsync();

                        foreach (var purchase in Purchas.produc)
                        {
                            int gSupplierId = supplierPurchas.IntPurchaseId;
                         

                            var UserPurchesDetsil = new Models.Write.TblPurchaseDetails()
                            {
                                IntPurchaseId = gSupplierId,
                                IntItemId = purchase.IntItemId,
                                NumItemQuantity = purchase.NumItemQuantity,
                                NumUnitPrice = purchase.NumUnitPrice,
                                IsActive = true
                            };
                            await _contextW.TblPurchaseDetails.AddAsync(UserPurchesDetsil);
                            var iteminStok = await _contextW.TblItem.Where(i=>i.IntItemId ==purchase.IntItemId).FirstOrDefaultAsync();
                            iteminStok.NumStockQuantity = iteminStok.NumStockQuantity - purchase.NumItemQuantity;
                            _contextW.TblItem.Update(iteminStok);
                            await _contextW.SaveChangesAsync();
                            
                        }
                        return "Successful";
                }
                else
                {
                    return "Bad Request";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }
        
        
        //6# Sale some item to a customer [ Check stock while selling items]
        public async Task<string> CustomerSalesItem(TblSalesDto SalesItem)
        {
            try
            {
                var purchaseList = SalesItem;
                var isItemPresent = await _contextR.TblPartner.Where(i => i.IntPartnerId == purchaseList.IntCustomerId).AnyAsync();
                if (isItemPresent)
                {
                    var customerPurchas = new Models.Write.TblSales
                    {
                        IntCustomerId = purchaseList.IntCustomerId,
                        DteSalesDate = purchaseList.DteSalesDate,
                        IsActive = isItemPresent
                    };
                    await _contextW.TblSales.AddAsync(customerPurchas);
                    await _contextW.SaveChangesAsync();

                    foreach (var purchase in SalesItem.ItemSales)
                    {
                        int gCustomerId = customerPurchas.IntSalesId;


                        var CutomerPurchesDetsil = new Models.Write.TblSalesDetails()
                        {
                            IntSalesId = gCustomerId,
                            IntItemId = purchase.IntItemId,
                            NumItemQuantity = purchase.NumItemQuantity,
                            NumUnitPrice = purchase.NumUnitPrice,
                            IsActive = true
                        };
                        await _contextW.TblSalesDetails.AddAsync(CutomerPurchesDetsil);
                        var iteminStok = await _contextW.TblItem.Where(i => i.IntItemId == purchase.IntItemId).FirstOrDefaultAsync();
                        iteminStok.NumStockQuantity = iteminStok.NumStockQuantity - purchase.NumItemQuantity;
                        _contextW.TblItem.Update(iteminStok);
                        await _contextW.SaveChangesAsync();

                    }
                    return "Successful";
                }
                else
                {
                    return "Bad Request";
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error");
            }
        }


        //7# Find item wise Daily Purchase report [Define your own fields for report]
        public async Task<List<DayPurches>> FindItemDaily(DateTime days)
        {
           var day = days.Date.Day;
            
            var daysItems = await (from i in _contextR.TblItem
                             join pd in _contextR.TblPurchaseDetails on i.IntItemId equals pd.IntItemId
                             join p in _contextR.TblPurchase on pd.IntPurchaseId equals p.IntPurchaseId
                             where p.DtePurchaseDate.Value.Day == day
                             select new DayPurches
                             {
                                 IntItemId = i.IntItemId,
                                 StrItemName = i.StrItemName,
                                 NumItemQuantity = pd.NumItemQuantity,
                                 NumUnitPrice = pd.NumUnitPrice,
                                 DtePurchaseDate = days.ToString("yyyy-mm-dd")
                             }).ToListAsync();
            return daysItems;
        }

        //8# Find item wise Monthly Sales report [Define your own fields for report]
        public async Task<List<DayPurches>> FindItemMonthly(DateTime months)
        {
            var rMonths = months.Date.Month;

            var daysItems = await(from i in _contextR.TblItem
                                  join pd in _contextR.TblSalesDetails on i.IntItemId equals pd.IntItemId
                                  join p in _contextR.TblSales on pd.IntSalesId equals p.IntSalesId
                                  where p.DteSalesDate.Value.Month == rMonths
                                  select new DayPurches
                                  {
                                      IntItemId = i.IntItemId,
                                      StrItemName = i.StrItemName,
                                      NumItemQuantity = pd.NumItemQuantity,
                                      NumUnitPrice = pd.NumUnitPrice,
                                      DtePurchaseDate = months.ToString("yyyy-mm-dd")
                                  }).ToListAsync();
            return daysItems;
        }


        //09# Find item wise Daily Purchase vs Sales Report

        public async Task<List<DayPurches>> ItemDailyPurchasVsSalesReport(DateTime days)
        {
            var day = days.Date.Day;

            var PurchesdaysItems = await (from i in _contextR.TblItem
                                   join pd in _contextR.TblPurchaseDetails on i.IntItemId equals pd.IntItemId
                                   join p in _contextR.TblPurchase on pd.IntPurchaseId equals p.IntPurchaseId
                                   where p.DtePurchaseDate.Value.Day == day
                                   select new DayPurches
                                   {
                                       IntItemId = i.IntItemId,
                                       StrItemName = i.StrItemName,
                                       NumItemQuantity = pd.NumItemQuantity,
                                       NumUnitPrice = pd.NumUnitPrice,
                                       DtePurchaseDate = days.ToString("yyyy-mm-dd")
                                   }).ToListAsync();

            

            var SalesdaysItems = await (from i in _contextR.TblItem
                                   join pd in _contextR.TblSalesDetails on i.IntItemId equals pd.IntItemId
                                   join p in _contextR.TblSales on pd.IntSalesId equals p.IntSalesId
                                   where p.DteSalesDate.Value.Day == day
                                   select new DayPurches
                                   {
                                       IntItemId = i.IntItemId,
                                       StrItemName = i.StrItemName,
                                       NumItemQuantity = pd.NumItemQuantity,
                                       NumUnitPrice = pd.NumUnitPrice,
                                       DtePurchaseDate = days.ToString("yyyy-mm-dd")
                                   }).ToListAsync();

            var DaysReport = PurchesdaysItems.Union(SalesdaysItems).ToList();
            return DaysReport;
        }

        //10# Find Report with given column
        //      (Monthname, year, total purchase amount, total sales amount, profit/loss status)


        public async Task<ReportDto> TotalReport(DateTime monthlyreport)
        {
            var months = monthlyreport.Date.Month;

            var PurchesdaysItems = (from i in _contextR.TblItem
                                          join pd in _contextR.TblPurchaseDetails on i.IntItemId equals pd.IntItemId into gt
                                          from d in gt.DefaultIfEmpty()
                                          join p in _contextR.TblPurchase on d.IntPurchaseId equals p.IntPurchaseId into gt1
                                          from g in gt1.DefaultIfEmpty()
                                          where g.DtePurchaseDate.Value.Month == months
                                          select new 
                                          {
                                              TotalPurchesAmount = (decimal)(d.NumItemQuantity*d.NumUnitPrice)
                                          }).ToList();
                                          
            var SalesdaysItems = (from i in _contextR.TblItem
                                        join pd in _contextR.TblSalesDetails on i.IntItemId equals pd.IntItemId into gt
                                        from d in gt.DefaultIfEmpty()
                                        join p in _contextR.TblSales on d.IntSalesId equals p.IntSalesId into gt1
                                        from g in gt1.DefaultIfEmpty()
                                        where g.DteSalesDate.Value.Month == months                                       
                                        select new 
                                        {
                                            TotalSalesAmount = (decimal)(d.NumItemQuantity*d.NumUnitPrice)
                                        }).ToList();




            var TotalPuchesItemSum = PurchesdaysItems.Select(x => x.TotalPurchesAmount).Sum();
            var TotalSalesItemSum = SalesdaysItems.Select(x=>x.TotalSalesAmount).Sum();
            
            //decimal TotalPuchesItemSum = 0;
            //foreach (var pr in PurchesdaysItems)
            //{
            //    TotalPuchesItemSum += pr.TotalPurchesAmount;
            //}

            //decimal TotalSalesItemSum = 0;
            //foreach(var sr in SalesdaysItems)
            //{
            //    TotalPuchesItemSum += sr.TotalSalesAmount;
            //}
            string ProfitOrLoss = "Loss";
            if (TotalPuchesItemSum < TotalSalesItemSum)
            {
                ProfitOrLoss = "Profit";
            }

            var DaysReport = new ReportDto
            {
                Year = monthlyreport.Year.ToString(),
                MonthName = monthlyreport.ToString("MMM"),
                TotalPurchesAmount = TotalPuchesItemSum,
                TotalSalesAmount = TotalSalesItemSum,
                ProfitLoss = ProfitOrLoss
            };
                              
            return DaysReport;
        }

        
        private string GenerateToken(UserLoginDto users)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credential = new SigningCredentials(securityKey,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwr:Audience"],null,
                expires:DateTime.Now.AddMinutes(1),signingCredentials:credential);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public async Task<string> LogIn(UserLoginDto user)
        {
            try
            {

                var isPresent = await _contextR.TblUser.Where(i => i.UserEmail == user.Email.ToLower() && i.UserPass == user.Password).FirstOrDefaultAsync();
                if (isPresent != null)
                {
                    var token = GenerateToken(user);
                    return $"{new { token = token }}";
                }
                return $"{user.Email} is not valid User";
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        //public async Task<string> InserDatasheetToDB(List<DtoGlocation> SheetData)
        //{
        //    try
        //    {
        //        foreach (var item in SheetData)
        //        {
        //            var itemSave = new Models.Write.TblGlocation()
        //            {
        //                    IntWorkplaceId = item.IntWorkplaceId,
        //                    StrWorkplace=item.StrWorkplace,
        //                    StrWorkplaceGroup=item.StrWorkplaceGroup,
        //                    StrBusinessUnitName=item.StrBusinessUnitName,
        //                    StrGoogleLocationName=item.StrGoogleLocationName
        //            };
        //             _contextW.TblGlocation.Add(itemSave);
        //             await _contextW.SaveChangesAsync();
        //        }

        //        return "Successfull";
        //    }
        //    catch (Exception ex)
        //    {

        //        throw new Exception("Not Valid Data");
        //    }

        //}




        //public async Task<List<TblItemDto>> GetItem()
        //{

        //    var item = await _contextR.TblItem.Select(x => new TblItemDto()
        //        {
        //            IntItemId = x.IntItemId,
        //            StrItemName = x.StrItemName ,
        //            NumStockQuantity = x.NumStockQuantity,
        //            IsActive = x.IsActive
        //        }).ToListAsync();
        //    return item;
        //}
        //public bool CreateItem(TblItemDto ItemDto)
        //{


        //    var item =  new Models.Write.TblItem()
        //    {
        //        IntItemId = ItemDto.IntItemId,
        //        StrItemName = ItemDto.StrItemName ,
        //        NumStockQuantity = ItemDto.NumStockQuantity,
        //        IsActive = ItemDto.IsActive,
        //    };
        //            _contextW.TblItem.Add(item);

        //    return _contextW.SaveChanges()>0;

        //}
        //public bool ItemIsExist(int IntItemId)
        //{
        //    return _contextR.TblItem.Any(e=>e.IntItemId == IntItemId);
        //}
        //public async Task<string> CreateItem(List<TblItemDto> tblitem)
        //{

        //   foreach (var item in tblitem)
        //    {
        //        var isItem = await _contextR.TblItem.Where(n=>n.StrItemName == item.StrItemName).ToListAsync();
        //        if(isItem==null)
        //        {
        //            return "Alredy Exist";

        //        }
        //        else
        //        {
        //            var itemSave = new Models.Write.TblItem()
        //            {
        //                IntItemId = item.IntItemId,
        //                StrItemName = item.StrItemName,
        //                NumStockQuantity = item.NumStockQuantity,
        //                IsActive = item.IsActive
        //            };
        //            _contextW.TblItem.Add(itemSave);
        //            await _contextW.SaveChangesAsync();
        //        }
        //    }
        //    return "Succesfully Save!!!!";
        //}
    }
}
