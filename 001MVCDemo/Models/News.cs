using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCDemo.Models
{
    public class News
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}