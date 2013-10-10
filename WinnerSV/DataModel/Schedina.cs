namespace WinnerSV.DataModel
{
    /// <summary>
    /// Oggetto per rappresentare una schedina da giocare memorizzata nel DB.
    /// </summary>
    public class Schedina
    {
        public int Id { get; set; }
        public string Title { get; set; }
        //public List<Incontro> Incontri { get; set; }
    }
}
