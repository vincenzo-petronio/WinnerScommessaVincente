using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Oggetto per rappresentare tutti i sport seguendo la struttra del file json.
    /// </summary>
    public class Sports
    {
        public Calcio Calcio { get; set; }
        public Tennis Tennis { get; set; }
        public Basket Basket { get; set; }
    }
    
    public class ElencoIncontri
    {
        public string TeamCasa { get; set; }
        public string TeamFCasa { get; set; }
        public string Data { get; set; }
        public string Q1 { get; set; }
        public string QX { get; set; }
        public string Q2 { get; set; }
        public string HC { get; set; }
        public string HC1 { get; set; }
        public string HCX { get; set; }
        public string HC2 { get; set; }
        public string TotalScore { get; set; }
        public string OVER { get; set; }
        public string UNDER { get; set; }
        public string DC1X { get; set; }
        public string DCX2 { get; set; }
        public string DC12 { get; set; }
        public string IdMatch { get; set; }
    }

    public class Calcio
    {
        public string NomeCampionato { get; set; }
        public List<ElencoIncontri> ElencoIncontriCalcio { get; set; }
    }

    public class Tennis
    {
        public string NomeCampionato { get; set; }
        public List<ElencoIncontri> ElencoIncontriTennis { get; set; }
    }

    public class Basket
    {
        public string NomeCampionato { get; set; }
        public List<ElencoIncontri> ElencoIncontriBasket { get; set; }
    }

}
