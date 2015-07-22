using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Identify.Controllers
{
    [Authorize(Users="")]
    [Authorize(Roles = "admin, mgr")]
    public class SecretController : Controller
    {

        public ContentResult Secret ()
        {
            return Content("This is a secret . . .");
        }

        [AllowAnonymous]
        public ContentResult Overt ()
        {
            return Content("no secrets here. . .");
        }
    }

}