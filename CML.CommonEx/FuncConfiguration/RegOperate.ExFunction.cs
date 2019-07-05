namespace CML.CommonEx.ConfigurationEx.ExFunction
{
    /// <summary>
    /// 注册表操作类(扩展方法)
    /// </summary>
    public static class RegOperateEF
    {
        #region 公有方法
        /// <summary>
        /// 创建注册表项
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        public static bool CF_CreateSubItem(this ERegDomain regDomain, string subItem)
        {
            return RegOperate.CF_CreateSubItem(regDomain, subItem);
        }

        /// <summary>
        /// 检测注册表项是否存在
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <returns>注册表项存在情况</returns>
        public static bool CF_ExistSubItem(this ERegDomain regDomain, string subItem)
        {
            return RegOperate.CF_ExistSubItem(regDomain, subItem);
        }

        /// <summary>
        /// 删除注册表项
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <returns>执行情况</returns>
        public static bool CF_DeleteSubItem(this ERegDomain regDomain, string subItem)
        {
            return RegOperate.CF_DeleteSubItem(regDomain, subItem);
        }

        /// <summary>
        /// 检测注册表键值是否存在
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <param name="regKey">键值名称</param>
        /// <returns>注册表键值存在情况</returns>
        public static bool CF_ExistRegeditKey(this ERegDomain regDomain, string subItem, string regKey)
        {
            return RegOperate.CF_ExistRegeditKey(regDomain, subItem, regKey);
        }

        /// <summary>
        /// 设置指定的键值内容
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <param name="regKey">键值名称</param>
        /// <param name="valueKind">键值类型</param>
        /// <param name="value">键值内容</param>
        /// <returns>执行情况</returns>
        public static bool CF_WriteRegeditKey(this ERegDomain regDomain, string subItem, string regKey, ERegValueKind valueKind, object value)
        {
            return RegOperate.CF_WriteRegeditKey(regDomain, subItem, regKey, valueKind, value);
        }

        /// <summary>
        /// 读取键值内容
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <param name="regKey">键值名称</param>
        /// <returns>键值内容</returns>
        public static object CF_ReadRegeditKey(this ERegDomain regDomain, string subItem, string regKey)
        {
            return RegOperate.CF_ReadRegeditKey(regDomain, subItem, regKey);
        }

        /// <summary>
        /// 删除键值
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <param name="regKey">键值名称</param>
        /// <returns>执行结果</returns>
        public static bool CF_DeleteRegeditKey(this ERegDomain regDomain, string subItem, string regKey)
        {
            return RegOperate.CF_DeleteRegeditKey(regDomain, subItem, regKey);
        }
        #endregion
    }
}
