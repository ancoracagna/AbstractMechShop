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
            List<CustomerViewModel> result = new List<CustomerViewModel>();
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                result.Add(new CustomerViewModel
                {
                    Id = source.Clients[i].Id,
                    CustomerFIO = source.Clients[i].CustomerFIO
                });
            }
            return result;
        }
        public CustomerViewModel GetElement(int id)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == id)
                {
                    return new CustomerViewModel
                    {
                        Id = source.Clients[i].Id,
                        CustomerFIO = source.Clients[i].CustomerFIO
                    };
                }
            }
            throw new Exception("Элемент не найден");
        }
        public void AddElement(CustomerBindingModel model)
        {
            int maxId = 0;
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id > maxId)
                {
                    maxId = source.Clients[i].Id;
                }
                if (source.Clients[i].CustomerFIO == model.CustomerFIO)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            source.Clients.Add(new Client
            {
                Id = maxId + 1,
                CustomerFIO = model.CustomerFIO
            });
        }
        public void UpdElement(CustomerBindingModel model)
        {
            int index = -1;
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == model.Id)
                {
                    index = i;
                }
                if (source.Clients[i].CustomerFIO == model.CustomerFIO &&
                source.Clients[i].Id != model.Id)
                {
                    throw new Exception("Уже есть клиент с таким ФИО");
                }
            }
            if (index == -1)
            {
                throw new Exception("Элемент не найден");
            }
            source.Clients[index].CustomerFIO = model.CustomerFIO;
        }
        public void DelElement(int id)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == id)
                {
                    source.Clients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }
    }
}

