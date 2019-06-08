using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.IO;
using System.Reflection;

namespace CML.ToolKit.DataBaseEx
{
    /// <summary>
    /// MYSQL 数据库操作类
    /// </summary>
    internal class MySqlDataBase : IDataBase
    {
        /// <summary>
        /// MYSQL 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// 检测操作库文件是否存在(MySql.Data.dll)
        /// </summary>
        public MySqlDataBase()
        {
            string strCurDllDir = new FileInfo(Assembly.GetExecutingAssembly().Location).DirectoryName;
            string strExDllPath = Path.Combine(strCurDllDir, "MySql.Data.dll");

            if (!File.Exists(strExDllPath))
            {
                throw new Exception("缺少运行文件（MySql.Data.dll）！");
            }
        }

        /// <summary>
        /// 建立Connection对象
        /// </summary>
        /// <returns>Connection对象</returns>
        public IDbConnection CreateConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        /// <summary>
        /// 根据连接字符串建立Connection对象
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns>Connection对象</returns>
        public IDbConnection CreateConnection(string strConn)
        {
            return new MySqlConnection(strConn);
        }

        /// <summary>
        /// 建立Command对象
        /// </summary>
        /// <returns>Command对象</returns>
        public IDbCommand CreateCommand()
        {
            return new MySqlCommand();
        }

        /// <summary>
        /// 建立DataAdapter对象
        /// </summary>
        /// <returns>DataAdapter对象</returns>
        public IDbDataAdapter CreateDataAdapter()
        {
            return new MySqlDataAdapter();
        }

        /// <summary>
        /// 根据Connection建立Transaction
        /// </summary>
        /// <param name="iConn">Connection对象</param>
        /// <returns>Transaction对象</returns>
        public IDbTransaction CreateTransaction(IDbConnection iConn)
        {
            return iConn.BeginTransaction();
        }

        /// <summary>
        /// 根据Command对象建立DataReader
        /// </summary>
        /// <param name="iCmd">Command对象</param>
        /// <returns>DataReader对象</returns>
        public IDataParameter CreateDataParameter(IDbCommand iCmd)
        {
            return iCmd.CreateParameter();
        }
    }
}
