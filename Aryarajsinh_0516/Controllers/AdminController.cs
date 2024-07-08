using Aryarajsinh_0516.Common;
using Aryarajsinh_0516.Session;
using Aryarajsinh_0516.Session.CustomAdminAuthorization;
using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using Aryarajsinh_0516_Repository.Service;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Newtonsoft.Json;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace Aryarajsinh_0516.Controllers
{
    [CustomAdminAuthorization]
    public class AdminController : Controller
    {

        AryarajsinhHarma_0516Entities _AryarajsinhHarma_0516Entities = new AryarajsinhHarma_0516Entities();
        ProductService _ProductService = new ProductService();
        public async Task<ActionResult> Dashboard()
        {
            ViewBag.Email = SessionHelper.Email;
            string Token = Request.Cookies["JWT"].Value;
            string Url = $"api/StockManagement/DashboardAdmin";
            string res = await WebHelper.HttpRequestResponse(Url,Token);
            DashboardModel _DashboardModel = JsonConvert.DeserializeObject<DashboardModel>(res);
            //DashboardModel _DashboardModel = _AryarajsinhHarma_0516Entities.Database.SqlQuery<DashboardModel>("exec DashboardAdmin").FirstOrDefault();
            return View(_DashboardModel);
        }

        public async Task<ActionResult> Product()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                //string URL = "api/StockManagement/GetListOfProduct";
                //var cookie = Request.Cookies["JWT"].Value;
                //string res = await WebHelper.HttpRequestResponse(URL, cookie);
                List<ProductModel> _ProductModelList = _ProductService.GetListOfProduct();

                return View(_ProductModelList);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public ActionResult AddProduct()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                ProductModel _ProductModel = new ProductModel();
                return View(_ProductModel);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductModel _ProductModel)
        {
            ViewBag.Email = SessionHelper.Email;
            try
            {
                if (ModelState.IsValid)
                {

                    var Cookie = Request.Cookies["JWT"].Value;
                    string url = "api/StockManagement/AddProduct";
                    string jsonContent = JsonConvert.SerializeObject(_ProductModel);
                    string res = await WebHelper.HttpClientPostRequests(url, jsonContent, Cookie);
                    Product product = JsonConvert.DeserializeObject<Product>(res);
                    if (product != null)
                    {
                        TempData["Success"] = "Product Added SuccessFully";
                        return RedirectToAction("Product");
                    }
                    else
                    {
                        TempData["Error"] = "Something Went Wrong";
                        return View(_ProductModel);
                    }
                }
                else
                {
                    TempData["Error"] = "Please Add Appropriate Data";
                    return View(_ProductModel);
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<ActionResult> EditProduct(int ProductId)
        {
            ViewBag.Email = SessionHelper.Email;
            try
            {
                var Cookie = Request.Cookies["JWT"].Value;
                string url = $"api/StockManagement/GetProductById?ProductId={ProductId}";
                string res = await WebHelper.HttpRequestResponse(url, Cookie);
                ProductModel _ProductModel = JsonConvert.DeserializeObject<ProductModel>(res);
                ViewBag.product = _AryarajsinhHarma_0516Entities.Product.ToList(); 
                if (_ProductModel != null)
                {
                    return View(_ProductModel);
                }
                return View();
            }
            catch (Exception)
            {

                throw;
            }


        }

        [HttpPost]
        public async Task<ActionResult> EditProduct(ProductModel _ProductModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Email = SessionHelper.Email;
                    var Cookie = Request.Cookies["JWT"].Value;
                    string url = "api/StockManagement/UpdateProduct";
                    string jsonContent = JsonConvert.SerializeObject(_ProductModel);
                    string res = await WebHelper.HttpClientPostRequests(url, jsonContent, Cookie);
                    Product product = JsonConvert.DeserializeObject<Product>(res);
                    TempData["Success"] = "Product Updated SuccessFully";
                    return RedirectToAction("Product");
                }
                else
                {
                    return View(_ProductModel);
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ActionResult> DeleteProduct(int ProductId)
        {
            try
            {

                ViewBag.Email = SessionHelper.Email;
                var Cookie = Request.Cookies["JWT"].Value;
                string Url = $"api/StockManagement/DeleteProduct?ProductId={ProductId}";
                string res = await WebHelper.HttpRequestResponse(Url, Cookie);
                int IsDeleted = JsonConvert.DeserializeObject<int>(res);
                if (IsDeleted > 0)
                {
                    TempData["Success"] = "Poduct Deleted SuccessFully";
                    return RedirectToAction("Product");
                }
                else
                {
                    TempData["Error"] = "Prodct has OrderHistory";
                    return RedirectToAction("Product");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<ActionResult> SupplierList()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                var cookie = Request.Cookies["JWT"].Value;
                string url = "api/StockManagement/GetSupplierList";
                string res = await WebHelper.HttpRequestResponse(url, cookie);
                List<SupplierModel> _SupplierModel = JsonConvert.DeserializeObject<List<SupplierModel>>(res);
                return View(_SupplierModel);

            }
            catch (Exception)
            {

                throw;
            }

        }

        [HttpGet]
        public async Task<ActionResult> OrdersList()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                //List<OrderTableViewModel> _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec GetOrderListForAdmin").ToList();
                string Token = Request.Cookies["JWT"].Value;
                string Url = "api/StockManagement/GetOrerListForAdmin";
                string res = await WebHelper.HttpRequestResponse(Url, Token);
                List<OrderTableViewModel> _OrderTable = JsonConvert.DeserializeObject<List<OrderTableViewModel>>(res).ToList();
                ViewBag.product = _AryarajsinhHarma_0516Entities.Product.ToList();
                
                return View(_OrderTable);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpPost]
        public async Task<ActionResult> OrdersList(FilterDataOrderModel _FilterDataOrderModel)
        {
            try
            {
                if (_FilterDataOrderModel.StartDate > _FilterDataOrderModel.Enddate)
                {
                    ModelState.AddModelError(nameof(_FilterDataOrderModel.StartDate), "Start Date is Not Greeater than End Date");
                    TempData["Error"] = "Start Date is Greater Than Endadate";
                    return RedirectToAction("OrdersList");
                }
                else
                {
                    ViewBag.Email = SessionHelper.Email;
                    int UserId = SessionHelper.UserId;
                    string Token = Request.Cookies["JWT"].Value;
                    /*List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
                    SqlParameter[] sqlParameters = new SqlParameter[]
                        {
                            new SqlParameter("@startdate",_FilterDataOrderModel.StartDateFormatted),
                            new SqlParameter("@endDate",_FilterDataOrderModel.EndDateFormatted)
                    s
                        };
                    ViewBag.startDate = _FilterDataOrderModel.StartDateFormatted;
                    ViewBag.endDate = _FilterDataOrderModel.EndDateFormatted;
                    _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrdersForAdmin @startdate,@endDate", sqlParameters).ToList();*/
                    string Url = "api/StockManagement/GetOrerListForAdminFilterList";
                    string jsonContent = JsonConvert.SerializeObject(_FilterDataOrderModel);
                    string res = await WebHelper.HttpClientPostRequests(Url, jsonContent, Token);
                    List<OrderTableViewModel> _OrderTable = JsonConvert.DeserializeObject<List<OrderTableViewModel>>(res);
                    ViewBag.startDate = _FilterDataOrderModel.StartDateFormatted;
                    ViewBag.endDate = _FilterDataOrderModel.EndDateFormatted;
                    ViewBag.product = _AryarajsinhHarma_0516Entities.Product.ToList();
                    ViewBag.SelectedProduct = _FilterDataOrderModel.ProductName;
                    return View(_OrderTable);
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }


        public async Task<ActionResult> Clear()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                ViewBag.startDate = null;
                ViewBag.endDate = null;
                ViewBag.SelectedProduct = "0";
                return RedirectToAction("OrdersList");
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        [AllowAnonymous]
        public async Task<ActionResult> OrdersRecordListPdf(DateTime? startDate, DateTime? endDate, string ProductName)
        {
            try
            {
                if (startDate == null && endDate == null && ProductName == null)
                {
                    startDate = DateTime.Parse("2024-01-01");
                    endDate = DateTime.Parse("2024-12-31");
                    ProductName = "0";
                }
                int UserId = SessionHelper.UserId;
                List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@startdate",startDate),
                        new SqlParameter("@endDate",endDate),
                        new SqlParameter("@ProductName",ProductName)

                    };
                _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrdersForAdmin @startdate,@endDate,@ProductName", sqlParameters).ToList();

                //string Token = Request.Cookies["JWT"].Value;
                //string Url = $"api/StockManagement/GetPdfViewForAdmin?startDate={startDate}&endDate={endDate}&ProductName={ProductName}";
                //string res = await WebHelper.HttpRequestResponse(Url, Token);
                //_OrderTable = JsonConvert.DeserializeObject<List<OrderTableViewModel>>(res);
                
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                ViewBag.totalcount = _OrderTable.Count;
                ViewBag.SelectedProduct = ProductName;
                if (_OrderTable != null && _OrderTable.Count > 0)
                {
                    return new Rotativa.ViewAsPdf("OrdersRecordListPdf", _OrderTable)
                    {
                        FileName = "OrdersRecordListPdf.pdf",
                        PageSize = Rotativa.Options.Size.A4
                    };
                }
                else
                {
                    TempData["Error"] = "No Records Found";
                    TempData["ProductId"] = ViewBag.SelectedProduct;
                    return RedirectToAction("OrdersList");
                }

                    
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
}