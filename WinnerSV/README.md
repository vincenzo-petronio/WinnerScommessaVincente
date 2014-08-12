## For devs
* Scaricare l'[ archivio con le dipendenze ](URL).
* **SQLite for Windows Phone SDK 3.8.2**: Lanciare il VSIX e completare l'installazione. Su Windows il percorso di default sarà _C:\Program Files (x86)\Microsoft SDKs\Windows Phone\v8.0\ExtensionSDKs\SQLite.WP80\3.8.2_
* **sqlite-winrt**: Tasto destro sulla Solution -> Add -> Existing Project... e scegliere la cartella **SQLiteWinRTPhone**.
* Tasto destro sul Project WinnerSV -> Add Reference -> Solution -> Projects -> mettere il check sul project SQLiteWinRTPhone precedentemente importato.
* Tasto destro sul Project WinnerSV -> Add Reference -> Windows Phone -> Extensions -> mettere il check su SQLite for Windows Phone.
* Tasto destro sul Project WinnerSV -> Properties -> Build -> aggiungere **USE_WP8_NATIVE_SQLITE** alla riga _Conditional compilation symbols_.
* Impostare in Visual Studio -> Build -> Configuration Manager... -> Active Solution Platform -> **x86** se il deploy è su emulatore, **ARM** se il deploy è su device reale.

*La versione di sqlite-winrt deve coincidere con la versione di SQLite for Windows Phone, 3.8.2 in questo caso!
Prima di aggiornare SQLite for Windows Phone assicurarsi di avere la corrispondente versione per sqlite-winrt*

#### Docs
* https://sqlwinrt.codeplex.com/
* http://www.sqlite.org/download.html#wp8
* http://blogs.msdn.com/b/andy_wigley/archive/2013/06/06/sqlite-winrt-database-programming-on-windows-phone-and-windows-8.aspx
