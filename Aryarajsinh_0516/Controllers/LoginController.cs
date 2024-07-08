using Aryarajsinh_0516.Common;
using Aryarajsinh_0516.Session;
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
    public class LoginController : Controller
    {

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Login(LoginModel _LoginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string Url = "api/StockManagement/GetUserByUsernameAndPassword";
                    string jsonData = JsonConvert.SerializeObject(_LoginModel);
                    string res = await WebHelper.HttpClientPostRequest(Url, jsonData);
                    UserStockModel _UserStockModel = JsonConvert.DeserializeObject<UserStockModel>(res);
                    if (_UserStockModel != null)
                    {
                        if (_UserStockModel.Role == _LoginModel.Role)
                        {
                            if (_UserStockModel != null && _UserStockModel.Role != null )
                            {
                                var Cookie = new HttpCookie("JWT", _UserStockModel.Token)
                                {
                                    HttpOnly = true
                                };
                                Response.Cookies.Add(Cookie);
                                Request.Headers.Add("Authorization", "Bearer " + _UserStockModel.Token);
                                if (_UserStockModel.Role == "Admin")
                                {
                                    SetSession(_UserStockModel);
                                    TempData["Success"] = "Admin Login SuccessFully";
                                    return RedirectToAction("Dashboard", "Admin");
                                }
                                else
                                {
                                    SetSession(_UserStockModel);
                                    TempData["Success"] = "Supplier Login SuccessFully";
                                    return RedirectToAction("Dashboard", "Supplier");
                                }
                            }
                            else
                            {
                                TempData["Error"] = "Please Check Credentials";
                                return View(_LoginModel);
                            }
                        }
                        else
                        {
                            TempData["Error"] = "Please Select Appropriate Role";
                            return View(_LoginModel);
                        }
                    }
                 
                    else
                    {
                        TempData["Error"] = "Please Check Credentials";
                        return View(_LoginModel);
                    }

                }
                else
                {
                    TempData["Error"] = "Please Add Appropriate Data";
                    return View(_LoginModel);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void SetSession(UserStockModel _UserStockModel)
        {
            SessionHelper.UserId = _UserStockModel.UserId;
            SessionHelper.Email = _UserStockModel.EmailId;
            SessionHelper.Role = _UserStockModel.Role;
        }
    }

}