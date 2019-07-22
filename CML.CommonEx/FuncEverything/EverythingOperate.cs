using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CML.CommonEx.EverythingEx
{
    /// <summary>
    /// Everything软件搜索操作类
    /// 需要[Everything32.dll/Everything64.dll]文件
    /// 需要运行[Everything]软件
    /// </summary>
    public class EverythingOperate
    {
        /// <summary>
        /// 软件平台（默认32位）
        /// </summary>
        public static EPlatform CP_Platform { get; set; } = EPlatform.X86;

        /// <summary>
        /// 自动检测软件平台
        /// </summary>
        public static void CF_DetectPlatform()
        {
            CP_Platform = Environment.Is64BitProcess ? EPlatform.X64 : EPlatform.X86;
        }

        #region Everything软件操作
        /// <summary>
        /// 判断Everything软件是否以管理员身份启动
        /// </summary>
        /// <returns>判断结果</returns>
        public static bool CF_IsAdmin()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IsAdmin();
                }
                else
                {
                    return ModEverything64.Everything_IsAdmin();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断Everything软件配置文件是否保存在AppData文件夹（否则与软件同文件夹）
        /// </summary>
        /// <returns>判断结果</returns>
        public static bool CF_IsAppData()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IsAppData();
                }
                else
                {
                    return ModEverything64.Everything_IsAppData();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断Everything软件数据库是否加载完成
        /// </summary>
        /// <returns>判断结果</returns>
        public static bool CF_IsDBLoaded()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IsDBLoaded();
                }
                else
                {
                    return ModEverything64.Everything_IsDBLoaded();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Everything软件主版本号
        /// </summary>
        /// <returns>主版本号</returns>
        public static uint CF_GetMajorVersion()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetMajorVersion();
                }
                else
                {
                    return ModEverything64.Everything_GetMajorVersion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Everything软件次版本号
        /// </summary>
        /// <returns>次版本号</returns>
        public static uint CF_GetMinorVersion()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetMinorVersion();
                }
                else
                {
                    return ModEverything64.Everything_GetMinorVersion();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Everything软件修订号
        /// </summary>
        /// <returns>修订号</returns>
        public static uint CF_GetRevision()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetRevision();
                }
                else
                {
                    return ModEverything64.Everything_GetRevision();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Everything软件内部版本号
        /// </summary>
        /// <returns>内部版本号</returns>
        public static uint CF_GetBuildNumber()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetBuildNumber();
                }
                else
                {
                    return ModEverything64.Everything_GetBuildNumber();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Everything软件运行平台类型
        /// </summary>
        /// <returns>平台类型</returns>
        public static ETarget CF_GetTargetMachine()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return (ETarget)ModEverything32.Everything_GetTargetMachine();
                }
                else
                {
                    return (ETarget)ModEverything64.Everything_GetTargetMachine();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 退出Everything软件
        /// </summary>
        /// <returns>执行结果</returns>
        public static bool CF_Exit()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_Exit();
                }
                else
                {
                    return ModEverything64.Everything_Exit();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存Everything软件索引数据库
        /// </summary>
        /// <returns>执行结果</returns>
        public static bool CF_SaveDB()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_SaveDB();
                }
                else
                {
                    return ModEverything64.Everything_SaveDB();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 强制重建Everything软件数据库索引
        /// </summary>
        /// <returns>执行结果</returns>
        public static bool CF_RebuildDB()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_RebuildDB();
                }
                else
                {
                    return ModEverything64.Everything_RebuildDB();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 重新扫描所有文件夹索引
        /// </summary>
        /// <returns>执行结果</returns>
        public static bool CF_UpdateAllFolderIndexes()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_UpdateAllFolderIndexes();
                }
                else
                {
                    return ModEverything64.Everything_UpdateAllFolderIndexes();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 保存Everything软件运行记录
        /// </summary>
        /// <returns>执行结果</returns>
        public static bool CF_SaveRunHistory()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_SaveRunHistory();
                }
                else
                {
                    return ModEverything64.Everything_SaveRunHistory();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除Everything软件运行记录
        /// </summary>
        /// <returns>执行结果</returns>
        public static bool CF_DeleteRunHistory()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_DeleteRunHistory();
                }
                else
                {
                    return ModEverything64.Everything_DeleteRunHistory();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 执行错误读取
        /// <summary>
        /// 获取最后一次错误信息
        /// </summary>
        /// <returns>错误信息</returns>
        public static EExecuteError CF_GetLastError()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return (EExecuteError)ModEverything32.Everything_GetLastError();
                }
                else
                {
                    return (EExecuteError)ModEverything64.Everything_GetLastError();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 搜索设置
        /// <summary>
        /// 设置搜索字符串
        /// </summary>
        /// <param name="lpSearchString">搜索字符串</param>
        public static void CF_SetSearch(string lpSearchString)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetSearchW(lpSearchString);
                }
                else
                {
                    ModEverything64.Everything_SetSearchW(lpSearchString);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取搜索字符串
        /// </summary>
        /// <returns>搜索字符串</returns>
        public static string CF_GetSearch()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetSearchW());
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetSearchW());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 启用或禁用全路径匹配
        /// </summary>
        /// <param name="bEnable">全路径匹配状态</param>
        public static void CF_SetMatchPath(bool bEnable)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetMatchPath(bEnable);
                }
                else
                {
                    ModEverything64.Everything_SetMatchPath(bEnable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取全路径匹配状态
        /// </summary>
        /// <returns>全路径匹配状态</returns>
        public static bool CF_GetMatchPath()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetMatchPath();
                }
                else
                {
                    return ModEverything64.Everything_GetMatchPath();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 启用或禁用字母大小写匹配
        /// </summary>
        /// <param name="bEnable">字母大小写匹配状态</param>
        public static void CF_SetMatchCase(bool bEnable)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetMatchCase(bEnable);
                }
                else
                {
                    ModEverything64.Everything_SetMatchCase(bEnable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取字母大小写匹配状态
        /// </summary>
        /// <returns>字母大小写匹配状态</returns>
        public static bool CF_GetMatchCase()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetMatchCase();
                }
                else
                {
                    return ModEverything64.Everything_GetMatchCase();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 启用或禁用全字匹配
        /// </summary>
        /// <param name="bEnable">全字匹配状态</param>
        public static void CF_SetMatchWholeWord(bool bEnable)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetMatchWholeWord(bEnable);
                }
                else
                {
                    ModEverything64.Everything_SetMatchWholeWord(bEnable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取全字匹配状态
        /// </summary>
        /// <returns>全字匹配状态</returns>
        public static bool CF_GetMatchWholeWord()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetMatchWholeWord();
                }
                else
                {
                    return ModEverything64.Everything_GetMatchWholeWord();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 启用或禁用正则表达式匹配
        /// </summary>
        /// <param name="bEnable">正则表达式匹配状态</param>
        public static void CF_SetRegex(bool bEnable)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetRegex(bEnable);
                }
                else
                {
                    ModEverything64.Everything_SetRegex(bEnable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取正则表达式匹配状态
        /// </summary>
        /// <returns>正则表达式匹配状态</returns>
        public static bool CF_GetRegex()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetRegex();
                }
                else
                {
                    return ModEverything64.Everything_GetRegex();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置结果返回最大数量
        /// </summary>
        /// <param name="dwMax">最大数量（设置0xFFFFFFFF返回所有结果）</param>
        public static void CF_SetMax(uint dwMax)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetMax(dwMax);
                }
                else
                {
                    ModEverything64.Everything_SetMax(dwMax);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取结果返回最大数量
        /// </summary>
        /// <returns>结果返回最大数量（0xFFFFFFFF为所有结果）</returns>
        public static uint CF_GetMax()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetMax();
                }
                else
                {
                    return ModEverything64.Everything_GetMax();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置结果返回偏移量
        /// </summary>
        /// <param name="dwOffset">偏移量（设置0从第一条开始返回）</param>
        public static void CF_SetOffset(uint dwOffset)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetOffset(dwOffset);
                }
                else
                {
                    ModEverything64.Everything_SetOffset(dwOffset);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取结果返回偏移量
        /// </summary>
        /// <returns>结果返回偏移量（0为从第一条开始返回）</returns>
        public static uint CF_GetOffset()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetOffset();
                }
                else
                {
                    return ModEverything64.Everything_GetOffset();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 搜索操作
        /// <summary>
        /// 执行搜索
        /// </summary>
        /// <param name="bWait">是否等待搜索结束</param>
        /// <returns>执行结果</returns>
        public static bool CF_Query(bool bWait)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_QueryW(bWait);
                }
                else
                {
                    return ModEverything64.Everything_QueryW(bWait);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据路径与文件名对当前结果进行排序
        /// </summary>
        public static void CF_SortResultsByPath()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SortResultsByPath();
                }
                else
                {
                    ModEverything64.Everything_SortResultsByPath();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 重置结果列表和搜索状态到默认状态，释放分配的内存
        /// </summary>
        public static void CF_Reset()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_Reset();
                }
                else
                {
                    ModEverything64.Everything_Reset();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 重置结果列表和搜索状态，释放分配的内存
        /// </summary>
        public static void CF_CleanUp()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_CleanUp();
                }
                else
                {
                    ModEverything64.Everything_CleanUp();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 结果设置
        /// <summary>
        /// 设置结果的排序方式
        /// </summary>
        /// <param name="eSortType">排序方式</param>
        public static void CF_SetSort(ESortMode eSortType)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetSort((uint)eSortType);
                }
                else
                {
                    ModEverything64.Everything_SetSort((uint)eSortType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取结果的排序方式
        /// </summary>
        /// <returns>排序方式</returns>
        public static ESortMode CF_GetSort()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return (ESortMode)ModEverything32.Everything_GetSort();
                }
                else
                {
                    return (ESortMode)ModEverything64.Everything_GetSort();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取结果的实际排序顺序
        /// </summary>
        /// <returns>排序顺序</returns>
        public static ESortMode CF_GetResultListSort()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return (ESortMode)ModEverything32.Everything_GetResultListSort();
                }
                else
                {
                    return (ESortMode)ModEverything64.Everything_GetResultListSort();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置所需的结果数据类型
        /// </summary>
        /// <param name="eRequestFlags">数据类型</param>
        public static void CF_SetRequestFlags(ERequest eRequestFlags)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetRequestFlags((uint)eRequestFlags);
                }
                else
                {
                    ModEverything64.Everything_SetRequestFlags((uint)eRequestFlags);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所需的结果数据类型
        /// </summary>
        /// <returns>数据类型</returns>
        public static ERequest CF_GetRequestFlags()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return (ERequest)ModEverything32.Everything_GetRequestFlags();
                }
                else
                {
                    return (ERequest)ModEverything64.Everything_GetRequestFlags();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取可用结果数据类型
        /// </summary>
        /// <returns></returns>
        public static ERequest CF_GetResultListRequestFlags()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return (ERequest)ModEverything32.Everything_GetResultListRequestFlags();
                }
                else
                {
                    return (ERequest)ModEverything64.Everything_GetResultListRequestFlags();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 结果获取
        /// <summary>
        /// 获取可见文件结果数量
        /// </summary>
        /// <returns>结果数量</returns>
        public static uint CF_GetNumFileResults()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetNumFileResults();
                }
                else
                {
                    return ModEverything64.Everything_GetNumFileResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取可见文件夹结果数量
        /// </summary>
        /// <returns>结果数量</returns>
        public static uint CF_GetNumFolderResults()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetNumFolderResults();
                }
                else
                {
                    return ModEverything64.Everything_GetNumFolderResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取可见结果数量
        /// </summary>
        /// <returns>结果数量</returns>
        public static uint CF_GetNumResults()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetNumResults();
                }
                else
                {
                    return ModEverything64.Everything_GetNumResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有文件结果数量
        /// </summary>
        /// <returns>结果数量</returns>
        public static uint CF_GetTotFileResults()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetTotFileResults();
                }
                else
                {
                    return ModEverything64.Everything_GetTotFileResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有文件夹结果数量
        /// </summary>
        /// <returns>结果数量</returns>
        public static uint CF_GetTotFolderResults()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetTotFolderResults();
                }
                else
                {
                    return ModEverything64.Everything_GetTotFolderResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有结果数量
        /// </summary>
        /// <returns>结果数量</returns>
        public static uint CF_GetTotResults()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetTotResults();
                }
                else
                {
                    return ModEverything64.Everything_GetTotResults();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断指定可见结果是否为磁盘根目录（如C:）
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>判断结果</returns>
        public static bool CF_IsVolumeResult(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IsVolumeResult(nIndex);
                }
                else
                {
                    return ModEverything64.Everything_IsVolumeResult(nIndex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断指定可见结果是否为目录
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>判断结果</returns>
        public static bool CF_IsFolderResult(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IsFolderResult(nIndex);
                }
                else
                {
                    return ModEverything64.Everything_IsFolderResult(nIndex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 判断指定可见结果是否为文件
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>判断结果</returns>
        public static bool CF_IsFileResult(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IsFileResult(nIndex);
                }
                else
                {
                    return ModEverything64.Everything_IsFileResult(nIndex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取结果完整路径（包括文件名）
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <param name="nMaxCount">缓冲区最大字符数</param>
        /// <returns>完整路径</returns>
        public static string CF_GetResultFullPathName(uint nIndex, uint nMaxCount)
        {
            StringBuilder lpString = new StringBuilder();

            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_GetResultFullPathName(nIndex, lpString, nMaxCount);
                }
                else
                {
                    ModEverything64.Everything_GetResultFullPathName(nIndex, lpString, nMaxCount);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lpString.ToString().Trim();
        }

        /// <summary>
        /// 获得结果路径
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>路径</returns>
        public static string CF_GetResultPath(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetResultPath(nIndex));
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetResultPath(nIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获得结果文件名
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>文件名</returns>
        public static string CF_GetResultFileName(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetResultFileName(nIndex));
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetResultFileName(nIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取结果后缀名
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>后缀名</returns>
        public static string CF_GetResultExtension(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetResultExtension(nIndex));
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetResultExtension(nIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取文件大小（Byte）
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <param name="lpFileSize">文件大小</param>
        /// <returns>执行结果</returns>
        public static bool CF_GetResultSize(uint nIndex, out long lpFileSize)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetResultSize(nIndex, out lpFileSize);
                }
                else
                {
                    return ModEverything64.Everything_GetResultSize(nIndex, out lpFileSize);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取创建时间
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <param name="dtFileTime">创建时间</param>
        /// <returns>执行结果</returns>
        public static bool CF_GetResultDateCreated(uint nIndex, out DateTime dtFileTime)
        {
            bool result;

            try
            {
                long lpDateCreated;
                if (CP_Platform == EPlatform.X86)
                {
                    result = ModEverything32.Everything_GetResultDateCreated(nIndex, out lpDateCreated);
                }
                else
                {
                    result = ModEverything64.Everything_GetResultDateCreated(nIndex, out lpDateCreated);
                }

                dtFileTime = DateTime.FromFileTime(lpDateCreated);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取修改时间
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <param name="dtFileTime">修改时间</param>
        /// <returns>执行结果</returns>
        public static bool CF_GetResultDateModified(uint nIndex, out DateTime dtFileTime)
        {
            bool result;

            try
            {
                long lpDateCreated;
                if (CP_Platform == EPlatform.X86)
                {
                    result = ModEverything32.Everything_GetResultDateModified(nIndex, out lpDateCreated);
                }
                else
                {
                    result = ModEverything64.Everything_GetResultDateModified(nIndex, out lpDateCreated);
                }

                dtFileTime = DateTime.FromFileTime(lpDateCreated);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取访问时间
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <param name="dtFileTime">访问时间</param>
        /// <returns>执行结果</returns>
        public static bool CF_GetResultDateAccessed(uint nIndex, out DateTime dtFileTime)
        {
            bool result;

            try
            {
                long lpDateCreated;
                if (CP_Platform == EPlatform.X86)
                {
                    result = ModEverything32.Everything_GetResultDateAccessed(nIndex, out lpDateCreated);
                }
                else
                {
                    result = ModEverything64.Everything_GetResultDateAccessed(nIndex, out lpDateCreated);
                }

                dtFileTime = DateTime.FromFileTime(lpDateCreated);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取属性
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>属性</returns>
        public static EFileAttribute CF_GetResultAttributes(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return (EFileAttribute)ModEverything32.Everything_GetResultAttributes(nIndex);
                }
                else
                {
                    return (EFileAttribute)ModEverything64.Everything_GetResultAttributes(nIndex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取完整路径和文件名
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>路径和文件名</returns>
        public static string CF_GetResultFileListFileName(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetResultFileListFileName(nIndex));
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetResultFileListFileName(nIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取运行次数
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>运行次数</returns>
        public static uint CF_GetResultRunCount(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetResultRunCount(nIndex);
                }
                else
                {
                    return ModEverything64.Everything_GetResultRunCount(nIndex);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取运行时间
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <param name="dtFileTime">运行时间</param>
        /// <returns>执行结果</returns>
        public static bool CF_GetResultDateRun(uint nIndex, out DateTime dtFileTime)
        {
            bool result;

            try
            {
                long lpDateCreated;
                if (CP_Platform == EPlatform.X86)
                {
                    result = ModEverything32.Everything_GetResultDateRun(nIndex, out lpDateCreated);
                }
                else
                {
                    result = ModEverything64.Everything_GetResultDateRun(nIndex, out lpDateCreated);
                }

                dtFileTime = DateTime.FromFileTime(lpDateCreated);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取最近修改时间
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <param name="dtFileTime">修改时间</param>
        /// <returns>执行结果</returns>
        public static bool CF_GetResultDateRecentlyChanged(uint nIndex, out DateTime dtFileTime)
        {
            bool result;

            try
            {
                long lpDateCreated;
                if (CP_Platform == EPlatform.X86)
                {
                    result = ModEverything32.Everything_GetResultDateRecentlyChanged(nIndex, out lpDateCreated);
                }
                else
                {
                    result = ModEverything64.Everything_GetResultDateRecentlyChanged(nIndex, out lpDateCreated);
                }

                dtFileTime = DateTime.FromFileTime(lpDateCreated);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// 获取高亮的文件名
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>高亮的文件名</returns>
        public static string CF_GetResultHighlightedFileName(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetResultHighlightedFileName(nIndex));
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetResultHighlightedFileName(nIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取高亮的路径
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>高亮的路径</returns>
        public static string CF_GetResultHighlightedPath(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetResultHighlightedPath(nIndex));
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetResultHighlightedPath(nIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取高亮的完整路径与文件名
        /// </summary>
        /// <param name="nIndex">结果序号</param>
        /// <returns>高亮的完整路径与文件名</returns>
        public static string CF_GetResultHighlightedFullPathAndFileName(uint nIndex)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return Marshal.PtrToStringUni(ModEverything32.Everything_GetResultHighlightedFullPathAndFileName(nIndex));
                }
                else
                {
                    return Marshal.PtrToStringUni(ModEverything64.Everything_GetResultHighlightedFullPathAndFileName(nIndex));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取文件运行次数
        /// </summary>
        /// <param name="lpFileName">完整路径</param>
        /// <returns>运行次数</returns>
        public static uint CF_GetRunCountFromFileName(string lpFileName)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetRunCountFromFileName(lpFileName);
                }
                else
                {
                    return ModEverything64.Everything_GetRunCountFromFileName(lpFileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取文件运行次数
        /// </summary>
        /// <param name="lpFileName">完整路径</param>
        /// <param name="dwRunCount">运行次数</param>
        /// <returns>执行结果</returns>
        public static bool CF_SetRunCountFromFileName(string lpFileName, uint dwRunCount)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_SetRunCountFromFileName(lpFileName, dwRunCount);
                }
                else
                {
                    return ModEverything64.Everything_SetRunCountFromFileName(lpFileName, dwRunCount);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 增加指定文件的运行计数一次
        /// </summary>
        /// <param name="lpFileName">完整路径</param>
        /// <returns>运行次数</returns>
        public static uint CF_IncRunCountFromFileName(string lpFileName)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IncRunCountFromFileName(lpFileName);
                }
                else
                {
                    return ModEverything64.Everything_IncRunCountFromFileName(lpFileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 其他操作
        /// <summary>
        /// 设置下次搜索的唯一编号
        /// </summary>
        /// <param name="nId">编号</param>
        public static void CF_SetReplyID(uint nId)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetReplyID(nId);
                }
                else
                {
                    ModEverything64.Everything_SetReplyID(nId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取下次搜索的唯一编号
        /// </summary>
        /// <returns>编号</returns>
        public static uint CF_GetReplyID()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetReplyID();
                }
                else
                {
                    return ModEverything64.Everything_GetReplyID();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 设置下次搜索的窗体句柄
        /// </summary>
        /// <param name="hWnd">窗体句柄</param>
        public static void CF_SetReplyWindow(IntPtr hWnd)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    ModEverything32.Everything_SetReplyWindow(hWnd);
                }
                else
                {
                    ModEverything64.Everything_SetReplyWindow(hWnd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取下次搜索的窗体句柄
        /// </summary>
        /// <returns></returns>
        public static IntPtr CF_GetReplyWindow()
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_GetReplyWindow();
                }
                else
                {
                    return ModEverything64.Everything_GetReplyWindow();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 检测指定的窗口消息是否为查询答复
        /// </summary>
        /// <param name="message">消息标识符</param>
        /// <param name="wParam">消息的其他消息</param>
        /// <param name="lParam">消息的其他消息</param>
        /// <param name="nId">内容标识符</param>
        /// <returns>检测结果</returns>
        public static bool CF_IsQueryReply(uint message, ulong wParam, long lParam, uint nId)
        {
            try
            {
                if (CP_Platform == EPlatform.X86)
                {
                    return ModEverything32.Everything_IsQueryReply(message, wParam, lParam, nId);
                }
                else
                {
                    return ModEverything64.Everything_IsQueryReply(message, wParam, lParam, nId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
