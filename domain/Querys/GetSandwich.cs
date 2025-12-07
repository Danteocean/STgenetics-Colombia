using Dapper;
using Domain.Entities;
using Npgsql;

namespace Domain.Querys;

public class GetSandwich
{
    private String _conexion = "";

    public const String query = @"SELECT sandwich_id sandwichId, name, price, is_active isActive FROM sandwich";

    public const String queryById = @"SELECT sandwich_id  sandwichId, name, price , is_active isActive FROM sandwich 
    WHERE sandwich_id = @id AND is_active = true";

    public GetSandwich(String conexion)
    {
        _conexion = conexion;
    }

    public async Task<List<Sandwich>> Get()
    {
        try
        {
            using var dbConnection = new NpgsqlConnection(_conexion);
            var result = await dbConnection.QueryAsync<Sandwich>(query);
            return result.ToList();
        }
        catch(Exception ex)
        {
            Console.WriteLine("Error GetSandwich:"+ex.Message);
            return new List<Sandwich>();
        }
    }


    public async Task<Sandwich> GetById(Int32 id)
    {
        try
        {
            using var dbConnection = new NpgsqlConnection(_conexion);
            var parameters = new { id };
            return await dbConnection.QueryFirstOrDefaultAsync<Sandwich>(queryById, parameters);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error GetSandwichById: " + ex.Message);
            return new Sandwich();
        }
    }

}