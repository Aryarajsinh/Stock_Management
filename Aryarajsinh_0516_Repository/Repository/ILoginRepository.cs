using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Repository.Repository
{
    public interface ILoginRepository
    {
        UserStockModel GetUserByEmailAndPassword(LoginModel _LoginModel);

        bool UserIsExists(RegisterModel _RegisterModel);

        UsersStock AddNewUser(RegisterModel _RegisterModel);

    }
}
