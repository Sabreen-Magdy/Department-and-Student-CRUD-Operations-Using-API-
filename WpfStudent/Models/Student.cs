using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfStudent.Models
{
    public class Student
    {
        public string name { get; set; }
        public int age { get; set; }
        public string address { get; set; }
        public int deptId { get; set; }
    }
}
