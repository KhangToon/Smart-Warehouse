using Microsoft.Data.SqlClient;
using Smart_Warehouse.Commons;
using Smart_Warehouse.Models;
using Smart_Warehouse.Models.SANPHAM;
using System.Linq.Expressions;

namespace Smart_Warehouse.Services.SQLService
{
    public class SQLServerServices
    {
        private static string? connectionString;

        public ISQLService<VTriLuuKho> _VTLKSQL;
        public ISQLService<Pallet> _PALLETSQL;
        public ISQLService<ITem> _ITEMSQL;

        public SQLServerServices()
        {
            var configuration = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json")
                                    .Build();

            string? configConstring = configuration["ConnectionStrings:DBConnectionstring"];

            connectionString = configConstring;

            _VTLKSQL = new VTLKSQL();
            _PALLETSQL = new PalletSQL();
            _ITEMSQL = new ItemSQL();
        }


        // Table_VTriLuuKho------------------------------------------------------------------------------------- //

        #region Table_VTriLuuKho
        public class VTLKSQL : ISQLService<VTriLuuKho>
        {
            public string ConnectionString { get => connectionString ?? string.Empty; set => _ = connectionString; }
            public string TargetTable { get => VTriLuuKho.DBName.Table_VTriLuuKho; set => _ = VTriLuuKho.DBName.Table_VTriLuuKho; }
            public string TargetIDName { get => VTriLuuKho.DBName.VTLKID; set => _ = VTriLuuKho.DBName.VTLKID; }

            public Task<VTriLuuKho> GetByAsync(Expression<Func<VTriLuuKho, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public async Task<(List<VTriLuuKho> items, string errorMessage)> GetListsAsync(Dictionary<string, object?> parameters, bool isGetAll = false)
            {
                List<VTriLuuKho> listTargetItems = new();

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
                            VTriLuuKho targetItem = new();

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

                            targetItem.TargetPallet = await Pallet.PalLetSQL.GetByIDAsync(targetItem.PLID.Value);

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


        }
        #endregion

        // Table_Pallet------------------------------------------------------------------------------------- //

        #region Table_Pallet
        public class PalletSQL : ISQLService<Pallet>
        {
            public string ConnectionString { get => connectionString ?? string.Empty; set => _ = connectionString; }
            public string TargetTable { get => Pallet.DBName.Table_Pallet; set => _ = Pallet.DBName.Table_Pallet; }
            public string TargetIDName { get => Pallet.DBName.PLID; set => _ = Pallet.DBName.PLID; }

            public Task<Pallet> GetByAsync(Expression<Func<Pallet, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public async Task<(List<Pallet> items, string errorMessage)> GetListsAsync(Dictionary<string, object?> parameters, bool isGetAll = false)
            {
                List<Pallet> listTargetItems = new();

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
                            Pallet targetItem = new();

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

                            Dictionary<string, object?> parametersITem = new() { { ITem.DBName.PLID, targetItem.PLID.Value }, { ITem.DBName.VTLKID, targetItem.VTLKID.Value } };

                            targetItem.ITems = (await ITem.ItemSQL.GetListsAsync(parametersITem)).items;

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
        }
        #endregion

        // Table_Item------------------------------------------------------------------------------------- //

        #region Table_Item
        public class ItemSQL : ISQLService<ITem>
        {
            public string ConnectionString { get => connectionString ?? string.Empty; set => _ = connectionString; }
            public string TargetTable { get => ITem.DBName.Table_Item; set => _ = ITem.DBName.Table_Item; }
            public string TargetIDName { get => ITem.DBName.ITID; set => _ = ITem.DBName.ITID; }

            public Task<ITem> GetByAsync(Expression<Func<ITem, bool>> predicate)
            {
                throw new NotImplementedException();
            }

            public async Task<(List<ITem> items, string errorMessage)> GetListsAsync(Dictionary<string, object?> parameters, bool isGetAll = false)
            {
                List<ITem> listTargetItems = new();

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
                            ITem targetItem = new();

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

                            // Get TEnSanPham and MaSanPham from SPID for ITem
                            targetItem.TenSanPham = SanPham.SPSQL.GetColValue(SanPham.DBName.TENSANPHAM, targetItem.SPID.Value);
                            targetItem.MaSanPham = SanPham.SPSQL.GetColValue(SanPham.DBName.MASANPHAM, targetItem.SPID.Value);

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

        }
        #endregion 

        // Table_SanPham------------------------------------------------------------------------------------- //

        #region Table_SanPham
        public class SanPhamSQL : ISQLService<SanPham>
        {
            public string ConnectionString { get => connectionString ?? string.Empty; set => _ = connectionString; }
            public string TargetTable { get => SanPham.DBName.Table_SanPham; set => _ = SanPham.DBName.Table_SanPham; }
            public string TargetIDName { get => SanPham.DBName.SPID; set => _ = SanPham.DBName.SPID; }

            public Task<SanPham> GetByAsync(Expression<Func<SanPham, bool>> predicate)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        // Table_LoaiTTSP------------------------------------------------------------------------------------- //

        #region Table_LoaiTTSP
        public class LoaiTTSPSQL : ISQLService<LoaiThongTinSanPham>
        {
            public string ConnectionString { get => connectionString ?? string.Empty; set => _ = connectionString; }
            public string TargetTable { get => LoaiThongTinSanPham.DBName.Table_LoaiTTSP; set => _ = LoaiThongTinSanPham.DBName.Table_LoaiTTSP; }
            public string TargetIDName { get => LoaiThongTinSanPham.DBName.LoaiTTSPID; set => _ = LoaiThongTinSanPham.DBName.LoaiTTSPID; }

            public Task<LoaiThongTinSanPham> GetByAsync(Expression<Func<LoaiThongTinSanPham, bool>> predicate)
            {
                throw new NotImplementedException();
            }
        }
        #endregion

        // Table_TTSanPham------------------------------------------------------------------------------------- //

        #region Table_TTSanPham
        public class TTSPhamSQL : ISQLService<ThongTinSanPham>
        {
            public string ConnectionString { get => connectionString ?? string.Empty; set => _ = connectionString; }
            public string TargetTable { get => ThongTinSanPham.DBName.Table_TTSanPham; set => _ = ThongTinSanPham.DBName.Table_TTSanPham; }
            public string TargetIDName { get => ThongTinSanPham.DBName.TTSPID; set => _ = ThongTinSanPham.DBName.TTSPID; }

            public Task<ThongTinSanPham> GetByAsync(Expression<Func<ThongTinSanPham, bool>> predicate)
            {
                throw new NotImplementedException();
            }
        }
        #endregion
    }
}

// Handle overide default method of InterFace