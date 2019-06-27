namespace CML.ControlEx
{
    /// <summary>
    /// 登录信息
    /// </summary>
    public struct ModLoginInfo
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public ModLoginInfo(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
