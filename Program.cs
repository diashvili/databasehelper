namespace G12_DataImporter
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string filePath = @"D:\Catalogue_20230331.txt";

            DataBaseExporter.ExportCatalogue();
            var catalogue = FileDataLoader.GetCategories(filePath);
			DatabaseImporter.ImportCatalogue(catalogue);
            //foreach (var category in catalogue)
            //{
            //	Console.WriteLine(category.CategoryName + " " + category.CategoryCode + " " + category.IsDeleted);

            //	foreach (var item in category.Products)
            //	{
            //		Console.WriteLine(item.ProductName + " " + item.ProductCode + " " + item.Price + " " + item.IsDeleted);
            //	}
            //}
		}
	}
}