namespace CML.ToolKit.DataBaseEx
{
    /// <summary>
    /// 事务执行参数模型
    /// </summary>
    public class ModTransactionParameter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="parameters">参数</param>
        public ModTransactionParameter(string strSql, ModDataParameter[] parameters = null)
        {
            Sql = strSql;
            Parameters = parameters;
        }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string Sql { get; }

        /// <summary>
        /// SQL语句中的参数
        /// </summary>
        public ModDataParameter[] Parameters { get; }
    }
}
