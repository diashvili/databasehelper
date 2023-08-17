using Microsoft.Data.SqlClient;
using System.Data;

namespace G12_DataImporter;

public static class DatabaseImporter
{
    private const string ConnectionString = @"Data Source=DESKTOP-NPNIJTC;Database=market;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

    public static void ImportCatalogue(IEnumerable<Category> catalogue)
    {
        using SqlConnection connection = new(ConnectionString);
        using SqlCommand command = new SqlCommand("ImportData_sp", connection);
        command.CommandType = CommandType.StoredProcedure;
        AddParameters(command);

        connection.Open();
        var transaction = connection.BeginTransaction();
        command.Transaction = transaction;

        try
        {
            foreach (var category in catalogue)
            {
                command.Parameters["@CategoryName"].Value = category.CategoryName;
                command.Parameters["@CategoryCode"].Value = category.CategoryCode;
                command.Parameters["@CategoryIsDeleted"].Value = category.IsDeleted;

                foreach (var product in category.Products)
                {
                    command.Parameters["@ProductName"].Value = product.ProductName;
                    command.Parameters["@ProductCode"].Value = product.ProductCode;
                    command.Parameters["@ProductPrice"].Value = product.Price;
                    command.Parameters["@ProductIsDeleted"].Value = product.IsDeleted;
                    command.ExecuteNonQuery();
                }
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    private static void AddParameters(SqlCommand command)
    {
        command.Parameters.Add("@CategoryName", SqlDbType.NVarChar);
        command.Parameters.Add("@CategoryCode", SqlDbType.VarChar);
        command.Parameters.Add("@CategoryIsDeleted", SqlDbType.Bit);

        command.Parameters.Add("@ProductName", SqlDbType.NVarChar);
        command.Parameters.Add("@ProductCode", SqlDbType.VarChar);
        command.Parameters.Add("@ProductPrice", SqlDbType.Money);
        command.Parameters.Add("@ProductIsDeleted", SqlDbType.Bit);
    }
}
