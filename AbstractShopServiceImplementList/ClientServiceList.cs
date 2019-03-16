using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.Interfaces;
using AbstractShopServiceDAL.ViewModel;
using MechShop;
using MechShopModel;

namespace AbstractShopServiceImplementList
{
    public class ClientServiceList : IClientService
    {
        private DataListSingleton source;
        public ClientServiceList()
        {
            source = DataListSingleton.GetInstance();
        }
        public List<CustomerViewModel> GetList()
        {           
            List<CustomerViewModel> result = source.Clients.Select(rec => new CustomerViewModel
            {
                Id = rec.Id,
                CustomerFIO = rec.CustomerFIO
            })
            .ToList();
            return result;
        }
        public CustomerViewModel GetElement(int id)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                return new CustomerViewModel
                {
                    Id = element.Id,
                    CustomerFIO = element.CustomerFIO
                };
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CustomerBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.Id) : 0;
            source.Clients.Add(new Client
            {
                Id = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }
        public void UpdElement(CustomerBindingModel model)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.CustomerFIO == model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
        }
        public void DelElement(int id)
        {
            Client element = source.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                source.Clients.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }
    }
}

