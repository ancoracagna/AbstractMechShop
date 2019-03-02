using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.Interfaces
{
    public interface IClientService
    {
        List<CustomerViewModel> GetList();
        CustomerViewModel GetElement(int id);
        void AddElement(CustomerBindingModel model);
        void UpdElement(CustomerBindingModel model);
        void DelElement(int id);
    }
}
