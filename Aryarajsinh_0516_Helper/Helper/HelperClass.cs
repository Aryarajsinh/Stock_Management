using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Helper.Helper
{
    public class HelperClass
    {
        public static UserStockModel ConvertUsterStockToUserStockModel(UsersStock _UsersStock)
        {
            try
            {
                UserStockModel _UserStockModel = new UserStockModel();
                _UserStockModel.EmailId = _UsersStock.EmailId;
                _UserStockModel.Password = _UsersStock.Password;
                _UserStockModel.Role = _UsersStock.Role;
                _UserStockModel.UserId = _UsersStock.UserId;
                return _UserStockModel;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static List<ProductModel> ConevertProductListToProductModelList(List<Product> _Product)
        {
            try
            {
                List<ProductModel> _ProductModelList = new List<ProductModel>();
                foreach (Product item in _Product)
                {
                    ProductModel _ProductModel = new ProductModel();
                    _ProductModel.ProductId = item.ProductId;
                    _ProductModel.ProductName = item.ProductName;
                    _ProductModel.ProductType = item.ProductType;
                    _ProductModel.Quantity = item.Quantity;
                    _ProductModel.ProductDescription = item.ProductDescription;
                    _ProductModel.price = item.price;
                    _ProductModelList.Add(_ProductModel);
                }
                return _ProductModelList;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static Product ConverProductToProductModel(ProductModel _ProductModel)
        {
            try
            {
                Product _Product = new Product();
                _Product.ProductId = _ProductModel.ProductId;
                _Product.ProductName = _ProductModel.ProductName;
                _Product.ProductType = _ProductModel.ProductType;
                _Product.Quantity = _ProductModel.Quantity;
                _Product.price = _ProductModel.price;
                _Product.ProductDescription = _ProductModel.ProductDescription;
                return _Product;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public static ProductModel ConvertProductToProductModel(Product _Product)
        {
            try
            {
                ProductModel _ProductModel = new ProductModel();
                _ProductModel.ProductId = _Product.ProductId;
                _ProductModel.ProductName = _Product.ProductName;
                _ProductModel.ProductDescription = _Product.ProductDescription;
                _ProductModel.Quantity = _Product.Quantity;
                _ProductModel.ProductType = _Product.ProductType;
                _ProductModel.price = _Product.price;
                return _ProductModel;
            }
            catch (Exception e)
            {
                throw e;
            }
           
        }
    }
}
