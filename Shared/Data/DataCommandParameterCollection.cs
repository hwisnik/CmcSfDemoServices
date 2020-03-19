using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Shared.Data
{
    public class DataCommandParameterCollection : List<DataCommandParameter>
    {
        public new int Add(DataCommandParameter parameter)
        {
            base.Add(parameter);
            return base.IndexOf(parameter);
        }

        public DataCommandParameter Add(string parameterName, SqlDbType sqlType, ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            DataCommandParameter parameter = new DataCommandParameter(parameterName, sqlType, parameterDirection);
            this.Add(parameter);
            return parameter;
        }

        public DataCommandParameter Add(string parameterName, SqlDbType sqlType, int size,  ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            DataCommandParameter parameter = new DataCommandParameter(parameterName, sqlType, size, parameterDirection);
            this.Add(parameter);
            return parameter;
        }


        public DataCommandParameter this[string name]
        {
            get
            {
                foreach (DataCommandParameter parameter in this)
                {
                    if (parameter.Name == name)
                    {
                        return parameter;
                    }
                }
                throw new IndexOutOfRangeException("Parameter with that name not contained in collection");
            }
            set
            {
                for (int i = 0; i < base.Count; i++)
                {
                    if (base[i].Name == name)
                    {
                        base[i] = value;
                        return;
                    }
                }
                throw new IndexOutOfRangeException("Parameter with that name not contained in collection");
            }
        }
    }

}
