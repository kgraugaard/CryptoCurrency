public class Converter
{
    //Main method
    public static void Main(string[] args){

        var con = new Converter();

        con.SetPricePerUnit("EUR", 100);
        con.SetPricePerUnit("DKK", 200);
        var res = con.Convert("DKK", "EUR", 200);

        Console.WriteLine(res);
    }

    public List<KeyValuePair<string, double>> CryptoCurrencies;
    public Converter()
    {
        CryptoCurrencies = new List<KeyValuePair<string, double>>();
    }

    private KeyValuePair<string, double> getCurrencyByName(string currencyName)
    {
        return CryptoCurrencies.FirstOrDefault(x => x.Key == currencyName);
    }

    /// <summary>
    /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
    /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
    /// bliver den gamle værdi overskrevet af den nye værdi
    /// </summary>
    /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
    /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
    public void SetPricePerUnit(String currencyName, double price)
    {
        if (price < 0)
            throw new ArgumentException("Price can't be less than zero");

        var currency = getCurrencyByName(currencyName);

        if (currency.Key != null)
        {
            CryptoCurrencies.Remove(currency);
        }

        CryptoCurrencies.Add(new KeyValuePair<string, double>(currencyName, price));
    }

    /// <summary>
    /// Konverterer fra en kryptovaluta til en anden. 
    /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
    /// 
    /// </summary>
    /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
    /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
    /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
    /// <returns>Værdien af beløbet i toCurrencyName</returns>
    public double Convert(String fromCurrencyName, String toCurrencyName, double amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Amount can't be less than zero");
        }

        var fromCurrency = getCurrencyByName(fromCurrencyName);
        var toCurrency = getCurrencyByName(toCurrencyName);

        if (fromCurrency.Key == null || toCurrency.Key == null)
        {
            throw new ArgumentException("Either From or to Currency not set");
        }

        var exchangeRate = toCurrency.Value / fromCurrency.Value;

        return amount * exchangeRate;
    }
}