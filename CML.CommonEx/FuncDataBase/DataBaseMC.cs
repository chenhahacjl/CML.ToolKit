using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CML.CommonEx.DataBaseEx
{
    /// <summary>
    /// 数据库功能帮助类（多连接）
    /// </summary>
    public class DataBaseMC
    {
        #region 私有变量
        /// <summary>
        /// 数据库
        /// </summary>
        private IDataBase m_iDataBase = null;
        /// <summary>
        /// 数据库连接
        /// </summary>
        private IDbConnection m_iConn = null;
        /// <summary>
        /// 数据库执行命令
        /// </summary>
        private IDbCommand m_iCmd = null;
        /// <summary>
        /// 数据库初始化标志
        /// </summary>
        private bool m_isInitDataBase = false;
        #endregion

        #region 公共属性
        /// <summary>
        /// 连接数据库类型
        /// </summary>
        public EDataBaseType CP_ConnectionType { get; set; } = EDataBaseType.NONE;

        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string CP_ConnectionString { get; set; } = "";

        /// <summary>
        /// 是否自动关闭数据库连接(默认开启自动关闭)
        /// </summary>
        public bool CP_IsAutoCloseConn { get; set; } = true;
        #endregion

        #region 构造函数
        /// <summary>
        /// 建立数据库连接
        /// </summary>
        public DataBaseMC() { }

        /// <summary>
        /// 建立数据库连接
        /// </summary>
        public DataBaseMC(EDataBaseType dbType, string strConnStr)
        {
            CP_ConnectionType = dbType;
            CP_ConnectionString = strConnStr;
        }
        #endregion

        #region 数据库连接操作

        private IDbCommand CreateDbCommand()
        {
            if (!m_isInitDataBase)
            {
                throw new Exception($"数据库未初始化！\n");
            }

            if (string.IsNullOrEmpty(CP_ConnectionString))
            {
                throw new Exception(
                    $"数据库连接配置错误！" +
                    $"数据库类型: {CP_ConnectionType}；" +
                    $"连接字符串: {CP_ConnectionString}。");
            }

            try
            {
                var command = m_iDataBase.CreateCommand();
                var connection = m_iDataBase.CreateConnection();

                command.Connection = connection;

                return command;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region 连接操作方法
        /// <summary>
        /// 初始化数据库（报错直接抛出异常，请主动捕获）
        /// </summary>
        public void CF_InitDataBase()
        {
            switch (CP_ConnectionType)
            {
                case EDataBaseType.SQLSERVER:
                {
                    m_iDataBase = new SqlServerDataBase();
                    break;
                }
                case EDataBaseType.MYSQL:
                {
                    m_iDataBase = new MySqlDataBase();
                    break;
                }
                case EDataBaseType.ORACLE:
                {
                    m_iDataBase = new OracleDataBase();
                    break;
                }
                case EDataBaseType.ACCESS:
                {
                    m_iDataBase = new AccessDataBase();
                    break;
                }
                default:
                {
                    m_isInitDataBase = false;
                    throw new Exception($"数据库类型错误或未设置！数据库类型: {CP_ConnectionType}");
                }
            }

            //异常
            Exception exception = null;

            try
            {
                m_iDataBase.ConnectionString = CP_ConnectionString;

                m_iConn = m_iDataBase.CreateConnection();
                m_iCmd = m_iDataBase.CreateCommand();
                m_iCmd.Connection = m_iConn;

                m_isInitDataBase = true;
            }
            catch (FileNotFoundException)
            {
                exception = new Exception($"缺少运行依赖程序: {m_iDataBase.RuntimeDepend}。");
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            finally
            {
                //存在异常
                if (exception != null)
                {
                    m_iDataBase = null;
                    m_iConn = null;
                    m_iCmd = null;

                    m_isInitDataBase = false;

                    throw exception;
                }
            }
        }

        /// <summary>
        /// 打开数据库连接（报错直接抛出异常，请主动捕获）
        /// </summary>
        public void CF_OpenConnect()
        {
            if (!m_isInitDataBase)
            {
                throw new Exception($"数据库未初始化！\n");
            }

            if (string.IsNullOrEmpty(CP_ConnectionString))
            {
                throw new Exception(
                    $"数据库连接配置错误！" +
                    $"数据库类型: {CP_ConnectionType}；" +
                    $"连接字符串: {CP_ConnectionString}。");
            }

            try
            {
                m_iConn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 关闭数据库连接（报错直接抛出异常，请主动捕获）
        /// </summary>
        public void CF_CloseConnect()
        {
            if (m_isInitDataBase)
            {
                try
                {
                    m_iConn.Close();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }
        #endregion

        #region SQL执行方法
        /// <summary>
        /// 执行查询语句，返回DataTable（报错直接抛出异常，请主动捕获）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>DataTable</returns>
        public DataTable CF_ExecuteQuery(string strSql, ModDataParameter[] parameters = null)
        {
            var command = CreateDbCommand();

            {
                DataTable dtResult = null;

                try
                {
                    if (command.Connection.State == ConnectionState.Broken || command.Connection.State == ConnectionState.Closed)
                    {
                        command.Connection.Open();
                    }

                    command.CommandText = strSql;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    if (parameters != null && parameters.Length != 0)
                    {
                        foreach (ModDataParameter parameter in parameters)
                        {
                            IDbDataParameter iParameter = m_iDataBase.CreateDataParameter(command);
                            parameter.LoadDataParameter(ref iParameter);

                            command.Parameters.Add(iParameter);
                        }
                    }

                    IDbDataAdapter iAdapter = m_iDataBase.CreateDataAdapter();
                    iAdapter.SelectCommand = command;

                    DataSet dsResult = new DataSet();
                    iAdapter.Fill(dsResult);

                    if (dsResult != null && dsResult.Tables.Count != 0)
                    {
                        dtResult = dsResult.Tables[0];
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }

                return dtResult;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数（报错直接抛出异常，请主动捕获）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>影响的记录数</returns>
        public int CF_ExecuteNonQuery(string strSql, ModDataParameter[] parameters = null)
        {
            var command = CreateDbCommand();

            {
                int nResult = -1;

                try
                {
                    if (command.Connection.State == ConnectionState.Broken || command.Connection.State == ConnectionState.Closed)
                    {
                        command.Connection.Open();
                    }

                    command.CommandText = strSql;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    if (parameters != null && parameters.Length != 0)
                    {
                        foreach (ModDataParameter parameter in parameters)
                        {
                            IDbDataParameter iParameter = m_iDataBase.CreateDataParameter(command);
                            parameter.LoadDataParameter(ref iParameter);

                            command.Parameters.Add(iParameter);
                        }
                    }

                    nResult = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }

                return nResult;
            }
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务（报错直接抛出异常，请主动捕获）
        /// </summary>
        /// <param name="lstParameters">事务执行参数</param>
        /// <returns>是否提交成功</returns>
        public void CF_ExecuteTransaction(List<ModTransactionParameter> lstParameters)
        {
            var command = CreateDbCommand();
            {
                try
                {
                    if (command.Connection.State == ConnectionState.Broken || command.Connection.State == ConnectionState.Closed)
                    {
                        command.Connection.Open();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                IDbTransaction iTransaction = command.Connection.BeginTransaction();
                try
                {
                    foreach (ModTransactionParameter item in lstParameters)
                    {
                        command.CommandText = item.Sql;
                        command.Transaction = iTransaction;
                        command.CommandType = CommandType.Text;
                        command.Parameters.Clear();
                        if (item.Parameters != null && item.Parameters.Length != 0)
                        {
                            foreach (ModDataParameter parameter in item.Parameters)
                            {
                                IDbDataParameter iParameter = m_iDataBase.CreateDataParameter(command);
                                parameter.LoadDataParameter(ref iParameter);

                                command.Parameters.Add(iParameter);
                            }
                        }

                        command.ExecuteNonQuery();
                    }

                    iTransaction.Commit();
                }
                catch (Exception ex)
                {
                    iTransaction.Rollback();
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }
            }
        }

        /// <summary>
        /// 执行存储过程（报错直接抛出异常，请主动捕获）
        /// </summary>
        /// <param name="procName">存储过程名称</param>
        /// <param name="parameters">存储过程执行参数</param>
        /// <returns>Dictionary</returns>
        public Dictionary<string, object> CF_ExecuteStoredProcedure(string procName, ModDataParameter[] parameters = null)
        {
            var command = CreateDbCommand();
            {
                Dictionary<string, object> dicResult = new Dictionary<string, object>();

                try
                {
                    if (command.Connection.State == ConnectionState.Broken || command.Connection.State == ConnectionState.Closed)
                    {
                        command.Connection.Open();
                    }

                    command.CommandText = procName;
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Clear();
                    if (parameters != null && parameters.Length != 0)
                    {
                        foreach (ModDataParameter parameter in parameters)
                        {
                            IDbDataParameter iParameter = m_iDataBase.CreateDataParameter(command);
                            parameter.LoadDataParameter(ref iParameter);

                            command.Parameters.Add(iParameter);
                        }
                    }

                    dicResult.Add("__NUMBER__", command.ExecuteNonQuery());

                    if (parameters != null && parameters.Length != 0)
                    {
                        foreach (IDataParameter parameter in command.Parameters)
                        {
                            dicResult.Add(parameter.ParameterName, parameter.Value);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }

                return dicResult;
            }
        }

        /// <summary>
        /// 执行SQL语句，返回结果集中的第一行的第一列（报错直接抛出异常，请主动捕获）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>结果集中的第一行的第一列</returns>
        public object CF_GetSingleObject(string strSql, ModDataParameter[] parameters = null)
        {
            var command = CreateDbCommand();
            {
                object objResult = null;

                try
                {
                    if (command.Connection.State == ConnectionState.Broken || command.Connection.State == ConnectionState.Closed)
                    {
                        command.Connection.Open();
                    }

                    command.CommandText = strSql;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Clear();
                    if (parameters != null && parameters.Length != 0)
                    {
                        foreach (ModDataParameter parameter in parameters)
                        {
                            IDbDataParameter iParameter = m_iDataBase.CreateDataParameter(command);
                            parameter.LoadDataParameter(ref iParameter);

                            command.Parameters.Add(iParameter);
                        }
                    }

                    objResult = command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    command.Connection.Close();
                }

                return objResult;
            }
        }
        #endregion
    }
}
