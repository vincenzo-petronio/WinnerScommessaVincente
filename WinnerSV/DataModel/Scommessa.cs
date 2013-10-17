using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Oggetto per rappresentare un elemento della tabella Scommessa nel DB.
    /// Ogni Scommessa contiene gli stessi elementi dell'oggetto Incontro piu' 
    /// un identificativo.
    /// </summary>
    public class Scommessa
    {
        public int IdScommessa { get; set; } // FOREIGN KEY
        public string TeamCasa { get; set; }
        public string TeamFCasa { get; set; }
        public string Data { get; set; }
        public string IdMatch { get; set; }
        public string TotalScore { get; set; }
        public string OVER { get; set; }
        public string UNDER { get; set; }
        public string Q1 { get; set; }
        public string QX { get; set; }
        public string Q2 { get; set; }
        public string HC { get; set; }
        public string HC1 { get; set; }
        public string HC2 { get; set; }
        public string HCX { get; set; }
        public string DC1X { get; set; }
        public string DC12 { get; set; }
        public string DCX2 { get; set; }
        public string Home12 { get; set; }
        public string Away12 { get; set; }
    }
}
