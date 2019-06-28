using Microsoft.Win32;
using System;

namespace CML.CommonEx.ConfigurationEx
{
    /// <summary>
    /// 注册表操作类
    /// </summary>
    public static class RegOperate
    {
        #region 公有方法
        /// <summary>
        /// 创建注册表项
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        public static bool CF_CreateSubItem(this ERegDomain regDomain, string subItem)
        {
            //判断注册表项是否为空
            if (string.IsNullOrWhiteSpace(subItem))
            {
                return false;
            }

            using (RegistryKey key = GetRegDomain(regDomain))
            {
                if (!CF_ExistSubItem(regDomain, subItem))
                {
                    try
                    {
                        _ = key.CreateSubKey(subItem);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 检测注册表项是否存在
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <returns>注册表项存在情况</returns>
        public static bool CF_ExistSubItem(this ERegDomain regDomain, string subItem)
        {
            //判断注册表项是否为空
            if (string.IsNullOrWhiteSpace(subItem))
            {
                return false;
            }

            bool result = false;
            using (RegistryKey key = OpenSubKey(regDomain, subItem))
            {
                result = key != null;
            }

            return result;
        }

        /// <summary>
        /// 删除注册表项
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <returns>执行情况</returns>
        public static bool CF_DeleteSubItem(this ERegDomain regDomain, string subItem)
        {
            //判断注册表项是否为空
            if (string.IsNullOrWhiteSpace(subItem))
            {
                return false;
            }

            //创建基于注册表基项的节点
            using (RegistryKey key = GetRegDomain(regDomain))
            {
                if (CF_ExistSubItem(regDomain, subItem))
                {
                    //删除注册表项
                    try
                    {
                        key.DeleteSubKey(subItem);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return true;
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
            //判断键值名称是否为空
            if (string.IsNullOrWhiteSpace(regKey))
            {
                return false;
            }

            //返回结果
            bool result = false;

            //判断注册表项是否存在
            if (CF_ExistSubItem(regDomain, subItem))
            {
                //打开注册表项
                using (RegistryKey key = OpenSubKey(regDomain, subItem))
                {
                    //键值集合
                    string[] regeditKeyNames = key.GetValueNames();

                    foreach (string regeditKey in regeditKeyNames)
                    {
                        if (string.Compare(regeditKey, regKey, true) == 0)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }

            return result;
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
            //判断键值名称是否为空
            if (string.IsNullOrWhiteSpace(regKey))
            {
                return false;
            }

            //判断注册表项是否存在
            if (!CF_ExistSubItem(regDomain, subItem))
            {
                CF_CreateSubItem(regDomain, subItem);
            }

            //以可写方式打开注册表项
            using (RegistryKey key = OpenSubKey(regDomain, subItem, true))
            {
                //注册表项打开失败
                if (key == null) { return false; }

                try
                {
                    key.SetValue(regKey, value, GetRegValueKind(valueKind));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return true;
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
            //判断键值名称是否为空
            if (string.IsNullOrWhiteSpace(regKey))
            {
                return null;
            }

            //键值内容结果
            object value = null;

            //判断键值是否存在
            if (CF_ExistRegeditKey(regDomain, subItem, regKey))
            {
                //打开注册表项
                using (RegistryKey key = OpenSubKey(regDomain, subItem))
                {
                    if (key != null)
                    {
                        value = key.GetValue(regKey);
                    }
                }
            }

            return value;
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
            //判断注册表项和键值名称是否为空
            if (string.IsNullOrWhiteSpace(subItem) || string.IsNullOrWhiteSpace(regKey))
            {
                return false;
            }

            //判断键值是否存在
            if (CF_ExistRegeditKey(regDomain, subItem, regKey))
            {
                //以可写方式打开注册表项
                using (RegistryKey key = OpenSubKey(regDomain, subItem, true))
                {
                    if (key == null) { return false; }

                    try
                    {
                        key.DeleteValue(regKey);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
            }

            return true;
        }
        #endregion

        #region 内部方法
        /// <summary>
        /// 获取注册表基项域对应顶级节点
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <returns>对应顶级节点</returns>
        private static RegistryKey GetRegDomain(ERegDomain regDomain)
        {
            switch (regDomain)
            {
                case ERegDomain.ClassesRoot:
                {
                    return Registry.ClassesRoot;
                }
                case ERegDomain.CurrentUser:
                {
                    return Registry.CurrentUser;
                }
                case ERegDomain.LocalMachine:
                {
                    return Registry.LocalMachine;
                }
                case ERegDomain.User:
                {
                    return Registry.Users;
                }
                case ERegDomain.CurrentConfig:
                {
                    return Registry.CurrentConfig;
                }
                case ERegDomain.PerformanceData:
                {
                    return Registry.PerformanceData;
                }
                default:
                {
                    return Registry.LocalMachine;
                }
            }
        }

        /// <summary>
        /// 获取注册表中对应值数据类型
        /// </summary>
        /// <param name="regValueKind">注册表数据类型</param>
        /// <returns>对应数据类型</returns>
        private static RegistryValueKind GetRegValueKind(ERegValueKind regValueKind)
        {
            switch (regValueKind)
            {
                case ERegValueKind.Unknown:
                {
                    return RegistryValueKind.Unknown;
                }
                case ERegValueKind.String:
                {
                    return RegistryValueKind.String;
                }
                case ERegValueKind.ExpandString:
                {
                    return RegistryValueKind.ExpandString;
                }
                case ERegValueKind.Binary:
                {
                    return RegistryValueKind.Binary;
                }
                case ERegValueKind.DWord:
                {
                    return RegistryValueKind.DWord;
                }
                case ERegValueKind.MultiString:
                {
                    return RegistryValueKind.MultiString;
                }
                case ERegValueKind.QWord:
                {
                    return RegistryValueKind.QWord;
                }
                default:
                {
                    return RegistryValueKind.String;
                }
            }
        }

        /// <summary>
        /// 打开注册表项节点
        /// </summary>
        /// <param name="regDomain">注册表基项域</param>
        /// <param name="subItem">注册表项名称</param>
        /// <param name="isWritable">是否需要节点Write访问权限</param>
        /// <returns>若注册表节点存在，则返回节点，否则返回null</returns>
        private static RegistryKey OpenSubKey(ERegDomain regDomain, string subItem, bool isWritable = false)
        {
            //判断注册表项名称是否为空
            if (subItem == string.Empty || subItem == null)
            {
                return null;
            }


            //创建基于注册表基项的节点
            RegistryKey key = GetRegDomain(regDomain);

            //打开注册表项
            RegistryKey sKey;
            try
            {
                sKey = key.OpenSubKey(subItem, isWritable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                key.Close();
            }

            return sKey;
        }
        #endregion
    }
}
