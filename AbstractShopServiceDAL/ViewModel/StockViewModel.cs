using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.ViewModel
{
    public class StockViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StockName { get; set; }
        public List<StockComponentViewModel> StockComponents { get; set; }
    }
}
