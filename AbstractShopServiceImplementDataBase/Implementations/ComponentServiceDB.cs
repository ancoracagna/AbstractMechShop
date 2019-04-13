using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModel;
using MechShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceImplementDataBase.Implementations
{
    public class ComponentServiceDB : IComponentService
    {
        private AbstractDbContext context;

        public ComponentServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }

        public List<AutoPartsViewModel> GetList()
        {
            List<AutoPartsViewModel> result = context.Components.Select(rec => new AutoPartsViewModel
            {
                Id = rec.Id,
                AutoPartsName = rec.AutoPartsName
            })
            .ToList();
            return result;
        }

        public AutoPartsViewModel GetElement(int id)
        {
            Component element = context.Components.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new AutoPartsViewModel
                {
                    Id = element.Id,
                    AutoPartsName = element.AutoPartsName
                };
            }
            throw new Exception("Элемент не найден");
        }

        public void AddElement(AutoPartsBindingModel model)
        {
            Component element = context.Components.FirstOrDefault(rec => rec.AutoPartsName == model.AutoPartsName);
            if (element != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            context.Components.Add(new Component
            {
                AutoPartsName = model.AutoPartsName
            });
            context.SaveChanges();
        }

        public void UpdElement(AutoPartsBindingModel model)
        {
            Component element = context.Components.FirstOrDefault(rec => rec.AutoPartsName ==
            model.AutoPartsName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть материал с таким названием");
            }
            element = context.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.AutoPartsName = model.AutoPartsName;
            context.SaveChanges();
        }

        public void DelElement(int id)
        {
            Component element = context.Components.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Components.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}