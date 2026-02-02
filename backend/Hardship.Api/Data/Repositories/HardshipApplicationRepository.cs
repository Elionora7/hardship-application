using Dapper;
using Hardship.Api.Models.Domain;

namespace Hardship.Api.Data.Repositories;

public class HardshipApplicationRepository
{
    private readonly DbConnectionFactory _factory;

    public HardshipApplicationRepository(DbConnectionFactory factory)
    {
        _factory = factory;
    }

    public async Task<int> CreateAsync(HardshipApplication application)
    {
        using var connection = _factory.CreateConnection();
        connection.Open();

        var sql = @"
            INSERT INTO hardship_applications
                (customer_name, date_of_birth, income, expenses, hardship_reason, created_at, updated_at)
            VALUES
                (@CustomerName, @DateOfBirth, @Income, @Expenses, @HardshipReason, @CreatedAt, @UpdatedAt);
            SELECT last_insert_rowid();";

        return await connection.ExecuteScalarAsync<int>(sql, application);
    }

    public async Task UpdateAsync(HardshipApplication application)
    {
        using var connection = _factory.CreateConnection();
        connection.Open();

        var sql = @"
            UPDATE hardship_applications
            SET customer_name = @CustomerName,
                date_of_birth = @DateOfBirth,
                income = @Income,
                expenses = @Expenses,
                hardship_reason = @HardshipReason,
                updated_at = @UpdatedAt
            WHERE id = @Id;";

        await connection.ExecuteAsync(sql, application);
    }

    public async Task<IEnumerable<HardshipApplication>> GetAllAsync()
    {
        using var connection = _factory.CreateConnection();
        connection.Open();

        // Alias snake_case columns to PascalCase for Dapper mapping
        var sql = @"
            SELECT 
                id AS Id,
                customer_name AS CustomerName,
                date_of_birth AS DateOfBirth,
                income AS Income,
                expenses AS Expenses,
                hardship_reason AS HardshipReason,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM hardship_applications
            ORDER BY created_at DESC;";

        return await connection.QueryAsync<HardshipApplication>(sql);
    }

    public async Task<HardshipApplication?> GetByIdAsync(int id)
    {
        using var connection = _factory.CreateConnection();
        connection.Open();

        var sql = @"
            SELECT 
                id AS Id,
                customer_name AS CustomerName,
                date_of_birth AS DateOfBirth,
                income AS Income,
                expenses AS Expenses,
                hardship_reason AS HardshipReason,
                created_at AS CreatedAt,
                updated_at AS UpdatedAt
            FROM hardship_applications
            WHERE id = @Id;";

        return await connection.QueryFirstOrDefaultAsync<HardshipApplication>(sql, new { Id = id });
    }

    public async Task<bool> DeleteAsync(int id)
    {
        const string sql = @"
            DELETE FROM hardship_applications
            WHERE id = @Id;";

        using var connection = _factory.CreateConnection();
        var affectedRows = await connection.ExecuteAsync(sql, new { Id = id });

        return affectedRows > 0;
    }
}
