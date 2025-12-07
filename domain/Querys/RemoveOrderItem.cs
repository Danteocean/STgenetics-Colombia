using Dapper;

namespace Domain.Querys;

public class RemoveOrderItem
{
    private String _conexion = "";

    public const String query = @"DELETE FROM order_item WHERE order_id = @id";

    public RemoveOrderItem(String conexion)
    {
        _conexion = conexion;
    }

    public async Task<Boolean> Remove(int id)
    {
        try
        {
            using var dbConnection = new Npgsql.NpgsqlConnection(_conexion);
            var parameters = new { id };
            var result = await dbConnection.ExecuteAsync(query, parameters);
            if (result != 0)
            {
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error RemoveOrderItem: " + ex.Message);
            return false;
        }
    }
}