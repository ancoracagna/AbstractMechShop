using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.Interfaces
{
    public interface IMechService
    {
        List<ServiceViewModel> GetList();
        ServiceViewModel GetElement(int id);
        void AddElement(ServiceBindingModel model);
        void UpdElement(ServiceBindingModel model);
        void DelElement(int id);
    }
}
