using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        void GetSports(Action<Sports, Exception> callback, string uri);
    }
}
