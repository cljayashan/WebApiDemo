using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi2.ViewModels
{
    public class Student
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Gender { get; set; }
        public DateTime DOB { get; set; }
    }
}