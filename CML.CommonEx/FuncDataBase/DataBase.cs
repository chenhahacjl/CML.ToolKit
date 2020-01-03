using System;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace CML.CommonEx.DataBaseEx
{
    /// <summary>
    /// 数据库功能帮助类
    /// </summary>
    public class DataBase
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
        public DataBase() { }

        /// <summary>
        /// 建立数据库连接
        /// </summary>
        public DataBase(EDataBaseType dbType, string strConnStr)
        {
            CP_ConnectionType = dbType;
            CP_ConnectionString = strConnStr;
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
                    throw new Exception($"数据库类型错误或未设置！数据库类型: {CP_ConnectionType.ToString()}");
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
                    $"数据库类型: {CP_ConnectionType.ToString()}；" +
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
            DataTable dtResult = null;

            try
            {
                if (m_iConn.State == ConnectionState.Broken || m_iConn.State == ConnectionState.Closed)
                {
                    m_iConn.Open();
                }

                m_iCmd.CommandText = strSql;
                m_iCmd.CommandType = CommandType.Text;
                m_iCmd.Parameters.Clear();
                if (parameters != null && parameters.Length != 0)
                {
                    foreach (ModDataParameter parameter in parameters)
                    {
                        IDataParameter iParameter = m_iDataBase.CreateDataParameter(m_iCmd);
                        iParameter.DbType = parameter.DataType;
                        iParameter.ParameterName = parameter.Name;
                        iParameter.Value = parameter.Value;

                        m_iCmd.Parameters.Add(iParameter);
                    }
                }

                IDbDataAdapter iAdapter = m_iDataBase.CreateDataAdapter();
                iAdapter.SelectCommand = m_iCmd;

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
                if (CP_IsAutoCloseConn)
                {
                    m_iConn.Close();
                }
            }

            return dtResult;
        }

        /// <summary>
        /// 执行SQL语句，返回影响的记录数（报错直接抛出异常，请主动捕获）
        /// </summary>
        /// <param name="strSql">SQL语句</param>
        /// <param name="parameters">参数数组</param>
        /// <returns>影响的记录数</returns>
        public int CF_ExecuteNonQuery(string strSql, ModDataParameter[] parameters = null)
        {
            int nResult = -1;

            try
            {
                if (m_iConn.State == ConnectionState.Broken || m_iConn.State == ConnectionState.Closed)
                {
                    m_iConn.Open();
                }

                m_iCmd.CommandText = strSql;
                m_iCmd.CommandType = CommandType.Text;
                m_iCmd.Parameters.Clear();
                if (parameters != null && parameters.Length != 0)
                {
                    foreach (ModDataParameter parameter in parameters)
                    {
                        IDataParameter iParameter = m_iDataBase.CreateDataParameter(m_iCmd);
                        iParameter.DbType = parameter.DataType;
                        iParameter.ParameterName = parameter.Name;
                        iParameter.Value = parameter.Value;
                        m_iCmd.Parameters.Add(iParameter);
                    }
                }

                nResult = m_iCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CP_IsAutoCloseConn)
                {
                    m_iConn.Close();
                }
            }

            return nResult;
        }

        /// <summary>
        /// 执行多条SQL语句，实现数据库事务（报错直接抛出异常，请主动捕获）
        /// </summary>
        /// <param name="lstParameters">事务执行参数</param>
        /// <returns>是否提交成功</returns>
        public void CF_ExecuteTransaction(List<ModTransactionParameter> lstParameters)
        {
            try
            {
                if (m_iConn.State == ConnectionState.Broken || m_iConn.State == ConnectionState.Closed)
                {
                    m_iConn.Open();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            IDbTransaction iTransaction = m_iConn.BeginTransaction();
            try
            {
                foreach (ModTransactionParameter item in lstParameters)
                {
                    m_iCmd.CommandText = item.Sql;
                    m_iCmd.Transaction = iTransaction;
                    m_iCmd.CommandType = CommandType.Text;
                    m_iCmd.Parameters.Clear();
                    if (item.Parameters != null && item.Parameters.Length != 0)
                    {
                        foreach (ModDataParameter parameter in item.Parameters)
                        {
                            IDataParameter iParameter = m_iDataBase.CreateDataParameter(m_iCmd);
                            iParameter.DbType = parameter.DataType;
                            iParameter.ParameterName = parameter.Name;
                            iParameter.Value = parameter.Value;

                            m_iCmd.Parameters.Add(iParameter);
                        }
                    }

                    m_iCmd.ExecuteNonQuery();
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
                if (CP_IsAutoCloseConn)
                {
                    m_iConn.Close();
                }
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
            object objResult = null;

            try
            {
                if (m_iConn.State == ConnectionState.Broken || m_iConn.State == ConnectionState.Closed)
                {
                    m_iConn.Open();
                }

                m_iCmd.CommandText = strSql;
                m_iCmd.CommandType = CommandType.Text;
                m_iCmd.Parameters.Clear();
                if (parameters != null && parameters.Length != 0)
                {
                    foreach (ModDataParameter parameter in parameters)
                    {
                        IDataParameter iParameter = m_iDataBase.CreateDataParameter(m_iCmd);
                        iParameter.DbType = parameter.DataType;
                        iParameter.ParameterName = parameter.Name;
                        iParameter.Value = parameter.Value;
                        m_iCmd.Parameters.Add(iParameter);
                    }
                }

                objResult = m_iCmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (CP_IsAutoCloseConn)
                {
                    m_iConn.Close();
                }
            }

            return objResult;
        }
        #endregion
    }
}
