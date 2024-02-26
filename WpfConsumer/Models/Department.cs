using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfConsumer.Models
{
    public class Department
    { 
        public string name { get; set; }
        public string location { get; set; }
        public string mgrName { get; set; }
        //public override string ToString()
        //{
        //    return $"Dept_Name : {name}  ,Dept_Location : {location}  ,Dept_MgrName : {mgrName} ";
        //}
    }
}
