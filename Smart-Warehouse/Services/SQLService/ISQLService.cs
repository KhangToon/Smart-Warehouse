using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Smart_Warehouse.Commons;
using Smart_Warehouse.Models.Interface;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Smart_Warehouse.Services.SQLService
{
    public interface ISQLService<T> where T : IPropertyHandler
    {
        string ConnectionString { get; set; }
        string TargetTable { get; set; }
        string TargetIDName { get; set; }

        //public (int result, string errorMessage) Insert(T item)
        //{
        //    if (string.IsNullOrEmpty(TargetIDName))
        //    {
        //        return (-1, "Error");
        //    }

        //    int result = -1;
        //    string errorMess = string.Empty;

        //    if (item == null) return (result, "Error: Item is null");

        //    List<Propertyy> properties = item.GetPropertiesValues()
        //        .Where(po => po.AlowDatabase == true && po.Value != null)
        //        .ToList();

        //    if (properties.Count == 0)
        //    {
        //        return (result, "Error: No valid properties to insert.");
        //    }

        //    using var connection = new SqlConnection(ConnectionString);
        //    connection.Open();
        //    using var transaction = connection.BeginTransaction();

        //    try
        //    {
        //        var command = connection.CreateCommand();
        //        command.Transaction = transaction;

        //        string columns = string.Join(", ", properties.Select(p => $"[{p.DBName}]"));
        //        string parameters = string.Join(", ", properties.Select(p => $"@{Regex.Replace(p.DBName ?? string.Empty, @"[^\w]+", "")}"));
        //        command.CommandText = $@"INSERT INTO [{TargetTable}] ({columns}) OUTPUT INSERTED.{TargetIDName} VALUES ({parameters})";

        //        foreach (var prop in properties)
        //        {
        //            string parameterName = $"@{Regex.Replace(prop.DBName ?? string.Empty, @"[^\w]+", "")}";
        //            object? parameterValue = prop.Value ?? DBNull.Value;
        //            command.Parameters.AddWithValue(parameterName, parameterValue);
        //        }

        //        object? rs = command.ExecuteScalar();
        //        if (rs != null && int.TryParse(rs.ToString(), out result) && result > 0)
        //        {
        //            transaction.Commit();
        //        }
        //        else
        //        {
        //            result = -1;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorMess = $"Error: {ex.Message}";
        //        try
        //        {
        //            transaction.Rollback();
        //        }
        //        catch (Exception rollbackEx)
        //        {
        //            errorMess += $" | Rollback Error: {rollbackEx.Message}";
        //        }
        //        return (-1, errorMess);
        //    }

        //    return (result, errorMess);
        //}

        //public (int result, string errorMessage) Update(T item, object? idValue)
        //{
        //    int result = -1;
        //    string errorMess = string.Empty;

        //    // Check for null input
        //    if (item == null || string.IsNullOrEmpty(TargetIDName) || idValue == null) return (result, "Error: is null");

        //    List<Propertyy> properties = item.GetPropertiesValues()
        //        .Where(po => po.AlowDatabase == true && po.Value != null)
        //        .ToList();

        //    // Validate properties before proceeding
        //    if (properties.Count == 0)
        //    {
        //        return (result, "Error: No valid properties to update.");
        //    }

        //    using var connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    using var transaction = connection.BeginTransaction(); // Start transaction

        //    try
        //    {
        //        var command = connection.CreateCommand();
        //        command.Transaction = transaction; // Associate command with the transaction

        //        string updateSet = string.Join(", ", properties.Select(p => $"[{p.DBName}] = @{Regex.Replace(p.DBName ?? string.Empty, @"[^\w]+", "")}"));

        //        command.CommandText = $@"UPDATE [{TargetTable}] SET {updateSet} WHERE [{TargetIDName}] = '{idValue}'";

        //        // Add parameters
        //        foreach (var prop in properties)
        //        {
        //            string parameterName = $"@{Regex.Replace(prop.DBName ?? string.Empty, @"[^\w]+", "")}";
        //            object? parameterValue = prop.Value ?? DBNull.Value;
        //            command.Parameters.AddWithValue(parameterName, parameterValue);
        //        }

        //        // Execute command
        //        result = command.ExecuteNonQuery();

        //        if (result > 0)
        //        {
        //            // Successfully updated
        //            transaction.Commit(); // Commit the transaction
        //        }
        //        else
        //        {
        //            result = -1; // Set to -1 if update was not successful
        //            errorMess = "No rows were updated. The specified may not exist.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        errorMess = $"Error: {ex.Message}";
        //        try
        //        {
        //            transaction.Rollback(); // Rollback transaction in case of error
        //        }
        //        catch (Exception rollbackEx)
        //        {
        //            errorMess += $" | Rollback Error: {rollbackEx.Message}";
        //        }
        //        return (-1, errorMess);
        //    }

        //    return (result, errorMess);
        //}

        //public (bool result, string errorMessage) Delete(object? idValue)
        //{
        //    // Check for valid ID
        //    if (idValue == null || string.IsNullOrEmpty(TargetIDName))
        //    {
        //        return (false, "Error");
        //    }

        //    using var connection = new SqlConnection(ConnectionString);
        //    connection.Open();

        //    string query = $"DELETE FROM [{TargetTable}] WHERE [{TargetIDName}] = @ID";

        //    using var command = new SqlCommand(query, connection);
        //    command.Parameters.AddWithValue("@ID", idValue);

        //    try
        //    {
        //        int rowsAffected = command.ExecuteNonQuery();
        //        return (rowsAffected > 0, string.Empty); // Return true if a row was deleted
        //    }
        //    catch (Exception ex)
        //    {
        //        return (false, $"Error: {ex.Message}"); // Return false and the error message
        //    }
        //}

        //public (List<T> items, string errorMessage) GetLists(Dictionary<string, object?> parameters, bool isGetAll = false)
        //{
        //    List<T> listTargeItems = new();

        //    if (string.IsNullOrEmpty(TargetTable))
        //    {
        //        return (listTargeItems, "Error");
        //    }

        //    string errorMessage = string.Empty;

        //    using (var connection = new SqlConnection(ConnectionString))
        //    {
        //        try
        //        {
        //            connection.Open();

        //            var conditions = new List<string>();
        //            var command = connection.CreateCommand();
        //            command.CommandText = $"SELECT * FROM [{TargetTable}]";

        //            if (!isGetAll)
        //            {
        //                // Process each parameter in the dictionary
        //                foreach (var param in parameters)
        //                {
        //                    conditions.Add($"[{param.Key}] = @{param.Key}");

        //                    command.Parameters.AddWithValue($"@{param.Key}", param.Value);
        //                }

        //                if (conditions.Any())
        //                {
        //                    command.CommandText += " WHERE " + string.Join(" AND ", conditions);
        //                }
        //            }

        //            using var reader = command.ExecuteReader();

        //            while (reader.Read())
        //            {
        //                T targetItem = Activator.CreateInstance<T>();

        //                List<Propertyy> rowItems = targetItem.GetPropertiesValues();

        //                foreach (var item in rowItems)
        //                {
        //                    string? columnName = item.DBName;

        //                    if (!string.IsNullOrEmpty(columnName) && reader.GetOrdinal(columnName) != -1)
        //                    {
        //                        object columnValue = reader[columnName];

        //                        item.Value = columnValue == DBNull.Value ? null : columnValue;
        //                    }
        //                }

        //                listTargeItems.Add(targetItem);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            errorMessage = $"Error: {ex.Message}";
        //            listTargeItems.Clear(); // Clear the list in case of error
        //        }
        //    }
        //    return (listTargeItems, errorMessage);

        //}

        public object? GetColValue(string colName, object? idValue)
        {
            if (string.IsNullOrEmpty(colName) || string.IsNullOrEmpty(TargetIDName) || idValue == null) return null;

            var results = GetColValues(new() { { TargetIDName, idValue } }, colName).columnValues.FirstOrDefault();

            return results;
        }

        public (List<object?> columnValues, string errorMessage) GetColValues(Dictionary<string, object?> whereParameters, string? returnColumnName = null, bool isGetAll = false)
        {
            List<object?> columnValues = new();
            string errorMessage = string.Empty;

            if (string.IsNullOrEmpty(TargetTable))
            {
                return (columnValues, "Error");
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    var conditions = new List<string>();
                    var command = connection.CreateCommand();

                    // If a specific column is requested, select only that column
                    string selectClause = returnColumnName != null
                        ? $"SELECT [{returnColumnName}]"
                        : "SELECT *";

                    command.CommandText = $"{selectClause} FROM [{TargetTable}]";

                    if (!isGetAll)
                    {
                        // Process each parameter in the dictionary
                        foreach (var param in whereParameters)
                        {
                            conditions.Add($"[{param.Key}] = @{param.Key}");
                            command.Parameters.AddWithValue($"@{param.Key}", param.Value);
                        }
                        if (conditions.Any())
                        {
                            command.CommandText += " WHERE " + string.Join(" AND ", conditions);
                        }
                    }

                    using var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        // If no specific column is requested, read entire object
                        if (returnColumnName == null)
                        {
                            T targetItem = Activator.CreateInstance<T>();

                            List<Propertyy> rowItems = targetItem.GetPropertiesValues();

                            foreach (var item in rowItems)
                            {
                                string? columnName = item.DBName;
                                if (!string.IsNullOrEmpty(columnName) && reader.GetOrdinal(columnName) != -1)
                                {
                                    object columnValue = reader[columnName];
                                    item.Value = columnValue == DBNull.Value ? null : columnValue;
                                    columnValues.Add(item.Value);
                                }
                            }
                        }
                        else
                        {
                            // If a specific column is requested, read only that column
                            object columnValue = reader[returnColumnName];
                            columnValues.Add(columnValue == DBNull.Value ? null : columnValue);
                        }
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Error: {ex.Message}";
                    columnValues.Clear(); // Clear the list in case of error
                }
            }
            return (columnValues, errorMessage);
        }




        public async Task<(int result, string errorMessage)> InsertAsync(T item)
        {
            if (string.IsNullOrEmpty(TargetIDName))
            {
                return (-1, "Error: TargetIDName is not specified.");
            }

            int result = -1;
            string errorMess = string.Empty;

            if (item == null)
            {
                return (result, "Error: Item is null.");
            }

            List<Propertyy> properties = item.GetPropertiesValues()
                .Where(po => po.AlowDatabase == true && po.Value != null)
                .ToList();

            if (properties.Count == 0)
            {
                return (result, "Error: No valid properties to insert.");
            }

            using var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync();

            // Cast the DbTransaction to SqlTransaction
            using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {
                var command = connection.CreateCommand();
                command.Transaction = transaction;

                string columns = string.Join(", ", properties.Select(p => $"[{p.DBName}]"));
                string parameters = string.Join(", ", properties.Select(p => $"@{Regex.Replace(p.DBName ?? string.Empty, @"[^\w]+", "")}"));
                command.CommandText = $@"INSERT INTO [{TargetTable}] ({columns}) OUTPUT INSERTED.{TargetIDName} VALUES ({parameters})";

                foreach (var prop in properties)
                {
                    string parameterName = $"@{Regex.Replace(prop.DBName ?? string.Empty, @"[^\w]+", "")}";
                    object? parameterValue = prop.Value ?? DBNull.Value;
                    command.Parameters.AddWithValue(parameterName, parameterValue);
                }

                object? rs = await command.ExecuteScalarAsync();
                if (rs != null && int.TryParse(rs.ToString(), out result) && result > 0)
                {
                    await transaction.CommitAsync();
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception ex)
            {
                errorMess = $"Error: {ex.Message}";
                try
                {
                    await transaction.RollbackAsync();
                }
                catch (Exception rollbackEx)
                {
                    errorMess += $" | Rollback Error: {rollbackEx.Message}";
                }
                return (-1, errorMess);
            }

            return (result, errorMess);
        }

        public async Task<(int result, string errorMessage)> UpdateAsync(T item, object? idValue)
        {
            int result = -1;
            string errorMess = string.Empty;

            // Check for null input
            if (item == null || string.IsNullOrEmpty(TargetIDName) || idValue == null)
            {
                return (result, "Error: Item, TargetIDName, or idValue is null.");
            }

            List<Propertyy> properties = item.GetPropertiesValues()
                .Where(po => po.AlowDatabase == true && po.Value != null)
                .ToList();

            // Validate properties before proceeding
            if (properties.Count == 0)
            {
                return (result, "Error: No valid properties to update.");
            }

            using var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync(); // Open connection asynchronously

            // Start transaction asynchronously and cast it to SqlTransaction
            using var transaction = (SqlTransaction)await connection.BeginTransactionAsync();

            try
            {
                var command = connection.CreateCommand();
                command.Transaction = transaction; // Associate command with the transaction

                // Build the SQL update query dynamically
                string updateSet = string.Join(", ", properties.Select(p => $"[{p.DBName}] = @{Regex.Replace(p.DBName ?? string.Empty, @"[^\w]+", "")}"));

                command.CommandText = $@"UPDATE [{TargetTable}] SET {updateSet} WHERE [{TargetIDName}] = @idValue";

                // Add update parameters
                foreach (var prop in properties)
                {
                    string parameterName = $"@{Regex.Replace(prop.DBName ?? string.Empty, @"[^\w]+", "")}";
                    object? parameterValue = prop.Value ?? DBNull.Value;
                    command.Parameters.AddWithValue(parameterName, parameterValue);
                }

                // Add the ID parameter
                command.Parameters.AddWithValue("@idValue", idValue);

                // Execute the command asynchronously
                result = await command.ExecuteNonQueryAsync();

                if (result > 0)
                {
                    // Successfully updated
                    await transaction.CommitAsync(); // Commit the transaction asynchronously
                }
                else
                {
                    result = -1; // Set to -1 if update was not successful
                    errorMess = "No rows were updated. The specified item may not exist.";
                }
            }
            catch (Exception ex)
            {
                errorMess = $"Error: {ex.Message}";
                try
                {
                    await transaction.RollbackAsync(); // Rollback transaction asynchronously in case of an error
                }
                catch (Exception rollbackEx)
                {
                    errorMess += $" | Rollback Error: {rollbackEx.Message}";
                }
                return (-1, errorMess);
            }

            return (result, errorMess);
        }

        public async Task<(bool result, string errorMessage)> DeleteAsync(object? idValue)
        {
            // Check for valid ID
            if (idValue == null || string.IsNullOrEmpty(TargetIDName))
            {
                return (false, "Error: ID value or TargetIDName is null or empty.");
            }

            using var connection = new SqlConnection(ConnectionString);
            await connection.OpenAsync(); // Open connection asynchronously

            string query = $"DELETE FROM [{TargetTable}] WHERE [{TargetIDName}] = @ID";

            using var command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ID", idValue);

            try
            {
                int rowsAffected = await command.ExecuteNonQueryAsync(); // Execute the command asynchronously
                return (rowsAffected > 0, string.Empty); // Return true if a row was deleted
            }
            catch (Exception ex)
            {
                return (false, $"Error: {ex.Message}"); // Return false and the error message
            }
        }

        public async Task<(List<T> items, string errorMessage)> GetListsAsync(Dictionary<string, object?> parameters, bool isGetAll = false)
        {
            List<T> listTargetItems = new();

            if (string.IsNullOrEmpty(TargetTable))
            {
                return (listTargetItems, "Error: TargetTable is not specified");
            }

            string errorMessage = string.Empty;

            using (var connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    await connection.OpenAsync();

                    var conditions = new List<string>();
                    var command = connection.CreateCommand();
                    command.CommandText = $"SELECT * FROM [{TargetTable}]";

                    if (!isGetAll)
                    {
                        // Process each parameter in the dictionary
                        foreach (var param in parameters)
                        {
                            conditions.Add($"[{param.Key}] = @{param.Key}");

                            // Use AddWithValue for simplicity, but it's better to use Add and specify the type in production.
                            command.Parameters.AddWithValue($"@{param.Key}", param.Value ?? DBNull.Value);
                        }

                        if (conditions.Any())
                        {
                            command.CommandText += " WHERE " + string.Join(" AND ", conditions);
                        }
                    }

                    using var reader = await command.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        T targetItem = Activator.CreateInstance<T>();

                        List<Propertyy> rowItems = targetItem.GetPropertiesValues();

                        foreach (var item in rowItems)
                        {
                            string? columnName = item.DBName;

                            if (!string.IsNullOrEmpty(columnName) && reader.GetOrdinal(columnName) != -1)
                            {
                                object columnValue = reader[columnName];
                                item.Value = columnValue == DBNull.Value ? null : columnValue;
                            }
                        }

                        listTargetItems.Add(targetItem);
                    }
                }
                catch (Exception ex)
                {
                    errorMessage = $"Error: {ex.Message}";
                    listTargetItems.Clear(); // Clear the list in case of error
                }
            }
            return (listTargetItems, errorMessage);
        }

        Task<T> GetByAsync(Expression<Func<T, bool>> predicate);

        async Task<IEnumerable<T>> FilterByListAsync(IEnumerable<T> data, Expression<Func<T, bool>> predicate)
        {
            // Convert the list to IQueryable for using the LINQ methods
            var queryableData = data.AsQueryable();

            // Apply the filter using the predicate
            var filteredData = await Task.Run(() => queryableData.Where(predicate).ToList());

            return filteredData;
        }

        async Task<T?> GetByIDAsync(object? idValue)
        {
            if (string.IsNullOrEmpty(this.TargetIDName) || idValue == null)
            {
                return default;
            }
            var results = (await GetListsAsync(new() { { this.TargetIDName, idValue } })).items;

            if (results != null && results.Any())
            {
                return results.FirstOrDefault();
            }
            else
            {
                return default;
            }
        }

        public bool IsValueExisting(object? proValue, object proName)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                string query = $"SELECT COUNT(*) FROM [{TargetTable}] WHERE [{proName}] = N'{proValue}'";

                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();

                    int count = (int)command.ExecuteScalar();

                    return count > 0;
                }
            }
        }
    }
}
