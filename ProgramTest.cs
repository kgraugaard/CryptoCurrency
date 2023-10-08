using Xunit;

public class ProgramTest
{

    [Fact]
    public void Check_that_price_is_invalid()
    {
        var sut = initConverter();

        var exception = Assert.Throws<ArgumentException>(() => sut.SetPricePerUnit("BTC", -0.01));
        Assert.Equal("Price can't be less than zero", exception.Message);

    }

    [Fact]
    public void Check_Convert_amount_is_invalid()
    {
        var sut = initConverter();

        var exception = Assert.Throws<ArgumentException>(() => sut.Convert("BTC", "ETH", -0.01));
        Assert.Equal("Amount can't be less than zero", exception.Message);

    }

    [Fact]
    public void Check_Currency_is_set()
    {
        var sut = initConverter();

        var exception = Assert.Throws<ArgumentException>(() => sut.Convert("BTC", "DKK", 100));

        Assert.Equal("Either From or to Currency not set", exception.Message);
    }

    [InlineData("BTC", "ETH", 100, 5.714285714285714)]
    [InlineData("ETH", "BTC", 100, 1750)]
    [InlineData("BNB", "ETH", 100, 752.9411764705882)]
    [InlineData("BNB", "TETHER", 250, 1.1647058823529413)]
    [Theory]
    public void Calculate_value_from_currency(string from, string to, double amount, double expected)
    {
        var sut = initConverter();

        Assert.Equal(expected, sut.Convert(from, to, amount));
    }


    private static Converter initConverter()
    {

        var sut = new Converter();

        sut.SetPricePerUnit("BTC", 28000.00);
        sut.SetPricePerUnit("ETH", 1600.00);
        sut.SetPricePerUnit("TETHER", 0.99);
        sut.SetPricePerUnit("BNB", 212.50);

        return sut;
    }
}