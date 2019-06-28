using System.Reflection;

namespace CML.CommonEx.UIAutomationEx
{
    /// <summary>
    /// 常用工具包UI自动化操作工具版本信息
    /// </summary>
    public class VersionInfo : VersionEx.VersionBase
    {
        #region 版本信息
        /// <summary>
        /// 主版本号
        /// </summary>
        public override string VerMain => "1.1";
        /// <summary>
        /// 研发版本号
        /// </summary>
        public override string VerDev => "19Y002R002";
        /// <summary>
        /// 更新时间
        /// </summary>
        public override string VerDate => "2019年6月28日 12:50";
        /// <summary>
        /// 当前程序集 
        /// </summary>
        protected override Assembly RunAssembly => Assembly.GetExecutingAssembly();
        #endregion

        #region 公共方法
        /// <summary>
        /// 获得版本信息
        /// </summary>
        /// <returns>版本信息</returns>
        public string GetVersionInfo()
        {
            string filePath = "CML.CommonEx.FuncUIAutomation.AssiVersion.UpdateInfo.LOG";
            return base.GetVersionInfo(filePath);
        }
        #endregion
    }
}
