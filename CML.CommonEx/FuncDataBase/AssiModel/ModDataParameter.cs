using System;
using System.Data;

namespace CML.CommonEx.DataBaseEx
{
    /// <summary>
    /// 数据传输参数模型
    /// </summary>
    public class ModDataParameter
    {
        /// <summary>
        /// 参数名
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// 参数值
        /// </summary>
        public object Value { get; set; } = DBNull.Value;
        /// <summary>
        /// 数据类型
        /// </summary>
        public DbType DataType { get; set; } = DbType.String;
        /// <summary>
        /// 数据长度
        /// </summary>
        public int DataSize { get; set; } = -1;
        /// <summary>
        /// 数据方向（存储过程用）
        /// </summary>
        public ParameterDirection Direction { get; set; } = ParameterDirection.Input;

        /// <summary>
        /// 数据传输参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="type">数据类型</param>
        public ModDataParameter(string name, object value, DbType type)
        {
            Name = name;
            Value = value;
            DataType = type;
        }

        /// <summary>
        /// 数据传输参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="type">数据类型</param>
        /// <param name="size">数据长度</param>
        public ModDataParameter(string name, object value, DbType type, int size)
        {
            Name = name;
            Value = value;
            DataType = type;
            DataSize = size;
        }

        /// <summary>
        /// 数据传输参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="type">数据类型</param>
        /// <param name="direction">数据方向</param>
        public ModDataParameter(string name, object value, DbType type, ParameterDirection direction)
        {
            Name = name;
            Value = value;
            DataType = type;
            Direction = direction;
        }

        /// <summary>
        /// 数据传输参数
        /// </summary>
        /// <param name="name">参数名</param>
        /// <param name="value">参数值</param>
        /// <param name="type">数据类型</param>
        /// <param name="size">数据长度</param>
        /// <param name="direction">数据方向</param>
        public ModDataParameter(string name, object value, DbType type, int size, ParameterDirection direction)
        {
            Name = name;
            Value = value;
            DataType = type;
            DataSize = size;
            Direction = direction;
        }

        /// <summary>
        /// 加载数据参数
        /// </summary>
        /// <param name="parameter">[REF]数据参数</param>
        public void LoadDataParameter(ref IDbDataParameter parameter)
        {
            parameter.ParameterName = Name;
            parameter.Value = Value;
            parameter.DbType = DataType;
            parameter.Direction = Direction;
            if (DataSize != -1)
            {
                parameter.Size = DataSize;
            }
        }
    }
}
