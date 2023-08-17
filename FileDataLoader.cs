namespace G12_DataImporter;

public static class FileDataLoader
{
    private const char Separator = '\t';

    public static IEnumerable<Category> GetCategories(string filePath)
    {
        if (filePath == null) throw new ArgumentNullException(nameof(filePath));
        if (!File.Exists(filePath)) throw new FileNotFoundException(filePath);

        string[] data = File.ReadAllLines(filePath);
        ValidateData(data);

        HashSet<Category> categories = new();
        foreach (var line in data)
        {
            Category category = 
                GetCategoryByCode(Category.GetCodeFromString(line), categories) ??
                Category.CreateFromString(line);
            
            category.Products.Add(Product.CreateFromString(line));
            categories.Add(category);
        }
        
        return categories;
    }

    private static Category? GetCategoryByCode(string code, IEnumerable<Category> categories)
    {
        foreach (var category in categories)
        {
            if (category.CategoryCode == code)
            {
                return category;
            }
        }

        return null;
    }

    private static void ValidateData(string[] data)
    {
        List<Exception> errors = new();

        int counter = 0;
        foreach (var line in data)
        {
            counter++;
            string[] columns = line.Split('\t');
            if (columns.Length != 7)
            {
                errors.Add(new FormatException($"Incorrect format line {counter}"));
                continue;
            }

            if (columns[2] != "0" && columns[2] != "1")
            {
                errors.Add(new ArgumentException($"Wrong category IsDeleted on line {counter}"));
            }
            if (columns[6] != "0" && columns[6] != "1")
            {
                errors.Add(new ArgumentException($"Wrong product IsDeleted on line {counter}"));
            }
            if (!decimal.TryParse(columns[5], out var price))
            {
                errors.Add(new ArgumentException($"Wrong product Price on line {counter}"));
            }
        }

        if (errors.Count > 0)
        {
            throw new AggregateException(errors);
        }
    }
}