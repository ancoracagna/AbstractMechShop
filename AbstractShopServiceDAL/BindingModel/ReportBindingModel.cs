using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AbstractShopServiceDAL.BindingModel
{
    public class ReportBindingModel
    {
        public string FileName { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}