﻿using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.Interfaces
{
    public interface IStockService
    {
        List<StockViewModel> GetList();
        StockViewModel GetElement(int id);
        void AddElement(StockBindingModel model);
        void UpdElement(StockBindingModel model);
        void DelElement(int id);
    }
}
