using System;
using Microsoft.Phone.Controls;
using System.Windows.Media;

namespace WinnerSV.Views
{
    public partial class SportsView : PhoneApplicationPage
    {
        // Posizione iniziale della griglia con l'anteprima, ottenuta dal 
        // binding nel CompositeTransform.
        private double initialPosition;
        // true se l'anteprima è visibile, false altrimenti. Inizialmente è nascosta.
        private bool isGridUp = false;

        /// <summary>
        /// Costruttore
        /// </summary>
        public SportsView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// gestione del Click/Tap sulla griglia con l'anteprima della schedina
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SportsGridAnteprima_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (isGridUp)
            {
                StoryBoardYDown.Begin();
                isGridUp = false;
            }
            else
            {
                StoryBoardYUp.Begin();
                isGridUp = true;
            }

            e.Handled = true;
        }

        private void SportsGridAnteprima_ManipulationStarted(object sender, System.Windows.Input.ManipulationStartedEventArgs e)
        {
            var ct = (CompositeTransform)this.SportsGridAnteprima.RenderTransform;
            if (initialPosition == 0)
            {
                initialPosition = ct.TranslateY;
                DoubleAnimationUp.From = initialPosition;
                DoubleAnimationDown.To = initialPosition;
                System.Diagnostics.Debug.WriteLine("[SPORTSVIEW] \t Grid_ManipulationStarted - Initial Position: " + initialPosition);
            }
            ////e.Handled = true;
        }

        private void SportsGridAnteprima_ManipulationDelta(object sender, System.Windows.Input.ManipulationDeltaEventArgs e)
        {
            var ct = (CompositeTransform)this.SportsGridAnteprima.RenderTransform;
            ct.TranslateY += e.DeltaManipulation.Translation.Y;
            ////e.Handled = true;
        }

        private void SportsGridAnteprima_ManipulationCompleted(object sender, System.Windows.Input.ManipulationCompletedEventArgs e)
        {
            var ct = (CompositeTransform)this.SportsGridAnteprima.RenderTransform;

            // Swipe Down-Up gesture < 0 
            if (e.TotalManipulation.Translation.Y < 0)
            {
                double lr = this.LayoutRoot.ActualHeight;
                double spa = this.SportsGridAnteprima.ActualHeight;
                ////System.Diagnostics.Debug.WriteLine("[DOWN-UP]\nLayoutRoot = " + lr + "\n" + "SportsGridAnteprima = " + spa + "\n" +
                ////    "Translation = " + e.TotalManipulation.Translation.Y + "\n" + 
                ////    "PositionGridAnte = " + ((CompositeTransform)this.SportsGridAnteprima.RenderTransform).TranslateY);
                if (((CompositeTransform)this.SportsGridAnteprima.RenderTransform).TranslateY < 0)
                {
                    // traslazione della Grid nel bordo superiore dello schermo
                    ct.TranslateY = -(lr - spa);
                }
                else
                {
                    // traslazione della Grid in posizione 0  (Grid visibile e allineata con il bordo inferiore dello schermo)
                    ct.TranslateY = 0;
                }

                // In entrambi i casi la Grid è visibile.
                isGridUp = true;
            }
            // Swipe Up-Down gesture
            else if (e.TotalManipulation.Translation.Y > 0)
            {
                var lr = this.LayoutRoot.ActualHeight;
                var spa = this.SportsGridAnteprima.ActualHeight;
                ////System.Diagnostics.Debug.WriteLine("[UP-DOWN]\nLayoutRoot = " + lr + "\n" + "SportsGridAnteprima = " + spa + "\n" +
                ////    "Translation = " + e.TotalManipulation.Translation.Y + "\n" +
                ////    "PositionGridAnte = " + ((CompositeTransform)this.SportsGridAnteprima.RenderTransform).TranslateY);
                if(((CompositeTransform)this.SportsGridAnteprima.RenderTransform).TranslateY < 0)
                {
                    // traslazione della Grid in posizione 0  (Grid visibile e allineata con il bordo inferiore dello schermo)
                    ct.TranslateY = 0;
                }
                else
                {
                    // traslazione della Grid in posizione iniziale (Grid semi nascosta con solo titolo visibile)
                    ct.TranslateY = initialPosition;
                    // solo in questo caso non è visibile
                    isGridUp = false;
                }
            }
            ////e.Handled = true;
        }
    }
}