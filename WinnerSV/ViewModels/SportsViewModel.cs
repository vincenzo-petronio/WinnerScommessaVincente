using GalaSoft.MvvmLight;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using WinnerSV.DataModel;
using WinnerSV.DataSample;

namespace WinnerSV.ViewModels
{

    public class SportsViewModel : ViewModelBase
    {

        private List<Calcio> listCalcio;
        private ObservableCollection<Basket> listBasket;
        private ObservableCollection<Tennis> listTennis;

        /// <summary>
        /// Costruttore.
        /// </summary>
        public SportsViewModel()
        {
            if (IsInDesignMode)
            {
                // TODO Fare injection con IDataService
                SportsData sd = new SportsData();
                listCalcio = sd.ListCalcio;
                listBasket = sd.ListBasket;
                listTennis = sd.ListTennis;
            }
            else
            {
                // real code
            }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Calcio.
        /// </summary>
        public List<Calcio> ListCalcio
        {
            get { return listCalcio; }
        }
        
        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Basket.
        /// </summary>
        public ObservableCollection<Basket> ListBasket
        {
            get { return listBasket; }
        }

        /// <summary>
        /// Proprieta' in binding con la lista presente nel Pivot con i campionati di Tennis.
        /// </summary>
        public ObservableCollection<Tennis> ListTennis
        {
            get { return listTennis; }
        }

        public List<KeyedList<string, Incontro>> GroupedCalcio
        {
            get
            {
                //lo tengo finche non riesco a farlo con la lista fittizia di clacio
                ////List<TestLongListSelector> listProva = new List<TestLongListSelector>()
                ////{
                ////    new TestLongListSelector(){NomeCampionato="nome1", Testo="Testo1.1"},
                ////    new TestLongListSelector(){NomeCampionato="nome1", Testo="Testo1.2"},
                ////    new TestLongListSelector(){NomeCampionato="nome1", Testo="Testo1.3"},
                ////    new TestLongListSelector(){NomeCampionato="nome1", Testo="Testo1.4"},
                ////    new TestLongListSelector(){NomeCampionato="nome1", Testo="Testo1.5"},
                ////    new TestLongListSelector(){NomeCampionato="nome1", Testo="Testo1.6"},
                ////    new TestLongListSelector(){NomeCampionato="nome2", Testo="Testo2.1"},
                ////    new TestLongListSelector(){NomeCampionato="nome2", Testo="Testo2.2"},
                ////    new TestLongListSelector(){NomeCampionato="nome2", Testo="Testo2.3"},
                ////    new TestLongListSelector(){NomeCampionato="nome3", Testo="Testo3.1"},
                ////    new TestLongListSelector(){NomeCampionato="nome4", Testo="Testo4.1"},
                ////    new TestLongListSelector(){NomeCampionato="nome4", Testo="Testo4.2"},
                ////    new TestLongListSelector(){NomeCampionato="nome4", Testo="Testo4.3"},


                ////};


                ////var prova =
                ////    from calcio in listProva
                ////    orderby calcio.NomeCampionato
                ////    group calcio by calcio.NomeCampionato into listaCalcioGroup
                ////    select new KeyedList<string, TestLongListSelector>(listaCalcioGroup);
                var listGruppi =
                    from calcio in listCalcio
                    orderby calcio.NomeCampionato
                    
                        from incontro in calcio.ElencoIncontriCalcio
                        group incontro by calcio.NomeCampionato into  listaCalcioGroup
                        select new KeyedList<string, Incontro>( listaCalcioGroup);

                //return new List<KeyedList<string, Calcio>>(listGruppi);
                return new List<KeyedList<string, Incontro>>(listGruppi);
                //return null;
            }
        }
    }
}
