using Aryarajsinh_0516.Common;
using Aryarajsinh_0516_Model.DbContext;
using Aryarajsinh_0516_Model.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Aryarajsinh_0516.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Register(RegisterModel _RegisterModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string url = "api/StockManagement/UserIsExists";
                    string jsonContent = JsonConvert.SerializeObject(_RegisterModel);
                    string res = await WebHelper.HttpClientPostRequest(url, jsonContent);
                    bool IsExists = JsonConvert.DeserializeObject<bool>(res);
                    if (IsExists)
                    {
                        string Url1 = "api/StockManagement/AddNewUser";
                        string JsonData = JsonConvert.SerializeObject(_RegisterModel);
                        string res1 = await WebHelper.HttpClientPostRequest(Url1, JsonData);
                        UsersStock _UsersStock = JsonConvert.DeserializeObject<UsersStock>(res1);
                        TempData["Success"] = "The User Added SuccessFully";
                        return RedirectToAction("Login", "Login");
                    }
                    else
                    {
                        TempData["Error"] = "Email is Already Exists";
                        return View(_RegisterModel);
                    }


                }
                else
                {
                    TempData["Error"] = "Please Add Appropriate Date";
                    return View(_RegisterModel);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
           

        }
    }
}