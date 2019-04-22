using AbstractShopServiceDAL.BindingModel;
using AbstractShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractShopServiceDAL.Interfaces
{
    public interface IReportService
    {
        void SaveProductPrice(ReportBindingModel model);
        List<StocksLoadViewModel> GetStocksLoad();
        void SaveStocksLoad(ReportBindingModel model);
        List<ClientOrdersModel> GetClientOrders(ReportBindingModel model);
        void SaveClientOrders(ReportBindingModel model);
    }
}
