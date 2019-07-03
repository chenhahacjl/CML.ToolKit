namespace CML.CommonEx.SingletonEx
{
    /// <summary>
    /// 单实例类基类
    /// </summary>
    /// <typeparam name="T">类</typeparam>
    public class SingletonBase<T> where T : class, new()
    {
        /// <summary>
        /// 单例对象
        /// </summary>
        private static T m_instance;
        /// <summary>
        /// 系统锁
        /// </summary>
        private static readonly object m_sysLock = new object();

        /// <summary>
        /// 获得单例对象
        /// </summary>
        /// <returns>单例对象</returns>
        public static T CP_Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_sysLock)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new T();
                        }
                    }
                }
                return m_instance;
            }
        }
    }
}
