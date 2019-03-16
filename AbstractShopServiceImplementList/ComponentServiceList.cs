using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModel;
using MechShopModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceImplementList
{
    public class ComponentServiceList : IComponentService
    {
        private DataListSingleton source;
        public ComponentServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<AutoPartsViewModel> GetList()
        {
            List<AutoPartsViewModel> result = source.Components.Select(rec => new AutoPartsViewModel
            {
                Id = rec.Id,
                AutoPartsName = rec.AutoPartsName
            })
 .ToList();
            return result;
        }
        public AutoPartsViewModel GetElement(int id)
        {
            Component element = source.Components.FirstOrDefault(rec => rec.Id == id);
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
            Component element = source.Components.FirstOrDefault(rec => rec.AutoPartsName == model.AutoPartsName);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            int maxId = source.Components.Count > 0 ? source.Components.Max(rec =>
           rec.Id) : 0;
            source.Components.Add(new Component
            {
                Id = maxId + 1,
                AutoPartsName = model.AutoPartsName
            });        
        }
        public void UpdElement(AutoPartsBindingModel model)
        {
            Component element = source.Components.FirstOrDefault(rec => rec.AutoPartsName == model.AutoPartsName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть компонент с таким названием");
            }
            element = source.Components.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.AutoPartsName = model.AutoPartsName;
        }
        public void DelElement(int id)
        {
            Component element = source.Components.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Components.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }      
    }
}
