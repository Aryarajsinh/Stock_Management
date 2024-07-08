using Aryarajsinh_0516_Helper.Helper;
using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using Aryarajsinh_0516_Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aryarajsinh_0516_Repository.Service
{
    public class LoginService : ILoginRepository
    {
        AryarajsinhHarma_0516Entities opetion = new AryarajsinhHarma_0516Entities();
        public UserStockModel GetUserByEmailAndPassword(LoginModel _LoginModel)
        {
            try
            {
                UserStockModel _UserStockModel = new UserStockModel();
                UsersStock _UsersStock = new UsersStock();
                _UsersStock = opetion.UsersStock.FirstOrDefault(u => u.EmailId == _LoginModel.Email && u.Password == _LoginModel.Password);
                if (_UsersStock != null && _UsersStock.UserId > 0)
                {
                    _UserStockModel = HelperClass.ConvertUsterStockToUserStockModel(_UsersStock);
                    return _UserStockModel;
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

        public bool UserIsExists(RegisterModel _RegisterModel)
        {
            var _UsersStock = opetion.UsersStock.FirstOrDefault(u=>u.EmailId== _RegisterModel.EmailId);
            if (_UsersStock != null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public UsersStock AddNewUser(RegisterModel _RegisterModel)
        {
            try
            {
                UsersStock _UsersStock = new UsersStock();
                _UsersStock.EmailId = _RegisterModel.EmailId;
                _UsersStock.Password = _RegisterModel.Password;
                _UsersStock.Role = "Supplier";
                _UsersStock.Username = _RegisterModel.Username;
                opetion.UsersStock.Add(_UsersStock);
                opetion.SaveChanges();
                return _UsersStock;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
