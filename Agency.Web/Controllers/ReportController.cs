using System;
using System.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Agency.Common.Controller;
using Agency.Common.Filters;
using Agency.DataLayer.Context;
using Agency.ServiceLayer.Contracts.Report;
using Agency.ServiceLayer.Contracts.Tour;
using Agency.ViewModel.Report;
using Agency.ViewModel.Tour;
using System.Web.Mvc.Html;
using Agency.Common.Extentions;
using Agency.Common.Noty;
using Agency.ServiceLayer.Contracts.Users;
using Agency.ViewModel.Reserve;
using Elmah.ContentSyndication;
using OfficeOpenXml;
using System.Drawing;
using OfficeOpenXml.Style;

namespace Agency.Web.Controllers
{
    [Authorize]
    public class ReportController : BaseController
    {
        #region Fields

        private readonly IUnitOfWork _unitOfWork;
        private readonly IReportService _reportService;
        private readonly ITourService _tourService;
        private readonly IApplicationUserManager _userManager;
        //private readonly IApplicationUserManager _userManager;

        #endregion

        #region Ctor

        public ReportController(IUnitOfWork unitOfWork,
            IReportService reportService, ITourService tourService, IApplicationUserManager userService)
        {
            _reportService = reportService;
            //_userManager = userManager;
            _unitOfWork = unitOfWork;
            _tourService = tourService;
            _userManager = userService;


        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        #region List

        [HttpGet]
        [Route("گزارش/لیست")]
        public virtual async Task<ActionResult> List()
        {
            var viewModel = await _tourService.GetPagedListAsync(new TourSearchRequest());
            return View(viewModel);
        }

        [AjaxOnly]
        [HttpPost]
        [Route("گزارش/لیست")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual async Task<ActionResult> ListAjax(TourSearchRequest request)
        {
            var viewModel = await _tourService.GetPagedListAsync(request);
            if (viewModel.Tours == null || !viewModel.Tours.Any()) return Content("no-more-info");
            return PartialView("_ListAjax", viewModel);

        }

        #endregion

        #region ListTour
        [HttpGet]
        [Route("گزارش/دفاتر")]
        public virtual async Task<ActionResult> ListTour()
        {
            var viewModel = _reportService.GetPagedList(new ReportSearchRequest());
            viewModel.SearchRequest = new ReportSearchRequest();
            viewModel.SearchRequest.UserItem = _userManager.UserSelectListIetm();
            return View(viewModel);
        }


        [HttpPost]
        [AjaxOnly]
        [Route("گزارش/دفاتر")]
        [OutputCache(Location = OutputCacheLocation.None, NoStore = true, Duration = 0)]
        public virtual ActionResult ListAjax2(ReportSearchRequest request)
        {
            var viewModel = _reportService.GetPagedList(request);
            if (viewModel == null || !viewModel.Tours.Any()) return Content("no-more-info");
            foreach (var item in viewModel.Tours)
            {
                item.UserId = request.UserId;
            }
            return PartialView("_ListAjax", viewModel);
        }
        #endregion

        #region Listreserves
        public ActionResult ShowReserves(Guid tourId, Guid userId, string startTime, string endTime)
        {
            var viewmodel = _reportService.GetPagedListAsync(tourId, userId);
            if (viewmodel == null || !viewmodel.Reserves.Any()) return Content("no-more-info");
            return View(viewmodel);
        }
        #endregion


        [HttpGet]
        public ActionResult showreport(Guid id)
        {
            ToursListViewModel viewModel = new ToursListViewModel();
            viewModel.ToursReportList = _reportService.ReportTours(id);
            if (viewModel.ToursReportList.Count == 0)
            {
                this.NotyAlert("گزارشی یافت نشد");
                return RedirectToAction("List", "Report");
            }
            else
                return View(viewModel);
        }

        public ActionResult CanceledReserves(Guid id)
        {
            CancelledToursListViewModel viewModel = new CancelledToursListViewModel();
            viewModel.CancelList = _reportService.CanceledReserves(id);
            if (viewModel.CancelList.Count == 0)
            {
                this.NotyAlert("گزارشی یافت نشد");
                return RedirectToAction("List", "Report");
            }
            else
                return View(viewModel);
        }

        public async Task<ActionResult> showcosts(Guid id)
        {
            OfficePersonListViewModel viewModel = new OfficePersonListViewModel();
            viewModel.OfficePersonList = _reportService.Costs(id);
            if (viewModel.OfficePersonList.Count == 0)
            {
                this.NotyAlert("گزارشی یافت نشد");
                return RedirectToAction("List", "Report");
            }
            return View(viewModel);
        }

        public async Task<ActionResult> showInsurance(Guid id)
        {
            InsuranceListViewModel viewModel = new InsuranceListViewModel();
            viewModel.PersonInsuranceList = _reportService.Insurance(id);
            if (viewModel.PersonInsuranceList.Count == 0)
            {
                this.NotyAlert("گزارشی یافت نشد");
                return RedirectToAction("List", "Report");
            }
            return View(viewModel);
        }

        public async Task<ActionResult> showSeats(Guid id)
        {
            SeatListViewModel viewModel = new SeatListViewModel();
            viewModel.SeatsList = _reportService.SeatsReport(id);
            if (viewModel.SeatsList.Count == 0)
            {
                this.NotyAlert("گزارشی یافت نشد");
                return RedirectToAction("List", "Report");
            }
            return View(viewModel);
        }


        [HttpGet]
        public ActionResult OfficeReportExcel(Guid? tourId, Guid? userId)
        {
            var data = _reportService.GetPagedListAsync(tourId.Value, userId.Value);
            var filename = "OfficeReport.xlsx";
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var fileInfo = new FileInfo(filePath);

            using (var xls = new ExcelPackage(fileInfo))
            {
                var sheet = xls.Workbook.Worksheets.Add("OfficeReport");
                sheet.Cells[1, 1].Value = ("سهم آژانس");
                sheet.Cells[1, 2].Value = ("هزینه بیمه");
                sheet.Cells[1, 3].Value = ("اتاق دو نفره");
                sheet.Cells[1, 4].Value = ("هزینه پایه");
                sheet.Cells[1, 5].Value = ("آدرس");
                sheet.Cells[1, 6].Value = ("تلفن ثابت");
                sheet.Cells[1, 7].Value = ("موبایل");
                sheet.Cells[1, 8].Value = ("شماره شناسنامه");
                sheet.Cells[1, 9].Value = ("کدملی");
                sheet.Cells[1, 10].Value = ("نسبت");
                sheet.Cells[1, 11].Value = ("نام سرپرست");
                sheet.Cells[1, 12].Value = ("نام خانوادگی");
                sheet.Cells[1, 13].Value = ("نام");
                sheet.Cells[1, 14].Value = ("نام تور");
                sheet.Cells[1, 15].Value = ("نام دفتر");
                sheet.Cells[1, 16].Value = ("ردیف");

                int i = 2;
                int a = 1;
                for (int t = 0; t <= data.Reserves.Count; t++)
                {
                    for (int j = 1; j <= 16; j++)
                    {
                        sheet.Cells[t + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        sheet.Cells[t + 1, j].Style.Font.Bold = true;
                    }
                }
                foreach (var item in data.Reserves)
                {
                    sheet.Cells[i, 1].Value = (item.agencyshare);
                    sheet.Cells[i, 2].Value = (item.Insurance);
                    sheet.Cells[i, 3].Value = (item.COupleBedCost);
                    sheet.Cells[i, 4].Value = (item.BaseCost);
                    sheet.Cells[i, 5].Value = (item.Address);
                    sheet.Cells[i, 6].Value = (item.Tel);
                    sheet.Cells[i, 7].Value = (item.Mobile);
                    sheet.Cells[i, 8].Value = (item.IdCode);
                    sheet.Cells[i, 9].Value = (item.NationalCode);
                    sheet.Cells[i, 10].Value = (item.Relation);
                    sheet.Cells[i, 11].Value = (item.ParentName);
                    sheet.Cells[i, 12].Value = (item.LastName);
                    sheet.Cells[i, 13].Value = (item.Name);
                    sheet.Cells[i, 14].Value = (item.TourName);
                    sheet.Cells[i, 15].Value = (item.UserName);
                    sheet.Cells[i, 16].Value = (a);
                    i++;
                    a++;
                }
                sheet.Cells[i + 3, 16].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[i + 3, 16].Style.Font.Bold = true;
                sheet.Cells[i + 3, 16].Value = ("مجموع کل");
                sheet.Cells[i + 4, 16].Value = ("مجموع سهم آژانس");
                foreach (var item in data.Reserves)
                {
                    sheet.Cells[i + 3, 16].Value = (item.TotalCost);
                    sheet.Cells[i + 4, 16].Value = (item.TotalAgencySHare);
                }

                xls.Save();
            }


            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
        public ActionResult Export(Guid id)
        {
            var data = _reportService.ReportTours(id).ToList();
            var filename = "TourCostumers.xlsx";
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var fileInfo = new FileInfo(filePath);

            using (var xls = new ExcelPackage(fileInfo))
            {
                var sheet = xls.Workbook.Worksheets.Add("Tour");
                sheet.Cells[1, 1].Value = ("سهم آژانس");
                sheet.Cells[1, 2].Value = ("هزینه بیمه");
                sheet.Cells[1, 3].Value = ("اتاق دو نفره");
                sheet.Cells[1, 5].Value = ("هزینه پایه");
                sheet.Cells[1, 6].Value = ("آدرس");
                sheet.Cells[1, 7].Value = ("تلفن ثابت");
                sheet.Cells[1, 8].Value = ("موبایل");
                sheet.Cells[1, 9].Value = ("شماره شناسنامه");
                sheet.Cells[1, 10].Value = ("کدملی");
                sheet.Cells[1, 11].Value = ("شماره صندلی");
                sheet.Cells[1, 12].Value = ("نام ماشین");
                sheet.Cells[1, 13].Value = ("نام هتل");
                sheet.Cells[1, 14].Value = ("نسبت");
                sheet.Cells[1, 15].Value = ("نام سرپرست");
                sheet.Cells[1, 16].Value = ("نام خانوادگی");
                sheet.Cells[1, 17].Value = ("نام");
                sheet.Cells[1, 18].Value = ("نام تور");
                sheet.Cells[1, 19].Value = ("نام دفتر");
                sheet.Cells[1, 20].Value = ("ردیف");

                int i = 2;
                for (int t = 0; t <= data.Count; t++)
                {
                    for (int j = 1; j <= 20; j++)
                    {
                        sheet.Cells[t + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        sheet.Cells[t + 1, j].Style.Font.Bold = true;
                    }
                }
                int a = 1;
                foreach (var item in data)
                {

                    sheet.Cells[i, 1].Value = (item.agencyshare);
                    sheet.Cells[i, 2].Value = (item.insurance);
                    sheet.Cells[i, 3].Value = (item.CoupleBedCost);
                    sheet.Cells[i, 5].Value = (item.BaseCost);
                    sheet.Cells[i, 6].Value = (item.Address);
                    sheet.Cells[i, 7].Value = (item.Tel);
                    sheet.Cells[i, 8].Value = (item.Mobile);
                    sheet.Cells[i, 9].Value = (item.IdCode);
                    sheet.Cells[i, 10].Value = (item.NationalCode);
                    sheet.Cells[i, 11].Value = (item.SeatNo);
                    sheet.Cells[i, 12].Value = (item.VehicleName);
                    sheet.Cells[i, 13].Value = (item.HotelName);
                    sheet.Cells[i, 14].Value = (item.Relation);
                    sheet.Cells[i, 15].Value = (item.ParentName);
                    sheet.Cells[i, 16].Value = (item.LastName);
                    sheet.Cells[i, 17].Value = (item.Name);
                    sheet.Cells[i, 18].Value = (item.TourName);
                    sheet.Cells[i, 19].Value = (item.UserName);
                    sheet.Cells[i, 20].Value = a;
                    i++;
                    a++;
                }
                sheet.Cells[i + 3, 18].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[i + 3, 18].Style.Font.Bold = true;
                sheet.Cells[i + 3, 18].Value = ("مجموع کل");
                sheet.Cells[i + 4, 18].Value = ("مجموع سهم آژانس");
                foreach (var item in data)
                {
                    sheet.Cells[i + 3, 17].Value = (item.TotalCost);
                    sheet.Cells[i + 4, 17].Value = (item.TotalAgencyShare);
                }

                xls.Save();
            }


            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);


        }

        public ActionResult ExportCanceledReserves(Guid id)
        {
            var data = _reportService.CanceledReserves(id).ToList();
            var filename = "Customers.xlsx";
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var fileInfo = new FileInfo(filePath);

            using (var xls = new ExcelPackage(fileInfo))
            {
                var sheet = xls.Workbook.Worksheets.Add("Canceled Reserves");
                sheet.Cells[1, 1].Value = ("جریمه");
                sheet.Cells[1, 2].Value = ("تلفن");
                sheet.Cells[1, 3].Value = ("موبایل");
                sheet.Cells[1, 4].Value = ("شماره شناسنامه");
                sheet.Cells[1, 5].Value = ("کد ملی");
                sheet.Cells[1, 6].Value = ("نسبت");
                sheet.Cells[1, 7].Value = ("نام سرپرست");
                sheet.Cells[1, 8].Value = ("نام خانوادگی");
                sheet.Cells[1, 9].Value = ("نام");
                sheet.Cells[1, 10].Value = ("دفتر");
                sheet.Cells[1, 11].Value = ("نام تور");
                sheet.Cells[1, 12].Value = ("ردیف");

                int i = 2;
                int a = 0;
                for (int t = 0; t <= data.Count; t++)
                {
                    for (int j = 1; j <= 12; j++)
                    {
                        sheet.Cells[t + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        sheet.Cells[t + 1, j].Style.Font.Bold = true;
                    }
                }
                foreach (var item in data)
                {

                    sheet.Cells[i, 1].Value = (item.PenaltyCost);
                    sheet.Cells[i, 2].Value = (item.Tel);
                    sheet.Cells[i, 3].Value = (item.Mobile);
                    sheet.Cells[i, 4].Value = (item.IdCode);
                    sheet.Cells[i, 5].Value = (item.NationalCode);
                    sheet.Cells[i, 6].Value = (item.Relation);
                    sheet.Cells[i, 7].Value = (item.ParentName);
                    sheet.Cells[i, 8].Value = (item.LastName);
                    sheet.Cells[i, 9].Value = (item.Name);
                    sheet.Cells[i, 10].Value = (item.UserName);
                    sheet.Cells[i, 11].Value = (item.TourName);
                    sheet.Cells[i, 12].Value = (a);
                    i++;
                    a++;
                }
                sheet.Cells[i + 3, 18].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[i + 3, 18].Style.Font.Bold = true;
                sheet.Cells[i + 3, 18].Value = ("مجموع کل");

                foreach (var item in data)
                {
                    sheet.Cells[i + 3, 17].Value = (item.TotalCost);

                }

                xls.Save();
            }


            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);


        }

        public ActionResult ExportCosts(Guid id)
        {
            var data = _reportService.Costs(id).ToList();
            var filename = "Costs.xlsx";
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var fileInfo = new FileInfo(filePath);

            using (var xls = new ExcelPackage(fileInfo))
            {
                var sheet = xls.Workbook.Worksheets.Add("Costs");
                sheet.Cells[1, 1].Value = ("سهم آژانس");
                sheet.Cells[1, 2].Value = ("هزینه کل");
                sheet.Cells[1, 3].Value = ("تخت دونفره");
                sheet.Cells[1, 4].Value = ("بیمه");
                sheet.Cells[1, 5].Value = ("هزینه پایه");
                sheet.Cells[1, 7].Value = ("تلفن");
                sheet.Cells[1, 8].Value = ("موبایل");
                sheet.Cells[1, 9].Value = ("آدرس");
                sheet.Cells[1, 10].Value = ("محل تولد");
                sheet.Cells[1, 11].Value = ("تاریخ تولد");
                sheet.Cells[1, 12].Value = ("کدملی");
                sheet.Cells[1, 13].Value = ("شماره شناسنامه");
                sheet.Cells[1, 14].Value = ("نسبت");
                sheet.Cells[1, 15].Value = ("نام سرپرست ");
                sheet.Cells[1, 16].Value = ("نام خانوادگی ");
                sheet.Cells[1, 17].Value = ("نام");
                sheet.Cells[1, 18].Value = ("نام دفتر");
                sheet.Cells[1, 19].Value = ("نام تور");
                sheet.Cells[1, 20].Value = ("ردیف");

                for (int t = 0; t <= data.Count; t++)
                {
                    for (int j = 1; j <= 20; j++)
                    {
                        sheet.Cells[t + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        sheet.Cells[t + 1, j].Style.Font.Bold = true;
                    }
                }
                int i = 2;
                int a = 1;
                foreach (var item in data)
                {
                    sheet.Cells[i, 1].Value = (item.agencyshare);
                    sheet.Cells[i, 2].Value = (item.TotalCost);
                    sheet.Cells[i, 3].Value = (item.COupleBedCost);
                    sheet.Cells[i, 4].Value = (item.Insurance);
                    sheet.Cells[i, 5].Value = (item.BaseCost);
                    //   sheet.Cells[i, 6].Value = (item.OptionCOst);
                    sheet.Cells[i, 7].Value = (item.Tel);
                    sheet.Cells[i, 8].Value = (item.Mobile);
                    sheet.Cells[i, 9].Value = (item.Address);
                    sheet.Cells[i, 10].Value = (item.BirthPlace);
                    sheet.Cells[i, 11].Value = (item.BirthDate);
                    sheet.Cells[i, 12].Value = (item.NationalCode);
                    sheet.Cells[i, 13].Value = (item.IdCode);
                    sheet.Cells[i, 14].Value = (item.Relation);
                    sheet.Cells[i, 15].Value = (item.ParentName);
                    sheet.Cells[i, 16].Value = (item.LastName);
                    sheet.Cells[i, 17].Value = (item.Name);
                    sheet.Cells[i, 18].Value = (item.UserName);
                    sheet.Cells[i, 19].Value = (item.TourName);
                    sheet.Cells[i, 20].Value = a;
                    i++;
                    a++;
                }
                sheet.Cells[i + 3, 18].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[i + 3, 18].Style.Font.Bold = true;
                sheet.Cells[i + 3, 18].Value = ("مجموع کل");
                sheet.Cells[i + 4, 18].Value = ("مجموع سهم آژانس");
                foreach (var item in data)
                {
                    sheet.Cells[i + 3, 17].Value = (item.SumCost);
                    sheet.Cells[i + 4, 17].Value = (item.TotalAgencySHare);
                }

                xls.Save();
            }


            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);


        }

        public ActionResult ExportInsurance(Guid id)
        {
            var data = _reportService.Insurance(id).ToList();

            var filename = "Insurance.xlsx";
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var fileInfo = new FileInfo(filePath);

            using (var xls = new ExcelPackage(fileInfo))
            {
                var sheet = xls.Workbook.Worksheets.Add("Tour");
                sheet.Cells[1, 2].Value = ("موبایل");
                sheet.Cells[1, 3].Value = ("کدملی");
                sheet.Cells[1, 4].Value = ("شماره شناسنامه");
                sheet.Cells[1, 5].Value = ("نسبت");
                sheet.Cells[1, 6].Value = ("نام سرپرست ");
                sheet.Cells[1, 7].Value = ("نام خانوادگی ");
                sheet.Cells[1, 8].Value = ("نام");
                sheet.Cells[1, 9].Value = ("نام دفتر");
                sheet.Cells[1, 10].Value = ("نام تور");
                sheet.Cells[1, 11].Value = ("ردیف");


                for (int t = 0; t <= data.Count; t++)
                {
                    for (int j = 1; j <= 11; j++)
                    {
                        sheet.Cells[t + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        sheet.Cells[t + 1, j].Style.Font.Bold = true;
                    }
                }
                int i = 2;
                int a = 0;

                foreach (var item in data)
                {
                    sheet.Cells[i, 2].Value = (item.Mobile);
                    sheet.Cells[i, 3].Value = (item.NationalCode);
                    sheet.Cells[i, 4].Value = (item.IdCode);
                    sheet.Cells[i, 5].Value = (item.Relation);
                    sheet.Cells[i, 6].Value = (item.ParentName);
                    sheet.Cells[i, 7].Value = (item.LastName);
                    sheet.Cells[i, 8].Value = (item.Name);
                    sheet.Cells[i, 9].Value = (item.UserName);
                    sheet.Cells[i, 10].Value = (item.TourName);
                    sheet.Cells[i, 11].Value = (a);
                    i++;
                    a++;
                }
                sheet.Cells[i + 3, 10].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                sheet.Cells[i + 3, 10].Style.Font.Bold = true;

                xls.Save();
            }


            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);


        }

        public ActionResult ExportSeats(Guid id)
        {
            var data = _reportService.SeatsReport(id).ToList();

            var filename = "Seats.xlsx";
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var fileInfo = new FileInfo(filePath);

            using (var xls = new ExcelPackage(fileInfo))
            {
                var sheet = xls.Workbook.Worksheets.Add("Tour");
                sheet.Cells[1, 1].Value = (" شماره صندلی");
                sheet.Cells[1, 2].Value = ("نام وسیله نقلیه ");
                sheet.Cells[1, 3].Value = ("نسبت");
                sheet.Cells[1, 4].Value = ("نام سرپرست ");
                sheet.Cells[1, 5].Value = ("نام خانوادگی ");
                sheet.Cells[1, 6].Value = ("نام ");
                sheet.Cells[1, 7].Value = ("نام دفتر");
                sheet.Cells[1, 8].Value = ("نام تور");
                sheet.Cells[1, 9].Value = ("ردیف");


                for (int t = 0; t <= data.Count; t++)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        sheet.Cells[t + 1, j].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                        sheet.Cells[t + 1, j].Style.Font.Bold = true;
                    }
                }
                int i = 2;
                int a = 1;
                foreach (var item in data)
                {
                    sheet.Cells[i, 1].Value = (item.SeatNo);
                    sheet.Cells[i, 2].Value = (item.VehicleName);
                    sheet.Cells[i, 3].Value = (item.Relation);
                    sheet.Cells[i, 4].Value = (item.ParentName);
                    sheet.Cells[i, 5].Value = (item.LastName);
                    sheet.Cells[i, 6].Value = (item.Name);
                    sheet.Cells[i, 7].Value = (item.UserName);
                    sheet.Cells[i, 8].Value = (item.TourName);
                    sheet.Cells[i, 9].Value = (a);
                    i++;
                    a++;
                }
                xls.Save();
            }


            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);


        }

        public async Task<FilePathResult> ExportSeatSchema(Guid id)
        {
            var data = _reportService.ReportTours(id).ToList();
            var vehicle = new List<VehicleIndexViewModel>();
            var filename = "Customers.xlsx";
            var filePath = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
            var fileInfo = new FileInfo(filePath);
            int k = 0;
            var x = new VehicleIndexViewModel();
            vehicle.Add(x);
            foreach (var item in data)
            {
                if (!vehicle.Any(p => p.VehicleId == item.VehicleId))
                {
                    vehicle[k].VehicleId = item.VehicleId;
                    vehicle[k].Index = item.VehicleIndex;
                    vehicle[k].VehicleName = item.VehicleName;
                    vehicle[k].TourId = item.TourId;
                    k++;
                    var y = new VehicleIndexViewModel();
                    vehicle.Add(y);
                }
            }
            vehicle.RemoveAt(vehicle.Count - 1);
            int i = 0;
            using (var xls = new ExcelPackage(fileInfo))
            {
                foreach (var item in vehicle)
                {
                    var sheet = xls.Workbook.Worksheets.Add(item.VehicleName + "(" + item.Index + ")");
                    var data1 = new List<ToursReportViewModel>();
                    var seatformat = await _tourService.GetSeatManageSchema(new SelectVehicleViewModel()
                    {
                        Id = item.VehicleId,
                        TourId = item.TourId
                    });
                    var row = seatformat.SeatList.Max(p => p.Row);
                    var col = seatformat.SeatList.Max(p => p.Col);

                    foreach (var item1 in data)
                    {
                        if (item1.VehicleId == item.VehicleId)
                        {
                            data1.Add(item1);
                        }
                    }

                    Color colFromHex = System.Drawing.ColorTranslator.FromHtml("#f2f2f2");
                    for (int t = 1; t <= row + 1; t++)
                    {
                        for (int tt = 1; tt <= col + 1; tt++)
                        {
                            sheet.Cells[t, tt].Style.WrapText = true;
                            sheet.Column(tt).Width = 25;
                            sheet.Row(t).Height = 70;
                            sheet.Cells[t, tt].Style.Border.BorderAround(ExcelBorderStyle.Medium);
                            sheet.Cells[t, tt].Style.Font.Bold = true;
                            sheet.Cells[t, tt].Style.Fill.PatternType = ExcelFillStyle.Solid;
                            sheet.Cells[t, tt].Style.Fill.BackgroundColor.SetColor(colFromHex);

                        }
                    }

                    foreach (var item2 in data1)
                    {
                        sheet.Cells[item2.Row + 2 + i, (col + 1) - item2.Col].Value = item2.SeatNo + "\n" + item2.UserName + "\n" + item2.Name + "\n" + item2.LastName;
                    }

                    sheet.Column(3).Width = 25;
                    sheet.Cells[i + 1, 3].Value = data1[0].VehicleName + "(" + data1[0].VehicleIndex + ")";
                    sheet.Cells[i + 1, 3].Style.Font.Size = 32;
                }
                xls.Save();
            }
            var path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/"), filename);
            return File(path, System.Net.Mime.MediaTypeNames.Application.Octet, filename);
        }
    }
}