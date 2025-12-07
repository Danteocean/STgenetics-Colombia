using Dapper;
using Domain.Entities;
using Npgsql;

namespace Domain.Querys;

public class GetExtras
{
    private String _conexion = "";

    private const String query = @"SELECT extra_id extraId, category_id categoryId, name nameExt, price priceExt,is_active isActive FROM extra WHERE is_active = true";
    private const String queryById = @"SELECT extra_id extraId, category_id categoryId, name nameExt, price priceExt, is_active isActive FROM extra
                         WHERE extra_id = ANY(@ids) AND is_active = true";

    public GetExtras(String conexion)
    {
        _conexion = conexion;
    }

    public async Task<List<ExtrasDtoResponse>> Get()
    {
        try
        {
            using var dbConnection = new NpgsqlConnection(_conexion);
            var result = await dbConnection.QueryAsync<ExtrasDtoResponse>(query);
            return result.ToList();
        }
        catch(Exception ex ) 
        {
            Console.WriteLine("Error GetExtras: " + ex.Message);
            return new List<ExtrasDtoResponse>();
        }
    }

    public async Task<List<Extras>> GetByIds(List<int> ids)
    {
        try
        {
            if (ids == null || ids.Count == 0) return new List<Extras>();

            using var db = new NpgsqlConnection(_conexion);
            var rows = await db.QueryAsync<Extras>(queryById, new { ids = ids.ToArray() });
            return rows.ToList();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error GetExtrasByIds: " + ex.Message);
            return new List<Extras>();
        }
       
    }
}