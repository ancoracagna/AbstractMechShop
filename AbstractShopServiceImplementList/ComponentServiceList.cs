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
            List<AutoPartsViewModel> result = new List<AutoPartsViewModel>();
            for (int i = 0; i < source.Components.Count; ++i)
            {
                result.Add(new AutoPartsViewModel
                {
                    Id = source.Components[i].Id,
                    AutoPartsName = source.Components[i].AutoPartsName
                });
            }
            return result;
        }
        public AutoPartsViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Components.Count; ++i)
            {
                if (source.Components[i].Id == id)
                {
                    return new AutoPartsViewModel
                    {
                        Id = source.Components[i].Id,
                        AutoPartsName = source.Components[i].AutoPartsName
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(AutoPartsBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Components.Count; ++i)
            {
                if (source.Components[i].Id > maxId)
                {
                    maxId = source.Components[i].Id;
                }
                if (source.Components[i].AutoPartsName == model.AutoPartsName)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            source.Components.Add(new Component
            {
                Id = maxId + 1,
                AutoPartsName = model.AutoPartsName
            });
        }
        public void UpdElement(AutoPartsBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Components.Count; ++i)
            {
                if (source.Components[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Components[i].AutoPartsName == model.AutoPartsName &&
                source.Components[i].Id != model.Id)
                {
                    throw new Exception("Уже есть компонент с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Components[index].AutoPartsName = model.AutoPartsName;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Components.Count; ++i)
            {
                if (source.Components[i].Id == id)
                {
                    source.Components.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }      
    }
}
