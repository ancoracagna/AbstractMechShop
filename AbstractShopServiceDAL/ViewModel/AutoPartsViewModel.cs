using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.ViewModel
{
    public class AutoPartsViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название запчасти")]
        public string AutoPartsName { get; set; }
    }
}
