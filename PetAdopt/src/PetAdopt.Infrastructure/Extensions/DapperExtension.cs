namespace PetAdopt.Infrastructure.Extensions;

//Métodos Base Genéricos em Dapper para uso futuro
public class DapperExtension<T> where T : class
{
    private readonly string _connectionString;

    public DapperExtension(string connectionString) => _connectionString = connectionString;

    private IDbConnection GetConnection() => new SqlConnection(_connectionString);

    public IEnumerable<T> GetAll()
    {
        using (IDbConnection dbConnection = GetConnection())
        {
            string selectQuery = $"SELECT * FROM {typeof(T).Name}s";
            dbConnection.Open();
            return dbConnection.Query<T>(selectQuery);
        }
    }

    public T GetById(int id)
    {
        using (IDbConnection dbConnection = GetConnection())
        {
            string selectQuery = $"SELECT * FROM {typeof(T).Name}s WHERE Id = @Id";
            dbConnection.Open();
            return dbConnection.Query<T>(selectQuery, new { Id = id }).FirstOrDefault();
        }
    }
}
