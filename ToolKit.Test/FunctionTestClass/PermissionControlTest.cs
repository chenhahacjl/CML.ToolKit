using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CML.CommonEx.PermissionControlEx;

namespace ToolKit.Test
{
    /// <summary>
    /// 权限控制测试类
    /// </summary>
    internal class PermissionControlTest : ToolKitTestBase
    {
        /// <summary>
        /// 测试类名
        /// </summary>
        public override string TestClassName => "PermissionControlTest";

        /// <summary>
        /// 版本信息
        /// </summary>
        public override void CF_GetVersionInfo()
        {
            PrintMsgLn(MsgType.Success, "⊙日志信息⊙");
            PrintMsgLn(MsgType.Info, new VersionInfo().CF_GetVersionInfo());
        }

        /// <summary>
        /// 执行测试
        /// </summary>
        public override void ExecuteTest()
        {
            ModDiskPermission diskPermission = new ModDiskPermission("测试数据")
            {
                VolumeLabel = "HELLO_WORLD",
                EnableModelControl = true,
                EnableSerialNumberControl = true,
            };

            bool result = DiskControl.CF_SetPermission(new System.IO.DriveInfo("G"), diskPermission, out string errMsg);
            if (result)
            {
                PrintLogLn(MsgType.Success, "权限设置成功");
            }
            else
            {
                PrintLogLn(MsgType.Error, "权限设置失败: " + errMsg);
            }

            result = DiskControl.CF_IsExistPermission(new System.IO.DriveInfo("G"));
            if (result)
            {
                PrintLogLn(MsgType.Success, "已设置权限控制");
            }
            else
            {
                PrintLogLn(MsgType.Warn, "未设置权限控制");
            }

            int count = 5;
            while (count-- > 0)
            {
                PrintLogLn(MsgType.Info, $"第{5 - count }/{5}次验证");
                result = DiskControl.CF_CheckPermission(diskPermission, out errMsg);
                if (result)
                {
                    PrintLogLn(MsgType.Success, "权限验证成功");
                }
                else
                {
                    PrintLogLn(MsgType.Error, "权限验证失败: " + errMsg);
                }
                PrintLogLn(MsgType.Info, "任意键继续");
                Console.ReadKey(true);
            }

            result = DiskControl.CF_ClearPermission(new System.IO.DriveInfo("G"), out errMsg);
            if (result)
            {
                PrintLogLn(MsgType.Success, "权限清除成功");
            }
            else
            {
                PrintLogLn(MsgType.Error, "权限清除失败: " + errMsg);
            }
        }
    }
}
