using System.Reflection;

namespace CML.CommonEx.NetworkEx
{
    /// <summary>
    /// 常用工具包网络操作工具版本信息
    /// </summary>
    public class VersionInfo : VersionEx.VersionBase
    {
        #region 版本信息
        /// <summary>
        /// 主版本号
        /// </summary>
        public override string CP_VerMain => "1.5";
        /// <summary>
        /// 研发版本号
        /// </summary>
        public override string CP_VerDev => "19Y006R002";
        /// <summary>
        /// 更新时间
        /// </summary>
        public override string CP_VerDate => "2019年09月04日 09:10";
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
            string filePath = "CML.CommonEx.FuncNetwork.AssiVersion.UpdateInfo.LOG";
            return base.CF_GetVersionInfo(filePath);
        }
        #endregion
    }
}
