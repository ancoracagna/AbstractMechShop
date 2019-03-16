using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.Interfaces
{
    public interface IComponentService
    {
        List<AutoPartsViewModel> GetList();
        AutoPartsViewModel GetElement(int id);
        void AddElement(AutoPartsBindingModel model);
        void UpdElement(AutoPartsBindingModel model);
        void DelElement(int id);
    }
}
