using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.ViewModel
{
    public class ServiceComponentViewModel
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ComponentId { get; set; }
        [DisplayName("Компонент")]
        public string AutoPartsName { get; set; }
        [DisplayName("Количество")]
        public int Count { get; set; }
    }
}
