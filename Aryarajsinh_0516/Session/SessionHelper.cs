using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Aryarajsinh_0516.Session
{

    public class SessionHelper
    {
        public static int UserId

        {

            set

            {

                HttpContext.Current.Session["UserId"] = value;

            }

            get

            {

                return HttpContext.Current.Session["UserId"] == null ? 0 : (int)HttpContext.Current.Session["UserId"];

            }

        }

        public static string Email

        {

            get
            {

                return HttpContext.Current.Session["Email"] == null ? "" : (string)HttpContext.Current.Session["Email"];

            }

            set

            {

                HttpContext.Current.Session["Email"] = value;

            }

        }
        public static string Role

        {

            get
            {

                return HttpContext.Current.Session["Role"] == null ? "" : (string)HttpContext.Current.Session["Role"];

            }

            set

            {

                HttpContext.Current.Session["Role"] = value;

            }

        }

    }

}