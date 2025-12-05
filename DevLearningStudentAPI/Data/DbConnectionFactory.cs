using Microsoft.Data.SqlClient;

namespace DevLearningStudentAPI.Data;

public class DbConnectionFactory
{
	private readonly string _connectionString;

	public DbConnectionFactory(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection");
	}

	public SqlConnection GetConnection()
	{
		return new SqlConnection(_connectionString);
	}
}
