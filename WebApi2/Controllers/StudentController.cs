using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi2.Models;
using WebApi2.ViewModels;

namespace WebApi2.Controllers
{
    public class StudentController : ApiController
    {
        WebApi2.Models.WebApiDemoEntities db = new Models.WebApiDemoEntities();

        [HttpPost]
        [ActionName("GetAll")]
        public HttpResponseMessage getAllStudents()
        {
            List<Student> data;
            try
            {
                data = db.TBL_STUDENT.Select(y => new Student
                {
                    Id = y.Id,
                    FirstName = y.FirstName,
                    LastName = y.LastName,
                    Gender = y.Gender ?? 0,
                    DOB = y.DOB ?? DateTime.MinValue
                }).ToList();

                return Request.CreateResponse(HttpStatusCode.OK, data);

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("GetById")]
        public HttpResponseMessage getStudentById(int id)
        {
            TBL_STUDENT data;
            try
            {
                data = db.TBL_STUDENT.Where(x => x.Id == id).FirstOrDefault();

                return Request.CreateResponse(HttpStatusCode.OK, data);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }


        [HttpPost]
        [ActionName("Create")]
        public HttpResponseMessage addNewStudent([FromBody]Student student)
        {
            try
            {
                var s = db.TBL_STUDENT.Add(new Models.TBL_STUDENT
                {
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Gender = student.Gender,
                    DOB = student.DOB
                });
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, new Student { Id = s.Id, FirstName = s.FirstName, LastName = s.LastName, Gender = (s.Gender ?? 0), DOB = (s.DOB ?? DateTime.MinValue) });
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("Update")]
        public HttpResponseMessage updateExistingStudent(Student student)
        {
            try
            {
                var existing = db.TBL_STUDENT.Where(x => x.Id == student.Id).FirstOrDefault();
                existing.FirstName = student.FirstName;
                existing.LastName = student.LastName;
                existing.Gender = student.Gender;
                existing.DOB = student.DOB;
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, existing);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

        [HttpPost]
        [ActionName("Delete")]
        public HttpResponseMessage deleteExistingStudent(Student student)
        {
            try
            {
                var existing = db.TBL_STUDENT.Where(x => x.Id == student.Id).FirstOrDefault();
                db.TBL_STUDENT.Remove(existing);
                db.SaveChanges();

                return Request.CreateResponse(HttpStatusCode.OK, existing);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
            }
        }

    }
}
