using System;
using System.Data;
using System.Data.SqlClient;

namespace Shared.Data
{
    public class DataCommandParameter
    {
        #region Properties
        private object _value;
        private ParameterDirection direction;

        private DataCommandParameter()
        {
            _value = DBNull.Value;
            direction = ParameterDirection.Input;
        }
        public ParameterDirection Direction
        {
            get { return direction; }
            set { direction = value; }
        }

        public string Name { get; set; }

        public int Size { get; set; }

        public SqlDbType SqlDbType { get; set; }

        public object Value
        {
            get { return _value; }
            set { _value = value ?? DBNull.Value; }
        }
        #endregion

        public DataCommandParameter(string name, SqlDbType type, ParameterDirection direction = ParameterDirection.Input)
        {
            _value = DBNull.Value;
            direction = ParameterDirection.Input;
            SqlParameter parameter = new SqlParameter(name, type);
            Name = parameter.ParameterName;
            SqlDbType = parameter.SqlDbType;
            Size = parameter.Size;
            Direction = direction;

        }

        public DataCommandParameter(string name, SqlDbType type, int size, ParameterDirection direction = ParameterDirection.Input)
        {
            _value = DBNull.Value;
            direction = ParameterDirection.Input;
            SqlParameter parameter = new SqlParameter(name, type, size);
            Name = parameter.ParameterName;
            SqlDbType = parameter.SqlDbType;
            Size = parameter.Size;
            Direction = direction;
        }

    }

}
