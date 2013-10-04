using System.Collections.Generic;

namespace WinnerSV.DataModel
{
    /// <summary>
    /// Oggetto per rappresentare tutti i sport seguendo la struttra del file json.
    /// </summary>
    public class Sports
    {
        public List<Calcio> Calcio { get; set; }
        public List<Tennis> Tennis { get; set; }
        public List<Basket> Basket { get; set; }
    }
    
    /// <summary>
    /// Contiene tutti i dati di un incontro sportivo
    /// </summary>
    public class Incontro
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

        ////public override string ToString()
        ////{
        ////    return base.ToString() + ": " + TeamCasa.ToString() + "\t" + TeamFCasa.ToString() + "\t {" + Data.ToString() + "} \n";
        ////}
    }

    /// <summary>
    /// Definisce il nome del campionato e le partite disputate
    /// </summary>
    public class Calcio
    {
        public string NomeCampionato { get; set; }
        public List<Incontro> ElencoIncontriCalcio { get; set; }
    }

    /// <summary>
    /// Definisce il nome del campionato e le partite disputate
    /// </summary>
    public class Tennis
    {
        public string NomeCampionato { get; set; }
        public List<Incontro> ElencoIncontriTennis { get; set; }
    }

    /// <summary>
    /// Definisce il nome del campionato e le partite disputate
    /// </summary>
    public class Basket
    {
        public string NomeCampionato { get; set; }
        public List<Incontro> ElencoIncontriBasket { get; set; }
    }

}
