namespace WinnerSV.Helpers
{
    /// <summary>
    /// Oggetto per passare parametri utili alla navigazione.
    /// </summary>
    public class NavToPage
    {
        /// <summary>
        /// Costruttore
        /// </summary>
        public NavToPage()
        {
        }

        /// <summary>
        /// Nome della vista
        /// </summary>
        public string PageName { get; set; }

        /// <summary>
        /// Parametro per la querystring
        /// </summary>
        public string PageParameter { get; set; }
    }
}
