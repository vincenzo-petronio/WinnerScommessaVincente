using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// interfaccia 
    /// </summary>
    public interface IServiceAgent
    {
        List<Calcio> GetListCalcio();
        List<Basket> GetListBasket();
        List<Tennis> GetListTennis();
    }
}
