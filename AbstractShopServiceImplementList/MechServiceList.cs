using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModel;
using MechShop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceImplementList
{
    public class MechServiceList : IMechService
    {
        private DataListSingleton source;
        public MechServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<ServiceViewModel> GetList()
        {
            List<ServiceViewModel> result = source.Services
    .Select(rec => new ServiceViewModel
    {
        Id = rec.Id,
        ServiceName = rec.ServiceName,
        Price = rec.Price,
        ServiceComponents = source.ServiceComponent
             .Where(recPC => recPC.ServiceId == rec.Id)
             .Select(recPC => new ServiceComponentViewModel
              {
              Id = recPC.Id,
              ServiceId = recPC.ServiceId,
               ComponentId = recPC.ComponentId,
               AutoPartsName = source.Components.FirstOrDefault(recC =>
               recC.Id == recPC.ComponentId)?.AutoPartsName,
               Count = recPC.Count
               })
              .ToList()
        })
        .ToList();
    return result;
    }
        public ServiceViewModel GetElement(int id)
        {
            Service element = source.Services.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ServiceViewModel
                {
                    Id = element.Id,
                    ServiceName = element.ServiceName,
                    Price = element.Price,
                    ServiceComponents = source.ServiceComponent
                .Where(recPC => recPC.ServiceId == element.Id)
                .Select(recPC => new ServiceComponentViewModel
                {
                    Id = recPC.Id,
                    ServiceId = recPC.ServiceId,
                    ComponentId = recPC.ComponentId,
                    AutoPartsName = source.Components.FirstOrDefault(recC =>
     recC.Id == recPC.ComponentId)?.AutoPartsName,
                    Count = recPC.Count
                })
               .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ServiceBindingModel model)
        {
            Service element = source.Services.FirstOrDefault(rec => rec.ServiceName ==
model.ServiceName);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            int maxId = source.Services.Count > 0 ? source.Services.Max(rec => rec.Id) :
           0;
            source.Services.Add(new Service
            {
                Id = maxId + 1,
                ServiceName = model.ServiceName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = source.ServiceComponent.Count > 0 ?
           source.ServiceComponent.Max(rec => rec.Id) : 0;
            // убираем дубли по компонентам
            var groupComponents = model.ServiceComponents
            .GroupBy(rec => rec.ComponentId)
           .Select(rec => new
           {
               ComponentId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            // добавляем компоненты
            foreach (var groupComponent in groupComponents)
            {
                source.ServiceComponent.Add(new ServiceComponent
                {
                    Id = ++maxPCId,
                    ServiceId = maxId + 1,
                    ComponentId = groupComponent.ComponentId,
                    Count = groupComponent.Count
                });
            }
        }
        public void UpdElement(ServiceBindingModel model)
        {
            Service element = source.Services.FirstOrDefault(rec => rec.ServiceName ==
model.ServiceName && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть изделие с таким названием");
            }
            element = source.Services.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.ServiceName = model.ServiceName;
            element.Price = model.Price;
            int maxPCId = source.ServiceComponent.Count > 0 ?
           source.ServiceComponent.Max(rec => rec.Id) : 0;
            // обновляем существуюущие компоненты
            var compIds = model.ServiceComponents.Select(rec =>
           rec.ComponentId).Distinct();
            var updateComponents = source.ServiceComponent.Where(rec => rec.ServiceId ==
           model.Id && compIds.Contains(rec.ComponentId));
            foreach (var updateComponent in updateComponents)
            {
                updateComponent.Count = model.ServiceComponents.FirstOrDefault(rec =>
               rec.Id == updateComponent.Id).Count;
            }
            source.ServiceComponent.RemoveAll(rec => rec.ServiceId == model.Id &&
           !compIds.Contains(rec.ComponentId));
            // новые записи
            var groupComponents = model.ServiceComponents
            .Where(rec => rec.Id == 0)
           .GroupBy(rec => rec.ComponentId)
           .Select(rec => new
           {
               ComponentId = rec.Key,
               Count = rec.Sum(r => r.Count)
           });
            foreach (var groupComponent in groupComponents)
            {
                ServiceComponent elementPC = source.ServiceComponent.FirstOrDefault(rec
               => rec.ServiceId == model.Id && rec.ComponentId == groupComponent.ComponentId);
                if (elementPC != null)
                {
                    elementPC.Count += groupComponent.Count;
                }
                else
                {
                    source.ServiceComponent.Add(new ServiceComponent
                    {
                        Id = ++maxPCId,
                        ServiceId = model.Id,
                        ComponentId = groupComponent.ComponentId,
                        Count = groupComponent.Count
                    });
                }
            }
        }
        public void DelElement(int id)
        {
            Service element = source.Services.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                // удаяем записи по компонентам при удалении изделия
                source.ServiceComponent.RemoveAll(rec => rec.ServiceId == id);
                source.Services.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}
