using Microsoft.Data.SqlClient;
using System.Text;

namespace G12_DataImporter;

internal class DataBaseExporter
{
    public static void ExportCatalogue()
    {
        string connectionString = "Data Source=DESKTOP-NPNIJTC;Initial Catalog=Northwind;Integrated Security=True;TrustServerCertificate=True;";
        using SqlConnection connection = new SqlConnection(connectionString);

        SqlCommand command = new SqlCommand("select  c.CategoryName, c.CategoryID, p.ProductName, p.ProductID, p.UnitPrice from Categories as c inner join Products as p on c.CategoryID = p.CategoryID", connection);
        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        using StreamWriter writer = new StreamWriter(new FileStream($"D:\\Catalogue_{DateTime.Now.ToString("yyyyMMdd")}.txt", FileMode.Create));

        while (reader.Read())
        {
            string CategoryName = reader.GetString(0);
            int CategoryID = reader.GetInt32(1);         
            string productName = reader.GetString(2);
            int ProductID = reader.GetInt32(3);
            decimal ProductPrice = reader.GetDecimal(4);
            int IsDeleted = 0;
            writer.WriteLine(CategoryName + "\t" + CategoryID + "\t" + IsDeleted + "\t" + productName + "\t" + ProductID + "\t" + ProductPrice + "\t" + IsDeleted);
        }
    }
}