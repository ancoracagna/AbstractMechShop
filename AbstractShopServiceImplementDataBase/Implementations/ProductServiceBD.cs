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
    public class ProductServiceBD : IMechService
    {
        private AbstractDbContext context;
        public ProductServiceBD(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<ServiceViewModel> GetList()
        {
            List<ServiceViewModel> result = context.Services.Select(rec => new
           ServiceViewModel
            {
                Id = rec.Id,
                ServiceName = rec.ServiceName,
                Price = rec.Price,
                ServiceComponents = context.ServiceComponent
            .Where(recPC => recPC.ServiceId == rec.Id)
           .Select(recPC => new ServiceComponentViewModel
           {
               Id = recPC.Id,
               ServiceId = recPC.ServiceId,
               ComponentId = recPC.ComponentId,
               Count = recPC.Count
           })
           .ToList()
            })
            .ToList();
            return result;
        }
        public ServiceViewModel GetElement(int id)
        {
            Service element = context.Services.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new ServiceViewModel
                {
                    Id = element.Id,
                    ServiceName = element.ServiceName,
                    Price = element.Price,
                    ServiceComponents = context.ServiceComponent
 .Where(recPC => recPC.ServiceId == element.Id)
 .Select(recPC => new ServiceComponentViewModel
 {
     Id = recPC.Id,
     ServiceId = recPC.ServiceId,
     ComponentId = recPC.ComponentId,
 })
 .ToList()
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ServiceBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Service element = context.Services.FirstOrDefault(rec =>
                   rec.ServiceName == model.ServiceName);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = new Service
                    {
                        ServiceName = model.ServiceName,
                        Price = model.Price
                    };
                    context.Services.Add(element);
                    context.SaveChanges();
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
                        context.ServiceComponent.Add(new ServiceComponent
                        {
                            ServiceId = element.Id,
                            ComponentId = groupComponent.ComponentId,
                            Count = groupComponent.Count
                        });
                        context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void UpdElement(ServiceBindingModel model)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Service element = context.Services.FirstOrDefault(rec =>
                   rec.ServiceName == model.ServiceName && rec.Id != model.Id);
                    if (element != null)
                    {
                        throw new Exception("Уже есть изделие с таким названием");
                    }
                    element = context.Services.FirstOrDefault(rec => rec.Id == model.Id);
                    if (element == null)
                    {
                        throw new Exception("Элемент не найден");
                    }
                    element.ServiceName = model.ServiceName;
                    element.Price = model.Price;
                    context.SaveChanges();
                    // обновляем существуюущие компоненты
                    var compIds = model.ServiceComponents.Select(rec =>
                   rec.ComponentId).Distinct();
                    var updateComponents = context.ServiceComponent.Where(rec =>
                   rec.ServiceId == model.Id && compIds.Contains(rec.ComponentId));
                    foreach (var updateComponent in updateComponents)
                    {
                        updateComponent.Count =
                       model.ServiceComponents.FirstOrDefault(rec => rec.Id == updateComponent.Id).Count;
                    }
                    context.SaveChanges();
                    context.ServiceComponent.RemoveRange(context.ServiceComponent.Where(rec =>
                    rec.ServiceId == model.Id && !compIds.Contains(rec.ComponentId)));
                    context.SaveChanges();
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
                        ServiceComponent elementPC =
                       context.ServiceComponent.FirstOrDefault(rec => rec.ServiceId == model.Id &&
                       rec.ComponentId == groupComponent.ComponentId);
                        if (elementPC != null)
                        {
                            elementPC.Count += groupComponent.Count;
                            context.SaveChanges();
                        }
                        else
                        {
                            context.ServiceComponent.Add(new ServiceComponent
                            {
                                ServiceId = model.Id,
                                ComponentId = groupComponent.ComponentId,
                                Count = groupComponent.Count
                            });
                            context.SaveChanges();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void DelElement(int id)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    Service element = context.Services.FirstOrDefault(rec => rec.Id ==
                   id);
                    if (element != null)
                    {
                        // удаяем записи по компонентам при удалении изделия
                        context.ServiceComponent.RemoveRange(context.ServiceComponent.Where(rec =>
                        rec.ServiceId == id));
                        context.Services.Remove(element);
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Элемент не найден");
                    }
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
