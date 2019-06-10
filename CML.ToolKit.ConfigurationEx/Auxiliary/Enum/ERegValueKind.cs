namespace CML.ToolKit.ConfigurationEx
{
    /// <summary>
    /// 注册表数据类型
    /// </summary>
    public enum ERegValueKind
    {
        /// <summary>
        /// 不受支持的注册表数据类型。
        /// 例如，不支持 Microsoft Win32 API 注册表数据类型 REG_RESOURCE_LIST，使用此值指定
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// 以 Null 结尾的字符串。
        /// 此值与 Win32 API 注册表数据类型 REG_SZ 等效。
        /// </summary>
        String = 1,
        /// <summary>
        /// 以 NULL 结尾的字符串，该字符串中包含对环境变量（如 %PATH%，当值被检索时，就会展开）的未展开的引用。
        /// 此值与 Win32 API注册表数据类型 REG_EXPAND_SZ 等效。
        /// </summary>
        ExpandString = 2,
        /// <summary>
        /// 任意格式的二进制数据。
        /// 此值与 Win32 API 注册表数据类型 REG_BINARY 等效。
        /// </summary>
        Binary = 3,
        /// <summary>
        /// 32 位二进制数。
        /// 此值与 Win32 API 注册表数据类型 REG_DWORD 等效。
        /// </summary>
        DWord = 4,
        /// <summary>
        /// 以 NULL 结尾的字符串数组，以两个空字符结束。
        /// 此值与 Win32 API 注册表数据类型 REG_MULTI_SZ 等效。
        /// </summary>
        MultiString = 5,
        /// <summary>
        /// 64 位二进制数。
        /// 此值与 Win32 API 注册表数据类型 REG_QWORD 等效。
        /// </summary>
        QWord = 6,
    }
}
