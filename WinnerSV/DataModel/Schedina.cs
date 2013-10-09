using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Oggetto per rappresentare una schedina da giocare.
    /// </summary>
    public class Schedina
    {
        [PrimaryKey]
        public string Title { get; set; }
        //public List<Incontro> Incontri { get; set; }
    }
}
