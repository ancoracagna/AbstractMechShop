﻿using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceImplementList;
using System;
using System.Collections.Generic;
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
            currentContainer.RegisterType<IClientService, ClientServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IComponentService, ComponentServiceList>(new
HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMechService, MechServiceList>(new
           HierarchicalLifetimeManager());
            currentContainer.RegisterType<IMainService, MainServiceList>(new
           HierarchicalLifetimeManager());
            return currentContainer;
        }
    }
}
