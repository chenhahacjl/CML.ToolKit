namespace CML.CommonEx.EverythingEx
{
    /// <summary>
    /// 文件属性枚举
    /// </summary>
    public enum EFileAttribute : uint
    {
        /// <summary>
        /// 只读
        /// </summary>
        READONLY = 0x00000001,
        /// <summary>
        /// 隐藏
        /// </summary>
        HIDDEN = 0x00000002,
        /// <summary>
        /// 系统
        /// </summary>
        SYSTEM = 0x00000004,
        /// <summary>
        /// 文件夹
        /// </summary>
        DIRECTORY = 0x00000010,
        /// <summary>
        /// 存档
        /// </summary>
        ARCHIVE = 0x00000020,
        /// <summary>
        /// This value is reserved for system use.
        /// </summary>
        DEVICE = 0x00000040,
        /// <summary>
        /// 未设置其他属性
        /// </summary>
        NORMAL = 0x00000080,
        /// <summary>
        /// 临时存储文件
        /// </summary>
        TEMPORARY = 0x00000100,
        /// <summary>
        /// 稀疏文件
        /// </summary>
        SPARSE_FILE = 0x00000200,
        /// <summary>
        /// A file or directory that has an associated reparse point, or a file that is a symbolic link.
        /// </summary>
        REPARSE_POINT = 0x00000400,
        /// <summary>
        /// 压缩
        /// </summary>
        COMPRESSED = 0x00000800,
        /// <summary>
        /// 离线
        /// </summary>
        OFFLINE = 0x00001000,
        /// <summary>
        /// 内容索引服务不为该文件或目录编制索引
        /// </summary>
        NOT_CONTENT_INDEXED = 0x00002000,
        /// <summary>
        /// 加密
        /// </summary>
        ENCRYPTED = 0x00004000,
        /// <summary>
        /// The directory or user data stream is configured with integrity (only supported on ReFS volumes).
        /// </summary>
        INTEGRITY_STREAM = 0x00008000,
        /// <summary>
        /// This value is reserved for system use.
        /// </summary>
        VIRTUAL = 0x00010000,
        /// <summary>
        /// The user data stream not to be read by the background data integrity scanner (AKA scrubber). 
        /// </summary>
        NO_SCRUB_DATA = 0x00020000,
        /// <summary>
        /// This attribute only appears in directory enumeration classes (FILE_DIRECTORY_INFORMATION, FILE_BOTH_DIR_INFORMATION, etc.).
        /// </summary>
        RECALL_ON_OPEN = 0x00040000,
        /// <summary>
        /// When this attribute is set, it means that the file or directory is not fully present locally. 
        /// </summary>
        RECALL_ON_DATA_ACCESS = 0x00400000,
    }
}
