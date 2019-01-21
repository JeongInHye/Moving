using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Modules;

namespace WebAPI.Controllers
{
    [ApiController]
    public class DataController : ControllerBase
    {
        [Route("api/Select")]
        [HttpPost]
        public ActionResult<ArrayList> Select()
        {
            return Query.GetSelect();
        }

        [Route("api/Select")]
        [HttpGet]
        public ActionResult<ArrayList> Select1()
        {
            return Query.GetSelect();
        }

        [Route("api/Insert")]
        [HttpPost]
        public ActionResult<ArrayList> Insert([FromForm] Data data)
        {
            return Query.GetInsert(data);
        }

        [Route("api/Update")]
        [HttpPost]
        public ActionResult<ArrayList> Update([FromForm] Data test)
        {
            return Query.GetUpdate(test);
        }

        [Route("api/Delete")]
        [HttpPost]
        public ActionResult<ArrayList> Delete([FromForm] Data test)
        {
            return Query.GetDelete(test);
        }
    }
}
