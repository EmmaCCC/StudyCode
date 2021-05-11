using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FileUpload;
using WebApi.File.Filters;

namespace WebApi.File.Controllers
{
    [SignatureAuth]
    public class TestController : ApiController
    {
        public IHttpActionResult Time(Order order)
        {

            return Ok(order);
        }
    }

    public class Order
    {
        public string Name { get; set; }
        public List<DateTime> Time { get; set; }
    }
}
