using Microsoft.Data.SqlClient;

namespace DevLearningAuthorAPI.Data;

public class DbConnectionFactory
{
	private readonly string _connectionStrin;

	public DbConnectionFactory(IConfiguration configuration)
	{
		_connectionString = configuration.GetConnectionString("DefaultConnection");
	}

	public SqlConnection GetConnection()
	{
		return new SqlConnection(_connectionStrin);
	}
}
