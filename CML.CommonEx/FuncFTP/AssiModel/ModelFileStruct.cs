using System;

namespace CML.CommonEx.FTPEx
{
    /// <summary>
    /// 文件信息
    /// </summary>
    public class ModelFileInfo
    {
        /// <summary>
        /// 是否为文件夹
        /// </summary>
        public bool IsDirectory { get; }
        /// <summary>
        /// 文件（夹）名称
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// 文件大小（文件夹为空）
        /// </summary>
        public long Length { get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; }
        /// <summary>
        /// 文件（夹）路径
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="isDirectory">是否为文件夹</param>
        /// <param name="name">文件（夹）名称</param>
        /// <param name="length">文件大小（文件夹为0）</param>
        /// <param name="createTime">创建时间</param>
        /// <param name="path">文件（夹）路径</param>
        public ModelFileInfo(bool isDirectory, string name, long length, DateTime createTime, string path)
        {
            IsDirectory = isDirectory;
            Name = name;
            Length = length;
            CreateTime = createTime;
            Path = path;
        }
    }
}
