using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceImplementList;
using System;
using AbstractShopServiceImplementDataBase;
using AbstractShopServiceImplementDataBase.Implementations;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unity;
using Unity.Lifetime;

namespace AbstractMechShopView
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var container = BuildUnityContainer();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(container.Resolve<FormMain>());
        }
        public static IUnityContainer BuildUnityContainer()
        {
            var currentContainer = new UnityContainer();
            currentContainer.RegisterType<DbContext, AbstractDbContext>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IClientService, ClientServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentService, ComponentServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMechService, ProductServiceBD>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IStockService, StockServiceDB>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceDB>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
