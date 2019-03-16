using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.ViewModel
{
    public class ServiceViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название услуги")]
        public string ServiceName { get; set; }
        [DisplayName("Цена")]
        public decimal Price { get; set; }
        public List<ServiceComponentViewModel> ServiceComponents { get; set; }
    }
}
