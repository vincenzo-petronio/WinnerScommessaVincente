using System;
using System.IO.IsolatedStorage;
namespace WinnerSV.Common
{
    /// <summary>
    /// Costanti hard-coded.
    /// </summary>
    public class Constants
    {
        // ENDPOINT 
        public const string URL_DOMAIN_WINNER = "http://winnerscommessavincente.altervista.org/";
        public const string URL_SUBDOMAIN_JSON = URL_DOMAIN_WINNER + "Json/";
        public const string URL_JSON = "_scommesse.json";

        // DB
        public const string PATH_LOCAL_DB = "db_schedine.sqlite";

        // SETTINGS
        public const string SETTINGS_KEY_CORRENTE_LINGUA = "lingua_corrente";

    }
}
