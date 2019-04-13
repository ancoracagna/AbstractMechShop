using MechShop;
using AbstractShopServiceDAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AbstractShopServiceDAL.ViewModel;
using AbstractShopServiceDAL.BindingModel;

namespace AbstractShopServiceImplementDataBase.Implementations
{
    public class ClientServiceDB : IClientService
    {
        private AbstractDbContext context;
        public ClientServiceDB(AbstractDbContext context)
        {
            this.context = context;
        }
        public List<CustomerViewModel> GetList()
        {
            List<CustomerViewModel> result = context.Clients.Select(rec => new
           CustomerViewModel
            {
                Id = rec.Id,
                CustomerFIO = rec.CustomerFIO
            })
            .ToList();
            return result;
        }
        public CustomerViewModel GetElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
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
            Client element = context.Clients.FirstOrDefault(rec => rec.CustomerFIO ==
           model.CustomerFIO);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            context.Clients.Add(new Client
            {
                CustomerFIO = model.CustomerFIO
            });
            context.SaveChanges();
        }
        public void UpdElement(CustomerBindingModel model)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.CustomerFIO ==
           model.CustomerFIO && rec.Id != model.Id);
            if (element != null)
            {
                throw new Exception("Уже есть клиент с таким ФИО");
            }
            element = context.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            element.CustomerFIO = model.CustomerFIO;
            context.SaveChanges();
        }
        public void DelElement(int id)
        {
            Client element = context.Clients.FirstOrDefault(rec => rec.Id == id);
            if (element != null)
            {
                context.Clients.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

    }
}
