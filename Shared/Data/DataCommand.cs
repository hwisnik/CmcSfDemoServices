﻿// Generated by .NET Reflector from C:\Program Files (x86)\Practical Computer Applications\PCA Components v17.1.4.4\PCA.WCF.v17.1.4.dll
namespace Shared.Data
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Linq;
   

    public class DataCommand
    {
        private DataCommandParameterCollection parameters;

        private DataCommand()
        {
        }

        public DataCommand(string procedureName)
        {
            this.ProcedureName = procedureName;
            this.parameters = new DataCommandParameterCollection();
            //this.Cache = new DataCommandCache(this);
        }

        //public DataCommand(string procedureName, Enums.CacheMode cacheMode) : this(procedureName)
        //{
        //    this.Cache.Mode = cacheMode;
        //}

        public DataCommand(string procedureName, string tableName) : this(procedureName)
        {
            this.TableNames = new string[] { tableName };
        }

        public DataCommand(string procedureName, string[] tableNames) : this(procedureName)
        {
            this.TableNames = tableNames;
        }

        //public DataCommand(string procedureName, string tableName, Enums.CacheMode cacheMode) : this(procedureName)
        //{
        //    this.TableNames = new string[] { tableName };
        //    this.Cache.Mode = cacheMode;
        //}


        //public DataCommand(string procedureName, string tableName, Enums.CacheType cacheType, Enums.CacheMode cacheMode, Enums.CacheRefresh cacheRefresh) : this(procedureName, tableName)
        //{
        //    this.Cache.Type = cacheType;
        //    this.Cache.Mode = cacheMode;
        //    this.Cache.Refresh = cacheRefresh;
        //}

        //public static SqlCommand[] ConvertToSqlCommands(DataCommand[] dataCommands)
        //{
        //    SqlCommand[] commandArray = new SqlCommand[dataCommands.Length];
        //    for (int i = 0; i < dataCommands.Length; i++)
        //    {
        //        SqlCommand command = new SqlCommand(dataCommands[i].ProcedureName)
        //        {
        //            CommandTimeout = dataCommands[i].CommandTimeout
        //        };
        //        if (dataCommands[i].Parameters != null)
        //        {
        //            foreach (SqlParameter parameter in from parameter in dataCommands[i].Parameters
        //                                               select new SqlParameter(parameter.Name, parameter.SqlDbType, parameter.Size)
        //                                               {
        //                                                   Direction = parameter.Direction,
        //                                                   Value = parameter.Value
        //                                               })
        //            {
        //                command.Parameters.Add(parameter);
        //            }
        //        }
        //        if (((dataCommands[i].Cache.Mode == Enums.CacheMode.Advanced_DataTier_HashCode) || (dataCommands[i].Cache.Mode == Enums.CacheMode.Advanced_DataTier_UpdatedOn)) || (dataCommands[i].Cache.Mode == Enums.CacheMode.Advanced_DataTier_RowVersion))
        //        {
        //            object[] commandValuesFromHashCode = GetCommandValuesFromHashCode(dataCommands[i].Cache.Mode, dataCommands[i].Cache.HashCode);
        //            switch (dataCommands[i].Cache.Mode)
        //            {
        //                case Enums.CacheMode.Advanced_DataTier_UpdatedOn:
        //                    command.Parameters.Add("@CacheRowCount", SqlDbType.Int).Value = commandValuesFromHashCode[0];
        //                    command.Parameters["@CacheRowCount"].Direction = ParameterDirection.InputOutput;
        //                    command.Parameters.Add("@CacheMaxDate", SqlDbType.DateTime).Value = commandValuesFromHashCode[1];
        //                    command.Parameters["@CacheMaxDate"].Direction = ParameterDirection.InputOutput;
        //                    break;

        //                case Enums.CacheMode.Advanced_DataTier_RowVersion:
        //                    command.Parameters.Add("@CacheRowCount", SqlDbType.Int).Value = commandValuesFromHashCode[0];
        //                    command.Parameters["@CacheRowCount"].Direction = ParameterDirection.InputOutput;
        //                    command.Parameters.Add("@CacheRowVersion", SqlDbType.Timestamp).Value = commandValuesFromHashCode[1];
        //                    command.Parameters["@CacheRowVersion"].Direction = ParameterDirection.InputOutput;
        //                    break;

        //                case Enums.CacheMode.Advanced_DataTier_HashCode:
        //                    command.Parameters.Add("@CacheHashCode", SqlDbType.NVarChar, 0x7fffffff).Value = commandValuesFromHashCode[0];
        //                    command.Parameters["@CacheHashCode"].Direction = ParameterDirection.InputOutput;
        //                    break;
        //            }
        //        }
        //        commandArray[i] = command;
        //    }
        //    return commandArray;
        //}

        //private static object[] GetCommandValuesFromHashCode(Enums.CacheMode cacheMode, string hashCode)
        //{
        //    if ((hashCode == null) || (hashCode == ""))
        //    {
        //        switch (cacheMode)
        //        {
        //            case Enums.CacheMode.Advanced_DataTier_UpdatedOn:
        //                return new object[] { -1, new DateTime(0x76c, 1, 1) };

        //            case Enums.CacheMode.Advanced_DataTier_RowVersion:
        //                return new object[] { -1, DBNull.Value };

        //            case Enums.CacheMode.Advanced_DataTier_HashCode:
        //                return new object[] { "" };
        //        }
        //    }
        //    char[] separator = new char[] { ';' };
        //    string[] strArray = hashCode.Split(separator);
        //    object[] objArray = new object[strArray.Length];
        //    try
        //    {
        //        switch (cacheMode)
        //        {
        //            case Enums.CacheMode.Advanced_DataTier_UpdatedOn:
        //                objArray[0] = int.Parse(strArray[0]);
        //                objArray[1] = new DateTime(long.Parse(strArray[1]));
        //                break;

        //            case Enums.CacheMode.Advanced_DataTier_RowVersion:
        //                objArray[0] = int.Parse(strArray[0]);
        //                objArray[1] = Encoding.Unicode.GetBytes(strArray[1]);
        //                if (((byte[])objArray[1]).Length != 8)
        //                {
        //                    throw new Exception();
        //                }
        //                break;

        //            case Enums.CacheMode.Advanced_DataTier_HashCode:
        //                objArray[0] = hashCode;
        //                break;
        //        }
        //        return objArray;
        //    }
        //    catch
        //    {
        //        return GetCommandValuesFromHashCode(cacheMode, null);
        //    }
        //}

        //public static object[] GetOutputParameterValues(DataCommand[] dataCommands, SqlCommand[] sqlCommands)
        //{
        //    List<object> list = new List<object>();
        //    for (int i = 0; i < dataCommands.Length; i++)
        //    {
        //        if (dataCommands[i].Parameters != null)
        //        {
        //            for (int j = 0; j < dataCommands[i].Parameters.Count; j++)
        //            {
        //                if (dataCommands[i].Parameters[j].Direction != ParameterDirection.Input)
        //                {
        //                    list.Add((sqlCommands[i].Parameters[j].Value == DBNull.Value) ? null : sqlCommands[i].Parameters[j].Value);
        //                }
        //            }
        //        }
        //        switch (dataCommands[i].Cache.Mode)
        //        {
        //            case Enums.CacheMode.Simple_MiddleTier_RowVersion:
        //                {
        //                    list.Add(dataCommands[i].Cache.HashCode);
        //                    if ((dataCommands[i].Cache.OptionalKeys != null) && (dataCommands[i].Cache.OptionalKeys.Length != 0))
        //                    {
        //                        break;
        //                    }
        //                    list.Add(0);
        //                    continue;
        //                }
        //            case Enums.CacheMode.Simple_MiddleTier_UpdatedOn:
        //                {
        //                    list.Add(dataCommands[i].Cache.HashCode);
        //                    continue;
        //                }
        //            case Enums.CacheMode.Advanced_DataTier_UpdatedOn:
        //                {
        //                    int num3 = (int)sqlCommands[i].Parameters["@CacheRowCount"].Value;
        //                    string item = num3.ToString() + ";";
        //                    if (sqlCommands[i].Parameters["@CacheMaxDate"].Value != DBNull.Value)
        //                    {
        //                        DateTime time = (DateTime)sqlCommands[i].Parameters["@CacheMaxDate"].Value;
        //                        item = item + time.Ticks;
        //                    }
        //                    list.Add(item);
        //                    continue;
        //                }
        //            case Enums.CacheMode.Advanced_DataTier_RowVersion:
        //                {
        //                    string item = ((int)sqlCommands[i].Parameters["@CacheRowCount"].Value).ToString() + ";";
        //                    if (sqlCommands[i].Parameters["@CacheRowVersion"].Value != DBNull.Value)
        //                    {
        //                        item = item + Convert.ToBase64String((byte[])sqlCommands[i].Parameters["@CacheRowVersion"].Value);
        //                    }
        //                    list.Add(item);
        //                    continue;
        //                }
        //            case Enums.CacheMode.Advanced_DataTier_HashCode:
        //                {
        //                    if (sqlCommands[i].Parameters["@CacheHashCode"].Value == DBNull.Value)
        //                    {
        //                        goto Label_019F;
        //                    }
        //                    list.Add(sqlCommands[i].Parameters["@CacheHashCode"].Value.ToString());
        //                    continue;
        //                }
        //            default:
        //                {
        //                    continue;
        //                }
        //        }
        //        list.Add(dataCommands[i].Cache.OptionalKeys.Length);
        //        foreach (LightObject obj2 in dataCommands[i].Cache.OptionalKeys)
        //        {
        //            list.Add(obj2.Value);
        //        }
        //        continue;
        //        Label_019F:
        //        list.Add(null);
        //    }
        //    return list.ToArray();
        //}

        public string ProcedureName { get; set; }

        public string[] TableNames { get; set; }

        //public DataCommandCache Cache { get; private set; }

        public int CommandTimeout { get; set; }

        //public bool AddKeys { get; set; }

        //private List<DataCommandParameter> ParametersList
        //{
        //    get
        //    {
        //        return this.parameters;
        //    }
        //    set
        //    {
        //        this.parameters = new DataCommandParameterCollection();
        //        this.parameters.AddRange(value);
        //    }
        //}

        public string TableName
        {
            get
            {
                if ((this.TableNames == null) || (this.TableNames.Length == 0))
                {
                    return null;
                }
                if (this.TableNames.Length > 1)
                {
                    throw new ArgumentException("The command has multiple table names specified.  Please use 'TableNames' property.", "TableName");
                }
                return this.TableNames[0];
            }
            set
            {
                if ((this.TableNames != null) && (this.TableNames.Length > 1))
                {
                    throw new ArgumentException("The command already has mulitple table names specified.  Please use 'TableNames' property.", "TableName");
                }
                this.TableNames = new string[] { value };
            }
        }


        public DataCommandParameterCollection Parameters
        {
            get
            {
                return this.parameters;
            }
            set
            {
                this.parameters = value;
            }
        }

    }
}

