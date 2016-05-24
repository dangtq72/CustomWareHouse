using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CW_Info;
using CW_Business;

namespace CustomWarehouse.Controllers
{
    public class LoginFirstController : Controller
    {
        public ActionResult ChangePassFirst()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                NaviCommon.Common.log.Error(ex.ToString());
                return View();
            }
        }
    }
}
