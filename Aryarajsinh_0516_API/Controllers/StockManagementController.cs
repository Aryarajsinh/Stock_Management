using Aryarajsinh_0516_API.JWTSetup;
using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using Aryarajsinh_0516_Repository.Service;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Aryarajsinh_0516_API.Controllers
{
    public class StockManagementController : ApiController
    {
        LoginService _LoginService = new LoginService();
        ProductService _ProductService = new ProductService();
        AryarajsinhHarma_0516Entities _AryarajsinhHarma_0516Entities = new AryarajsinhHarma_0516Entities();

        [Route("api/StockManagement/GetUserByUsernameAndPassword")]
        [HttpPost]
        public UserStockModel GetUserByUsernameAndPassword(LoginModel _LoginModel)
        {
            try
            {
                UserStockModel _UserStockModel1 = new UserStockModel();
                _UserStockModel1 = _LoginService.GetUserByEmailAndPassword(_LoginModel);
                if (_UserStockModel1 != null && _UserStockModel1.UserId > 0)
                {
                    _UserStockModel1.Token = Authentication.GenerateJWTAuthetication(_UserStockModel1);
                    return _UserStockModel1;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [Route("api/StockManagement/UserIsExists")]
        [HttpPost]
        public bool UserIsExists(RegisterModel _RegisterModel)
        {
            bool IsExists = _LoginService.UserIsExists(_RegisterModel);
            if (IsExists)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [Route("api/StockManagement/AddNewUser")]
        [HttpPost]
        public UsersStock AddNewUser(RegisterModel _RegisterModel)
        {
            try
            {
                UsersStock _UsersStock = _LoginService.AddNewUser(_RegisterModel);
                return _UsersStock;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [JwtAuthentication]
        [Route("api/StockManagement/GetListOfProduct")]
        [HttpGet]
        public List<ProductModels> GetListOfProduct(int UserID)
        {
            try
            {
                List<ProductModels> _ProductModel = new List<ProductModels>();
                _ProductModel = _ProductService.GetProductModelsList(UserID);
                return _ProductModel;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [JwtAuthentication]
        [Route("api/StockManagement/AddProduct")]
        [HttpPost]
        public Product AddProduct(ProductModel _ProductModel)
        {
            try
            {
                Product _Product = new Product();
                _Product = _ProductService.AddProduct(_ProductModel);
                return _Product;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [JwtAuthentication]
        [HttpGet]
        [Route("api/StockManagement/GetProductById")]
        public ProductModel GetProductById(int ProductId)
        {
            try
            {
                ProductModel _ProductModel = new ProductModel();
                _ProductModel = _ProductService.GetProductById(ProductId);
                return _ProductModel;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [JwtAuthentication]
        [HttpPost]
        [Route("api/StockManagement/UpdateProduct")]
        public Product UpdateProduct(ProductModel _ProductModel)
        {
            try
            {
                Product _Product = new Product();
                _Product = _ProductService.UpdateProduct(_ProductModel);
                return _Product;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [JwtAuthentication]
        [HttpGet]
        [Route("api/StockManagement/DeleteProduct")]
        public int DeleteProduct(int ProductId)
        {
            try
            {
               int a=  _ProductService.DeleteProduct(ProductId);
               return a;
            }
            catch (Exception e)
            {
                throw e;
            }
        }



        [JwtAuthentication]
        [Route("api/StockManagement/GetSupplierList")]
        [HttpGet]
        public List<SupplierModel> GetSupplierList()
        {
            try
            {
                List<SupplierModel> _SupplierModel = new List<SupplierModel>();
                _SupplierModel = _AryarajsinhHarma_0516Entities.Database.SqlQuery<SupplierModel>("exec SupplierList").ToList();
                return _SupplierModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        [JwtAuthentication]
        [Route("api/StockManagement/GetOrderList")]
        [HttpGet]
        public List<OrderTable> GetOrderList()
        {
            try
            {
                List<OrderTable> _OrderTable = new List<OrderTable>();
                _OrderTable = _AryarajsinhHarma_0516Entities.OrderTable.ToList();
                return _OrderTable;
            }
            catch (Exception)
            {

                throw;
            }

        }


        [JwtAuthentication]
        [Route("api/StockManagement/GetOrerListForAdmin")]
        [HttpGet]
        public List<OrderTableViewModel> GetOrerListForAdmin()
        {
            try
            {
                List<OrderTableViewModel> _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec GetOrderListForAdmin").ToList();
                return _OrderTable;
            }
            catch (Exception)
            {

                throw;
            }

        }
        [JwtAuthentication]
        [Route("api/StockManagement/GetOrerListForAdminFilterList")]
        [HttpPost]
        public List<OrderTableViewModel> GetOrderFilterList(FilterDataOrderModel _FilterDataOrderModel)
        {
            try
            {
                List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@startdate",_FilterDataOrderModel.StartDateFormatted),
                        new SqlParameter("@endDate",_FilterDataOrderModel.EndDateFormatted),
                        new SqlParameter("@ProductName",_FilterDataOrderModel.ProductName)
                    };
                _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrdersForAdmin @startdate,@endDate,@ProductName", sqlParameters).ToList();
                return _OrderTable;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [JwtAuthentication]
        [Route("api/StockManagement/GetCartList")]
        [HttpGet]
        public List<CartModel> GetCartList(int SupplierId)
        {
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                           {
                            new SqlParameter("@SupplierId",SupplierId)
                           };
                List<CartModel> _CartModel = _AryarajsinhHarma_0516Entities.Database.SqlQuery<CartModel>("exec GetOrderManagement @SupplierId", sqlParameters).ToList();
                return _CartModel;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [JwtAuthentication]
        [Route("api/StockManagement/IncreseProductQuantity")]
        [HttpPost]
        public int IncreseProductQuantity(IncreaseQuantityModel _IncreaseQuantityModel)
        {
            try
            {
                
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("@Qunatity",_IncreaseQuantityModel.actualQuantity),
                    new SqlParameter("@Product",_IncreaseQuantityModel.itemId),
                    new SqlParameter("@SupplierId",_IncreaseQuantityModel.suplierid)
                    };

                int a = _AryarajsinhHarma_0516Entities.Database.SqlQuery<int>("exec IncreaseStock @Qunatity,@Product,@SupplierId", sqlParameters).FirstOrDefault();
                return a;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [JwtAuthentication]
        [Route("api/StockManagement/DeleteProducts")]
        [HttpGet]
        public void DeleteProducts(int ProductId)
        {
            try
            {
                Cart _Cart = new Cart();
                _Cart = _AryarajsinhHarma_0516Entities.Cart.FirstOrDefault(u => u.CartId == ProductId);
                _AryarajsinhHarma_0516Entities.Cart.Remove(_Cart);
                _AryarajsinhHarma_0516Entities.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }


        }

        [JwtAuthentication]
        [Route("api/StockManagement/AddToCart")]
        [HttpGet]
        public int AddToCart(int ProductId,int SupplierId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
                   {
                    new SqlParameter("@ProductId",ProductId),
                    new SqlParameter("@supplierId",SupplierId)
                   };
            int a = _AryarajsinhHarma_0516Entities.Database.SqlQuery<int>("exec AddToCart @ProductId,@supplierId", sqlParameters).FirstOrDefault();
            return a;
        }

        [JwtAuthentication]
        [Route("api/StockManagement/GetOrderListFilterSupplier")]
        [HttpPost]
        public List<OrderTableViewModel> GetOrderListFilterSupplier(FilterDataOrderModels _FilterDataOrderModel)
        {
            try
            {
                List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                                new SqlParameter("@startdate",_FilterDataOrderModel.StartDateFormatted),
                                new SqlParameter("@endDate",_FilterDataOrderModel.EndDateFormatted),
                                new SqlParameter("@UserId",_FilterDataOrderModel.UserId)
                    };
                _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrders @startdate,@endDate,@UserId", sqlParameters).ToList();
                return _OrderTable;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [JwtAuthentication]
        [Route("api/StockManagement/GetOrderPdfView")]
        [HttpGet]
        public List<OrderTableViewModel> GetOrderPdfView(DateTime startDate, DateTime endDate,int UserId)
        {
            try
            {
                List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@startdate",startDate),
                        new SqlParameter("@endDate",endDate),
                        new SqlParameter("@UserId",UserId)
                    };
                _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrders @startdate,@endDate,@UserId", sqlParameters).ToList();
                return _OrderTable;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [JwtAuthentication]
        [Route("api/StockManagement/GetPdfViewForAdmin")]
        [HttpGet]
        public List<OrderTableViewModel> GetPdfViewForAdmin(DateTime startDate, DateTime endDate, string ProductName)
        {
            try
            {
                List<OrderTableViewModel> _OrderTable = new List<OrderTableViewModel>();
                SqlParameter[] sqlParameters = new SqlParameter[]
                       {
                        new SqlParameter("@startdate",startDate),
                        new SqlParameter("@endDate",endDate),
                        new SqlParameter("@ProductName",ProductName)
                       };
                _OrderTable = _AryarajsinhHarma_0516Entities.Database.SqlQuery<OrderTableViewModel>("exec SearchOrdersForAdmin @startdate,@endDate,@ProductName", sqlParameters).ToList();
                return _OrderTable;
            }
            catch (Exception)
            {

                throw;
            }

        }

        [JwtAuthentication]
        [Route("api/StockManagement/DashboardSupplier")]
        [HttpGet]
        public DashboardForSupplier DashboardSupplier(int SupplierId)
        {

            SqlParameter[] sqlParameters = new SqlParameter[]
              {
                    new SqlParameter("@SupplierId",SupplierId)
              };
            DashboardForSupplier _DashboardForSupplier = _AryarajsinhHarma_0516Entities.Database.SqlQuery<DashboardForSupplier>("exec DashboardForSupplier @SupplierId", sqlParameters).FirstOrDefault();
            return _DashboardForSupplier;
        }

        [JwtAuthentication]
        [Route("api/StockManagement/DashboardAdmin")]
        [HttpGet]

        public DashboardModel DashboardAdmin()
        {
            try
            {
                DashboardModel _DashboardModel = _AryarajsinhHarma_0516Entities.Database.SqlQuery<DashboardModel>("exec DashboardAdmin").FirstOrDefault();
                return _DashboardModel;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


    }
}
