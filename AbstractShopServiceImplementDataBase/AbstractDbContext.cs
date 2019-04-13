using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MechShop;
using MechShopModel;
namespace AbstractShopServiceImplementDataBase
{
    public class AbstractDbContext : DbContext
    {
        public AbstractDbContext() : base("AbstractDatabase")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<ServiceComponent> ServiceComponent { get; set; }
        public virtual DbSet<Stock> Stocks { get; set; }
        public virtual DbSet<StockComponent> StockComponents { get; set; }
    }
}
