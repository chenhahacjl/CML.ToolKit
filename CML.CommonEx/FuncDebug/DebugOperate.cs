using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace CML.CommonEx.DebugEx
{
    /// <summary>
    /// 调试操作类
    /// </summary>
    public class DebugOperate
    {
        /// <summary>
        /// 打开调试DLL文件
        /// </summary>
        /// <param name="dllFilePath">DLL文件路径</param>
        /// <param name="modelID">模块识别码（留空不筛选）</param>
        /// <param name="projectObj">项目实例</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        /// <returns>执行结果</returns>
        public static bool OpenDebugDll(string dllFilePath, string modelID, object projectObj, out string errMsg)
        {
            bool result = true;

            try
            {
                //开发接口类型
                Type baseType = typeof(IDebugDev);

                //加载DLL程序集
                Assembly assembly = Assembly.Load(File.ReadAllBytes(dllFilePath));

                //获取达成契约类型
                Type[] arrTypes = assembly.GetTypes().AsEnumerable().Where(item => item != baseType && item.GetInterface(baseType.Name) != null).ToArray();

                //查询项目ID
                List<object> lstDebugModel = new List<object>();
                foreach (var type in arrTypes)
                {
                    object instance = Activator.CreateInstance(type);
                    SetProperty(instance, "ProjectObject", projectObj);

                    if (string.IsNullOrEmpty(modelID))
                    {
                        lstDebugModel.Add(instance);
                    }
                    else
                    {
                        string innerModelID = GetProperty(instance, "ModelID") as string;
                        if (string.IsNullOrEmpty(innerModelID) || modelID == innerModelID)
                        {
                            lstDebugModel.Add(instance);
                        }
                    }
                }

                using (FormModelSelect modelSelect = new FormModelSelect(lstDebugModel))
                {
                    if (modelSelect.ShowDialog() == DialogResult.OK)
                    {
                        InvokeMethod(lstDebugModel[modelSelect.SelectedIndex], "ExecuteDebug", null);
                    }
                }

                errMsg = "";
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取变量值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="fieldName">变量名</param>
        /// <returns>变量值</returns>
        public static object GetField(object instance, string fieldName)
        {
            return instance?.GetType()?.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(instance);
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="fieldName">变量名</param>
        /// <param name="value">变量值</param>
        public static void SetField(object instance, string fieldName, object value)
        {
            SetField(instance, fieldName, value, out _);
        }

        /// <summary>
        /// 设置变量值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="fieldName">变量名</param>
        /// <param name="value">变量值</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        public static bool SetField(object instance, string fieldName, object value, out string errMsg)
        {
            bool result = true;
            try
            {
                instance?.GetType()?.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.SetValue(instance, value);

                errMsg = "";
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propName">属性名</param>
        /// <returns>属性值</returns>
        public static object GetProperty(object instance, string propName)
        {
            return instance?.GetType()?.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.GetValue(instance, null);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propName">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetProperty(object instance, string propName, object value)
        {
            SetProperty(instance, propName, value, out _);
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="propName">属性名</param>
        /// <param name="value">属性值</param>
        /// <param name="errMsg">[OUT]错误信息</param>
        public static bool SetProperty(object instance, string propName, object value, out string errMsg)
        {
            bool result = true;
            try
            {
                instance?.GetType()?.GetProperty(propName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.SetValue(instance, value, null);

                errMsg = "";
            }
            catch (Exception ex)
            {
                result = false;
                errMsg = ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 调用动态方法
        /// </summary>
        /// <param name="instance">对象实例</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">调用参数</param>
        public static void InvokeMethod(object instance, string methodName, object[] parameters)
        {
            try
            {
                instance?.GetType()?.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.Invoke(instance, parameters);
            }
            catch { }
        }

        /// <summary>
        /// 调用静态方法
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="methodName">方法名</param>
        /// <param name="parameters">调用参数</param>
        public static void InvokeMethod(Type type, string methodName, object[] parameters)
        {
            try
            {
                type?.GetMethod(methodName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)?.Invoke(null, parameters);
            }
            catch { }
        }
    }
}
