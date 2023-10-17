
using Assignment.DbContexts;
using Assignment.DTO;
using Assignment.IRepository;
using Assignment.Models.Read;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Assignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignment _IRepository;

        

        public AssignmentController(IAssignment IRepository)
        {
            _IRepository = IRepository;
        }


        //public async Task<string> CreatItem(List<TblItemDto> tblitem)
        //{
        //    return await _IRepository.CreateItem(tblitem);
        //}


        
        //1# Create partner type [Customer, Supplier]
        [HttpPost]
        [Route("CreatePartner")]

        public async Task<IActionResult> CreatePartnerType(TblPartnerTypeDto PartnerT)
        {
            var PartnerType = await _IRepository.CreatePartnerType(PartnerT);
            return Ok(PartnerType);
        }



        //2# Create some customer and supplier [Single]
        [HttpPost]
        [Route("CreateCustomerAndSupplier")]

        public async Task<IActionResult> CreateCustomerAndSupplier(TblPartnerDto Partner)
        {
            var CreatePartner = await _IRepository.CreateCustomerAndSupplier(Partner);
            return Ok(CreatePartner); 
        }


       

        //3# Create Some items [List of item, don’t allow duplicates]
        [HttpPost]
        [Route("CreateItems")]

        public async Task<IActionResult> CreateItems(List<TblItemDto> items)
        {
            var itemadd = await _IRepository.CreateItems(items);
            return Ok(itemadd);
        }



        //4# Edit some items [List of item, don’t allow duplicates]
        [HttpPut]
        [Route("EditItems")]
        public async Task<IActionResult> EditItems(List<TblItemDto> edititem)
        {
            var itemEdit = await _IRepository.EditItems(edititem);
            return Ok(itemEdit);
        }




        //5# Purchase Some item from a supplier
        [HttpPost]
        [Route("PurchaseItem")]

        public async Task<IActionResult> PurchaseItem(TblPurchaseDto Purchas)
        {
            var nPurchas = await _IRepository.PurchaseItem(Purchas);
            return Ok(nPurchas);
        }

        //6# Sale some item to a customer [ Check stock while selling items]
        [HttpPost]
        [Route("CustomerSalesItem")]
        public async Task<IActionResult> CustomerSalesItem(TblSalesDto SalesItem )
        {
            var CustomerSalesItem = await _IRepository.CustomerSalesItem(SalesItem);
            return Ok(CustomerSalesItem);
        }
        
        //7# Find item wise Daily Purchase report [Define your own fields for report]
        [HttpGet]
        [Route("FindItemDaily")]

        public async Task<IActionResult> FindItemDaily(DateTime days)
        {
           var  daysReport =await _IRepository.FindItemDaily(days);
            return Ok(daysReport);
        }
        
        //8# Find item wise Monthly Sales report [Define your own fields for report]

        [HttpGet]
        [Route("FindItemMonthly")]

        public async Task<IActionResult> FindItemMonthly(DateTime months)
        {
            var daysReport = await _IRepository.FindItemMonthly(months);
            return Ok(daysReport);
        }

        //09# Find item wise Daily Purchase vs Sales Report
        [HttpGet]
        [Route("ItemDailyPurchasVsSalesReport")]

        public async Task<IActionResult> ItemDailyPurchasVsSalesReport(DateTime days)
        {
            var daysReport = await _IRepository.ItemDailyPurchasVsSalesReport(days);
            return Ok(daysReport);
        }


        //10# Find Report with given column
        //      (Monthname, year, total purchase amount, total sales amount, profit/loss status)

        [HttpGet]
        [Route("TotalReport")]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> TotalReport(DateTime monthlyreport)
        {

            var GetMonthlyReport = await _IRepository.TotalReport(monthlyreport);

            return Ok(GetMonthlyReport);
        }
        [HttpPost]
        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> LogIn(UserLoginDto user)
        {

            return Ok(await _IRepository.LogIn(user));
        }
        [HttpPost]
        [Route("dataCheck")]
        [AllowAnonymous]
        public async Task<IActionResult> dataCheck(string data)
        {

            return Ok(await _IRepository.dataCheck(data));
        }

        //[HttpPost]
        //[Route("InserDatasheetToDB")]

        //public async Task<IActionResult> InserDatasheetToDB(List<DtoGlocation>SheetData)
        //{
        //    var GSheet = await _IRepository.InserDatasheetToDB(SheetData);
        //    return Ok(GSheet);
        //}


        //[HttpGet("GetItem")]
        //public async Task<List<TblItemDto>> GetItem()
        //{
        //    var item = await _IRepository.GetItem();
        //    //var item = await _context.TblItem.ToListAsync();
        //    return item;
        //}


        //[HttpPost]
        //[Route("CreateItem")]
        //public  ActionResult<TblItemDto>CreateItem([FromBody] TblItemDto tblItem)
        //{
        //    //if input null value
        //    if(tblItem == null)
        //    {
        //        return BadRequest("Invalid Item data");
        //    }

        //    // Check if the new EmployeeCode already exists.
        //    if (_IRepository.ItemIsExist(tblItem.IntItemId))
        //    {
        //        return BadRequest("Item Alreday exist");
        //    }

        //    return _IRepository.CreateItem(tblItem) 
        //        ? (ActionResult)Ok("Item created successfully")
        //        : BadRequest("Item creation failed");
        //}
    }
}
