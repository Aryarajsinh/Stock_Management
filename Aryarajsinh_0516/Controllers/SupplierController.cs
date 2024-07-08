using Aryarajsinh_0516.Common;
using Aryarajsinh_0516.Session;
using Aryarajsinh_0516.Session.CustomSupplierAuthorization;
using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using iText.Layout.Borders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aryarajsinh_0516.Controllers
{

    [CustomAutorizationSupplier]
    public class SupplierController : Controller
    {

        AryarajsinhHarma_0516Entities _AryarajsinhHarma_0516Entities = new AryarajsinhHarma_0516Entities();
        
        public async Task<ActionResult> Dashboard()
        {
            ViewBag.Email = SessionHelper.Email;
            int SupplierId = SessionHelper.UserId;

            string JWt = Request.Cookies["JWT"].Value;
            string Url = $"api/StockManagement/DashboardSupplier?SupplierId={SupplierId}";
            string res = await WebHelper.HttpRequestResponse(Url, JWt);
            DashboardForSupplier _DashboardForSupplier = JsonConvert.DeserializeObject<DashboardForSupplier>(res);
            return View(_DashboardForSupplier);
        }

        public async Task<ActionResult> ProductList()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                var cookie = Request.Cookies["JWT"].Value;
                int SupplierId = SessionHelper.UserId;
                string url = $"api/StockManagement/GetListOfProduct?UserID={SupplierId}";
                string res = await WebHelper.HttpRequestResponse(url, cookie);
                List<ProductModels> _ProductModel = JsonConvert.DeserializeObject<List<ProductModels>>(res);
                return View(_ProductModel);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<ActionResult> AddToCart(int ProductId)
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                int SupplierId = SessionHelper.UserId;
                string url = $"api/StockManagement/AddToCart?ProductId={ProductId}&SupplierId={SupplierId}";
                string Token = Request.Cookies["JWT"].Value;
                string res = await WebHelper.HttpRequestResponse(url, Token);
                int a = JsonConvert.DeserializeObject<int>(res);
                
                if (a == 0)
                {
                    TempData["Error"] = "Does Not Have the Enough Quantity";
                }
                else
                {
                    TempData["Success"] = "Product Added In the Cart";
                }
                return RedirectToAction("ProductList");
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public async Task<ActionResult> OrderManagement()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                int SupplierId = SessionHelper.UserId;
                string Token = Request.Cookies["JWT"].Value;
               string Url = $"api/StockManagement/GetCartList?SupplierId={SupplierId}";
                string res = await WebHelper.HttpRequestResponse(Url, Token);
                List<CartModel> _CartModel = JsonConvert.DeserializeObject<List<CartModel>>(res);
                return View(_CartModel);
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpPost]
        public JsonResult OrderManagementAdd(List<OrderModel> _OrderModel)
        {
            ViewBag.Email = SessionHelper.Email;

            try
            {
                if (_OrderModel != null)
                {
                    int SupplierId = SessionHelper.UserId;

                    foreach (var orderModel in _OrderModel)
                    {
                        SqlParameter[] sqlParameters = new SqlParameter[]
                        {
                        new SqlParameter("@Price", orderModel.Price),
                        new SqlParameter("@ProductId", orderModel.ProductId),
                        new SqlParameter("@TotalPrice", orderModel.TotalPrice),
                        new SqlParameter("@Quantity",orderModel.Quantity),
                        new SqlParameter("@SupplierId", SupplierId)
                        };


                        int a = _AryarajsinhHarma_0516Entities.Database.SqlQuery<int>("exec AddOrder @Price,@ProductId,@TotalPrice,@SupplierId,@Quantity", sqlParameters).FirstOrDefault();
                        if (a == 0)
                        {
                            break;
                        }
                    }

                    TempData["Success"] = "Order Added SuccessFully";
                    return Json(new { success = true, redirectTo = Url.Action("OrderManagement", "Supplier") });
                }
                else
                {
                    return Json(new { success = false, error = "No order data received." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<JsonResult> IncreaseQuantity(IncreaseQuantityModel _IncreaseQuantityModel)
        {
            ViewBag.Email = SessionHelper.Email;
            _IncreaseQuantityModel.suplierid = SessionHelper.UserId;
            string Url = "api/StockManagement/IncreseProductQuantity";
            string token = Request.Cookies["JWT"].Value;
            string JsonContent = JsonConvert.SerializeObject(_IncreaseQuantityModel);
            string res = await WebHelper.HttpClientPostRequests(Url, JsonContent, token);
            int a = JsonConvert.DeserializeObject<int>(res);
            return Json(new { a = a }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DeleteProduct(int ProductId)
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                Cart _Cart = new Cart();
            
                string Token = Request.Cookies["JWT"].Value;
                string Url = $"api/StockManagement/DeleteProducts?ProductId={ProductId}";
                string res = await WebHelper.HttpRequestResponse(Url, Token);
                TempData["Success"] = "Deleted From the Cart";
                return RedirectToAction("OrderManagement");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<ActionResult> OrdersList()
        {
            try
            {
                ViewBag.Email = SessionHelper.Email;
                int UserId = SessionHelper.UserId;
                var Cookie = Request.Cookies["JWT"].Value;
                string url = $"api/StockManagement/GetOrderList";
                string res = await WebHelper.HttpRequestResponse(url, Cookie);
              
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@UserId",UserId)

                    };
                List<OrderTableViewModel> _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec GetOrderList @UserId", sqlParameters).ToList();

                //List<OrderTable> _OrderTable = new List<OrderTable>();
                //_OrderTable = _AryarajsinhHarma_0516Entities.OrderTables.ToList();
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
                ViewBag.Email = SessionHelper.Email;
                int UserId = SessionHelper.UserId;
                string Token = Request.Cookies["JWT"].Value;
                List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
                if (_FilterDataOrderModel.StartDate > _FilterDataOrderModel.Enddate)
                {
                    ModelState.AddModelError(nameof(_FilterDataOrderModel.StartDate), "Start Date is Not Greeater than End Date");
                    TempData["Error"] = "Start Date is Greater Than Endadate";
                    return RedirectToAction("OrdersList");
                }
                if ((_FilterDataOrderModel.StartDate.ToString().Contains("1/1/0001 12:00:00 AM"))&&(_FilterDataOrderModel.StartDate.ToString().Contains("1/1/0001 12:00:00 AM")))
                {
                    _FilterDataOrderModel.Enddate = DateTime.Parse("2024-01-01");
                    _FilterDataOrderModel.StartDate = DateTime.Parse("2024-01-01");
                }

                /*SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@startdate",_FilterDataOrderModel.StartDateFormatted),
                        new SqlParameter("@endDate",_FilterDataOrderModel.EndDateFormatted),
                        new SqlParameter("@UserId",UserId)
                    };/*
                string JsonContent = JsonConvert.SerializeObject(_FilterDataOrderModel);
                string Data = JsonConvert.SerializeObject(UserId);
                string actualData =  $"{{ _FilterDataOrderModel: {JsonContent}, UserId: {UserId} }}";*/

                //var objectData = new { _FilterDataOrderModel = _FilterDataOrderModel, UserId = UserId };*/

                FilterDataOrderModels _FilterDataOrderModels = new FilterDataOrderModels();
                _FilterDataOrderModels.StartDate = _FilterDataOrderModel.StartDate;
                _FilterDataOrderModels.Enddate = _FilterDataOrderModel.Enddate;
                _FilterDataOrderModels.UserId = UserId;


                string url = "api/StockManagement/GetOrderListFilterSupplier";
                string actualData = JsonConvert.SerializeObject(_FilterDataOrderModels);
                string res = await WebHelper.HttpClientPostRequests(url, actualData, Token);
                _OrderTable = JsonConvert.DeserializeObject<List<OrderTableViewModel>>(res);
                ViewBag.startDate = _FilterDataOrderModel.StartDateFormatted;
                ViewBag.endDate = _FilterDataOrderModel.EndDateFormatted;
                //_OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrders @startdate,@endDate,@UserId", sqlParameters).ToList();
                return View(_OrderTable);
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
                return RedirectToAction("OrdersList");
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [AllowAnonymous]
        public async Task<ActionResult> OrdersRecordListPdf(DateTime? startDate, DateTime? endDate)
        {
            try
            {
              
                if (startDate == null && endDate == null)
                {
                    startDate = DateTime.Parse("2024-01-01");
                    endDate = DateTime.Parse("2024-12-31");
                }

                int UserId = SessionHelper.UserId;
                List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
            SqlParameter[] sqlParameters = new SqlParameter[]
                {
                        new SqlParameter("@startdate",startDate),
                        new SqlParameter("@endDate",endDate),
                        new SqlParameter("@UserId",UserId)
                };
            _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrders @startdate,@endDate,@UserId", sqlParameters).ToList();

            //string Token = Request.Cookies["JWT"].Value;
            //string Url = $"api/StockManagement/GetOrderPdfView?startDate={startDate}&endDate={endDate}&UserId={UserId}";
            //string res = await WebHelper.HttpRequestResponse(Url, Token);
            //_OrderTable = JsonConvert.DeserializeObject<List< OrderTableViewModel >>(res);
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                ViewBag.totalcount = _OrderTable.Count;

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
                    return RedirectToAction("OrdersList");
                }

            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}