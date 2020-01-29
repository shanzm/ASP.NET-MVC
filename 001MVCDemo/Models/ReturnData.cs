using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class ReturnData<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string Redirect { get; set; }

        public List<T> Data { get; set; }
    }
}