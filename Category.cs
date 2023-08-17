namespace G12_DataImporter;

public sealed class Category
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public string CategoryCode { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
    
    public static Category CreateFromString(string input)
    {
        string[] fields = input.Split('\t');
        return new()
        {
            CategoryName = fields[0],
            CategoryCode = fields[1],
            IsDeleted = Convert.ToBoolean(byte.Parse(fields[2]))
        };
    }

    public static string GetCodeFromString(string input)
    {
        return input.Split('\t')[1];
    }
}