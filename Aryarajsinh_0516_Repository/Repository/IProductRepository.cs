using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Repository.Repository
{
    public interface IProductRepository
    {
        List<ProductModel> GetListOfProduct();

        Product AddProduct(ProductModel _ProductModel);

        ProductModel GetProductById(int ProductId);

        Product UpdateProduct(ProductModel _ProductModel);

        int  DeleteProduct(int ProductId);
        
    }
}
