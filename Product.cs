namespace G12_DataImporter;

public sealed class Product
{
	public int Id { get; set; }
	public string ProductName { get; set; } = null!;
	public string ProductCode { get; set; } = null!;
    public decimal Price { get; set; }
	public bool IsDeleted { get; set; }

    public static Product CreateFromString(string input)
    {
        string[] fields = input.Split('\t');
        return new()
        {
            ProductName = fields[3],
            ProductCode = fields[4],
            Price = decimal.Parse(fields[5]),
            IsDeleted = Convert.ToBoolean(byte.Parse(fields[6]))
        };
    }
}