using Microsoft.Phone.Controls;
using System.Windows.Media;

namespace WinnerSV.Views
{
    public partial class SportsView : PhoneApplicationPage
    {
        // Posizione iniziale della griglia con l'anteprima, ottenuta dal 
        // binding nel CompositeTransform.
        private double initialPosition;
        // è true se la grid è visibile, false altrimenti
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
            if (initialPosition == null || initialPosition == 0)
            {
                initialPosition = ct.TranslateY;
                DoubleAnimationUp.From = initialPosition;
                DoubleAnimationDown.To = initialPosition;
                System.Diagnostics.Debug.WriteLine("Grid_ManipulationStarted - Initial Position: " + initialPosition);
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
                ct.TranslateY = 0;
                //setto la grid visible
                isGridUp = true;
            }
            // Swipe Up-Down gesture
            else if (e.TotalManipulation.Translation.Y > 0)
            {
                ct.TranslateY = initialPosition;
                //setto la grid invisible
                isGridUp = false;
            }
            ////e.Handled = true;
        }
    }
}