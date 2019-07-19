using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CML.CommonEx.EverythingEx
{
    /// <summary>
    /// 64位外部方法引用
    /// </summary>
    internal class ModelEverything64
    {
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern uint Everything_SetSearchW(string lpSearchString);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchPath(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchCase(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMatchWholeWord(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRegex(bool bEnable);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetMax(uint dwMax);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetOffset(uint dwOffset);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchPath();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchCase();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetMatchWholeWord();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetRegex();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetMax();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetOffset();
        [DllImport("Everything64.dll")]
        public static extern IntPtr Everything_GetSearchW();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetLastError();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_QueryW(bool bWait);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SortResultsByPath();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetNumFileResults();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetNumFolderResults();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetNumResults();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetTotFileResults();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetTotFolderResults();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetTotResults();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsVolumeResult(uint nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFolderResult(uint nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsFileResult(uint nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern void Everything_GetResultFullPathName(uint nIndex, StringBuilder lpString, uint nMaxCount);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultPath(uint nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileName(uint nIndex);
        [DllImport("Everything64.dll")]
        public static extern void Everything_Reset();
        [DllImport("Everything64.dll")]
        public static extern void Everything_CleanUp();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetMajorVersion();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetMinorVersion();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetRevision();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetBuildNumber();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_Exit();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsDBLoaded();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsAdmin();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsAppData();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_RebuildDB();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_UpdateAllFolderIndexes();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_SaveDB();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_SaveRunHistory();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_DeleteRunHistory();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetTargetMachine();
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetSort(uint dwSortType);
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetSort();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetResultListSort();
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetRequestFlags(uint dwRequestFlags);
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetRequestFlags();
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetResultListRequestFlags();
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultExtension(uint nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultSize(uint nIndex, out long lpFileSize);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateCreated(uint nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateModified(uint nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateAccessed(uint nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetResultAttributes(uint nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultFileListFileName(uint nIndex);
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetResultRunCount(uint nIndex);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateRun(uint nIndex, out long lpFileTime);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_GetResultDateRecentlyChanged(uint nIndex, out long lpFileTime);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFileName(uint nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedPath(uint nIndex);
        [DllImport("Everything64.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr Everything_GetResultHighlightedFullPathAndFileName(uint nIndex);
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetRunCountFromFileName(string lpFileName);
        [DllImport("Everything64.dll")]
        public static extern bool Everything_SetRunCountFromFileName(string lpFileName, uint dwRunCount);
        [DllImport("Everything64.dll")]
        public static extern uint Everything_IncRunCountFromFileName(string lpFileName);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetReplyID(uint nId);
        [DllImport("Everything64.dll")]
        public static extern void Everything_SetReplyWindow(IntPtr hWnd);
        [DllImport("Everything64.dll")]
        public static extern uint Everything_GetReplyID();
        [DllImport("Everything64.dll")]
        public static extern IntPtr Everything_GetReplyWindow();
        [DllImport("Everything64.dll")]
        public static extern bool Everything_IsQueryReply(uint message, ulong wParam, long lParam, uint nId);
    }
}
