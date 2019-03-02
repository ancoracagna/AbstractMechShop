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
            List<ServiceViewModel> result = new List<ServiceViewModel>();
            for (int i = 0; i < source.Services.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их                
 List<ServiceComponentViewModel> ServiceComponents = new
List<ServiceComponentViewModel>();
                for (int j = 0; j < source.ServiceComponent.Count; ++j)
                {
                    if (source.ServiceComponent[j].ServiceId == source.Services[i].Id)
                    {
                        string autoPartsName = string.Empty;
                        for (int k = 0; k < source.Components.Count; ++k)
                        {
                            if (source.ServiceComponent[j].ComponentId ==
                           source.Components[k].Id)
                            {
                                autoPartsName = source.Components[k].AutoPartsName;
                                break;
                            }
                        }
                        ServiceComponents.Add(new ServiceComponentViewModel
                        {
                            Id = source.ServiceComponent[j].Id,
                            ServiceId = source.ServiceComponent[j].ServiceId,
                            ComponentId = source.ServiceComponent[j].ComponentId,
                            AutoPartsName = autoPartsName,
                            Count = source.ServiceComponent[j].Count
                        });
                    }
                }
                result.Add(new ServiceViewModel
                {
                    Id = source.Services[i].Id,
                    ServiceName = source.Services[i].ServiceName,
                    Price = source.Services[i].Price,
                    ServiceComponents = ServiceComponents
                });
            }
            return result;
        }
        public ServiceViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Services.Count; ++i)
            {
                // требуется дополнительно получить список компонентов для изделия и их                
            List<ServiceComponentViewModel> ServiceComponent = new
List<ServiceComponentViewModel>();
                for (int j = 0; j < source.ServiceComponent.Count; ++j)
                {
                    if (source.ServiceComponent[j].ServiceId == source.Services[i].Id)
                    {
                        string autoPartsName = string.Empty;
                        for (int k = 0; k < source.Components.Count; ++k)
                        {
                            if (source.ServiceComponent[j].ComponentId ==
                           source.Components[k].Id)
                            {
                                autoPartsName = source.Components[k].AutoPartsName;
                                break;
                            }
                        }
                        ServiceComponent.Add(new ServiceComponentViewModel
                        {
                            Id = source.ServiceComponent[j].Id,
                            ServiceId = source.ServiceComponent[j].ServiceId,
                            ComponentId = source.ServiceComponent[j].ComponentId,
                            AutoPartsName = autoPartsName,
                            Count = source.ServiceComponent[j].Count
                        });
                    }
                }
                if (source.Services[i].Id == id)
                {
                    return new ServiceViewModel
                    {
                        Id = source.Services[i].Id,
                        ServiceName = source.Services[i].ServiceName,
                        Price = source.Services[i].Price,
                        ServiceComponents = ServiceComponent
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(ServiceBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Services.Count; ++i)
            {
                if (source.Services[i].Id > maxId)
                {
                    maxId = source.Services[i].Id;
                }
                if (source.Services[i].ServiceName == model.ServiceName)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            source.Services.Add(new Service
            {
                Id = maxId + 1,
                ServiceName = model.ServiceName,
                Price = model.Price
            });
            // компоненты для изделия
            int maxPCId = 0;
            for (int i = 0; i < source.ServiceComponent.Count; ++i)
            {
                if (source.ServiceComponent[i].Id > maxPCId)
                {
                    maxPCId = source.ServiceComponent[i].Id;
                }
            }
            // убираем дубли по компонентам
            for (int i = 0; i < model.ServiceComponents.Count; ++i)
            {
                for (int j = 1; j < model.ServiceComponents.Count; ++j)
                {
                    if (model.ServiceComponents[i].ComponentId ==
                    model.ServiceComponents[j].ComponentId)
                    {
                        model.ServiceComponents[i].Count +=
                        model.ServiceComponents[j].Count;
                        model.ServiceComponents.RemoveAt(j--);
                    }
                }
            }
            // добавляем компоненты
            for (int i = 0; i < model.ServiceComponents.Count; ++i)
            {
                source.ServiceComponent.Add(new ServiceComponent
                {
                    Id = ++maxPCId,
                    ServiceId = maxId + 1,
                    ComponentId = model.ServiceComponents[i].ComponentId,
                    Count = model.ServiceComponents[i].Count
                });
            }
        }
        public void UpdElement(ServiceBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Services.Count; ++i)
            {
                if (source.Services[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Services[i].ServiceName == model.ServiceName &&
                source.Services[i].Id != model.Id)
                {
                    throw new Exception("Уже есть изделие с таким названием");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Services[index].ServiceName = model.ServiceName;
            source.Services[index].Price = model.Price;
            int maxPCId = 0;
            for (int i = 0; i < source.ServiceComponent.Count; ++i)
            {
                if (source.ServiceComponent[i].Id > maxPCId)
                {
                    maxPCId = source.ServiceComponent[i].Id;
                }
            }
            // обновляем существуюущие компоненты
            for (int i = 0; i < source.ServiceComponent.Count; ++i)
            {
                if (source.ServiceComponent[i].ServiceId == model.Id)
                {
                    bool flag = true;
                    for (int j = 0; j < model.ServiceComponents.Count; ++j)
                    {
                        // если встретили, то изменяем количество
                        if (source.ServiceComponent[i].Id ==
                       model.ServiceComponents[j].Id)
                        {
                            source.ServiceComponent[i].Count =
                           model.ServiceComponents[j].Count;
                            flag = false;
                            break;
                        }
                    }
                    // если не встретили, то удаляем
                    if (flag)
                    {
                        source.ServiceComponent.RemoveAt(i--);
                    }
                }
            }
            // новые записи
            for (int i = 0; i < model.ServiceComponents.Count; ++i)
            {
                if (model.ServiceComponents[i].Id == 0)
                {
                    // ищем дубли
                    for (int j = 0; j < source.ServiceComponent.Count; ++j)
                    {
                        if (source.ServiceComponent[j].ServiceId == model.Id &&
                        source.ServiceComponent[j].ComponentId ==
                       model.ServiceComponents[i].ComponentId)
                        {
                            source.ServiceComponent[j].Count +=
                           model.ServiceComponents[i].Count;
                            model.ServiceComponents[i].Id =
                           source.ServiceComponent[j].Id;
                            break;
                        }
                    }
                    // если не нашли дубли, то новая запись
                    if (model.ServiceComponents[i].Id == 0)
                    {
                        source.ServiceComponent.Add(new ServiceComponent
                        {
                            Id = ++maxPCId,
                            ServiceId = model.Id,
                            ComponentId = model.ServiceComponents[i].ComponentId,
                            Count = model.ServiceComponents[i].Count
                        });
                    }
                }
            }
        }
        public void DelElement(int id)
        {
            // удаяем записи по компонентам при удалении изделия
            for (int i = 0; i < source.ServiceComponent.Count; ++i)
            {
                if (source.ServiceComponent[i].ServiceId == id)
                {
                    source.ServiceComponent.RemoveAt(i--);
                }
            }
            for (int i = 0; i < source.Services.Count; ++i)
            {
                if (source.Services[i].Id == id)
                {
                    source.Services.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}
