using System;
using MechShopModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MechShop;

namespace AbstractShopServiceImplementList
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Client> Clients { get; set; }
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Service> Services { get; set; }
        public List<ServiceComponent> ServiceComponent { get; set; }
        private DataListSingleton()
        {
            Clients = new List<Client>();
            Components = new List<Component>();
            Orders = new List<Order>();
            Services = new List<Service>();
            ServiceComponent = new List<ServiceComponent>();
     //       Stocks = new List<Stock>();
     //       StockComponents = new List<StockComponent>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
