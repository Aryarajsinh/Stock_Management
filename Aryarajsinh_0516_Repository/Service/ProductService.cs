using Aryarajsinh_0516_Helper.Helper;
using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using Aryarajsinh_0516_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Repository.Service
{
    public class ProductService : IProductRepository
    {
        AryarajsinhHarma_0516Entities _AryarajsinhHarma_0516Entities = new AryarajsinhHarma_0516Entities();

        public List<ProductModels> GetProductModelsList(int SupplierId)
        {
            try
            {

                List<ProductModels> _ProductModels = new List<ProductModels>();
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter("@UserId",SupplierId)
                    };
                _ProductModels = _AryarajsinhHarma_0516Entities.Database.SqlQuery<ProductModels>("exec GetProductList @UserId",sqlParameters).ToList();
                return _ProductModels;
            }
            catch (Exception e)
            {
                throw e;
            }


        }


        public List<ProductModel> GetListOfProduct()
        {
            try
            {
                List<ProductModel> _ProductModel = new List<ProductModel>();
                List<Product> _ProductList = _AryarajsinhHarma_0516Entities.Product.ToList();
                if (_ProductList != null)
                {
                    _ProductModel = HelperClass.ConevertProductListToProductModelList(_ProductList);
                    return _ProductModel;
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

        public Product AddProduct(ProductModel _ProductModel)
        {
            try
            {
                Product _Product = new Product();
                _Product = HelperClass.ConverProductToProductModel(_ProductModel);
                _AryarajsinhHarma_0516Entities.Product.Add(_Product);
                _AryarajsinhHarma_0516Entities.SaveChanges();
                return _Product;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        public ProductModel GetProductById(int ProductId)
        {
            try
            {
                Product _Product = new Product();
                ProductModel _ProductModel = new ProductModel();
                _Product = _AryarajsinhHarma_0516Entities.Product.FirstOrDefault(m => m.ProductId == ProductId);
                _ProductModel = HelperClass.ConvertProductToProductModel(_Product);
                return _ProductModel;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public Product UpdateProduct(ProductModel _ProductModel)
        {
            try
            {
                Product _Product = new Product();

                _Product = HelperClass.ConverProductToProductModel(_ProductModel);
              
                _AryarajsinhHarma_0516Entities.Entry(_Product).State = System.Data.Entity.EntityState.Modified;
                _AryarajsinhHarma_0516Entities.SaveChanges();
                return _Product;
            }
            catch (Exception e)
            {
                throw e;
            }


        }
        public int DeleteProduct(int ProductId)
        {
            Product _Product = new Product();
        
            SqlParameter[] sqlParameters1 = new SqlParameter[]
                {
                    new SqlParameter("@ProductId",ProductId)
                };
            int IsSuccess = _AryarajsinhHarma_0516Entities.Database.SqlQuery<int>("exec DeleteProduct @ProductId",sqlParameters1).FirstOrDefault();
            if (IsSuccess == 0)
            {
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                    new SqlParameter("@ProductId",ProductId)
                    };
                _AryarajsinhHarma_0516Entities.Database.ExecuteSqlCommand("exec DeleteFromCart @ProductId", sqlParameters);
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
