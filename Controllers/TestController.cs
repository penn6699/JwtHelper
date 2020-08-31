using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace JwtHelperDemo.Controllers
{

    [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = true)]
    public class TestController : ApiController
    {
        [HttpGet]
        public AjaxResult t1() { 
            return new AjaxResult { 
                success =true,
                data = JWT.JwtHelper.CreateJWT(new { a=123,b=456})
            }; 
        }

        [HttpGet]
        public AjaxResult t2(int i=1) {
            int s = 100 / i;
            return new AjaxResult {success = true,data = s };
        }

    }
}
