using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.ViewModels;

namespace WebApi2.Controllers
{
    public class StudentController : ApiController
    {
        WebApi2.Models.WebApiDemoEntities db = new Models.WebApiDemoEntities();
        
        [HttpPost]
        [ActionName ("GetAll")]
        public HttpResponseMessage  getAllStudents()
        {
            List<Student> data = db.TBL_STUDENT.Select(y => new Student
            {
                Id = y.Id,
                FirstName = y.FirstName,
                LastName = y.LastName,
                Gender = y.Gender ?? 0,
                DOB = y.DOB ?? DateTime.MinValue
            }).ToList();

            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

    }
}
