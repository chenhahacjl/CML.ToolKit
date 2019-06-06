namespace CML.ToolKit.ControlEx
{
    /// <summary>
    /// 登录结果
    /// </summary>
    public enum ELoginResult
    {
        /// <summary>
        /// 登录成功
        /// </summary>
        Success = 0,
        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        WrongUsernameOrPassword = 1,
        /// <summary>
        /// 网络错误
        /// </summary>
        NetworkError = 2,
        /// <summary>
        /// 网络错误
        /// </summary>
        PermissionDeny = 3
    }
}
