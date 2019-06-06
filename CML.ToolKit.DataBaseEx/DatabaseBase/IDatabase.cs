using System.Data;

namespace CML.ToolKit.DataBaseEx
{
    /// <summary>
    /// 数据库接口
    /// </summary>
    internal interface IDataBase
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        string ConnectionString { get; set; }

        /// <summary>
        /// 建立Connection对象
        /// </summary>
        /// <returns>Connection对象</returns>
        IDbConnection CreateConnection();

        /// <summary>
        /// 根据连接字符串建立Connection对象
        /// </summary>
        /// <param name="strConn">连接字符串</param>
        /// <returns>Connection对象</returns>
        IDbConnection CreateConnection(string strConn);

        /// <summary>
        /// 建立Command对象
        /// </summary>
        /// <returns>Command对象</returns>
        IDbCommand CreateCommand();

        /// <summary>
        /// 建立DataAdapter对象
        /// </summary>
        /// <returns>DataAdapter对象</returns>
        IDbDataAdapter CreateDataAdapter();

        /// <summary>
        /// 根据Connection建立Transaction
        /// </summary>
        /// <param name="iConn">Connection对象</param>
        /// <returns>Transaction对象</returns>
        IDbTransaction CreateTransaction(IDbConnection iConn);

        /// <summary>
        /// 根据Command对象建立DataReader
        /// </summary>
        /// <param name="iCmd">Command对象</param>
        /// <returns>DataReader对象</returns>
        IDataParameter CreateDataParameter(IDbCommand iCmd);
    }
}
