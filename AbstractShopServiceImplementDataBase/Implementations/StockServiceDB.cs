using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModel;
using MechShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceImplementDataBase.Implementations
{
    public class StockServiceDB : IStockService
    {
        private AbstractDbContext context;

        public StockServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<StockViewModel> GetList()
        {
            List<StockViewModel> result = context.Stocks.Select(rec => new
            StockViewModel
            {
                Id = rec.Id,
                StockName = rec.StockName,
                StockComponents = context.StockComponents
            .Where(recPC => recPC.StockId == rec.Id)
            .Select(recPC => new StockComponentViewModel
            {
                Id = recPC.Id,
                StockId = recPC.StockId,
                ComponentId = recPC.ComponentId,
                Count = recPC.Count
            })
            .ToList()
            })
            .ToList();
            return result;
        }

        public StockViewModel GetElement(int id)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new StockViewModel
                {
                    Id = element.Id,
                    StockName = element.StockName,
                    StockComponents = context.StockComponents
                .Where(recPC => recPC.StockId == element.Id)
                .Select(recPC => new StockComponentViewModel
                {
                    Id = recPC.Id,
                    StockId = recPC.StockId,
                    ComponentId = recPC.ComponentId,
                    ComponentName = element.StockName,
                    Count = recPC.Count
                })
                .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(StockBindingModel model)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.StockName == model.StockName);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            context.Stocks.Add(new Stock
            {
                StockName = model.StockName
            });
            context.SaveChanges();
        }

        public void UpdElement(StockBindingModel model)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.StockName ==
            model.StockName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            element = context.Stocks.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.StockName = model.StockName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Stock element = context.Stocks.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Stocks.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
