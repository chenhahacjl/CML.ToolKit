﻿using System.Reflection;

namespace CML.EntertainmentEx.FriendEx
{
    /// <summary>
    /// 娱乐包朋友游戏版本信息
    /// </summary>
    public class VersionInfo : CommonEx.VersionEx.VersionBase
    {
        #region 版本信息
        /// <summary>
        /// 主版本号
        /// </summary>
        public override string CP_VerMain => "1.1";
        /// <summary>
        /// 研发版本号
        /// </summary>
        public override string CP_VerDev => "19Y002R001";
        /// <summary>
        /// 更新时间
        /// </summary>
        public override string CP_VerDate => "2019年06月27日 09:45";
        /// <summary>
        /// 当前程序集 
        /// </summary>
        protected override Assembly CP_RunAssembly => Assembly.GetExecutingAssembly();
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得版本信息
        /// </summary>
        /// <returns>版本信息</returns>
        public string CF_GetVersionInfo()
        {
            string filePath = "CML.EntertainmentEx.FuncFriend.AssiVersion.UpdateInfo.LOG";
            return base.CF_GetVersionInfo(filePath);
        }
        #endregion
    }
}
