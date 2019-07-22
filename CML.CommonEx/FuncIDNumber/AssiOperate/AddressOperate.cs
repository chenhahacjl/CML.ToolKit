using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace CML.CommonEx.IDNumberEx
{
    /// <summary>
    /// 地址信息模型操作类
    /// </summary>
    internal class AddressOperate
    {
        /// <summary>
        /// 获得所有地址模型
        /// </summary>
        /// <returns></returns>
        public static ModAddress[] GetAllAddressModel()
        {
            List<ModAddress> address = new List<ModAddress>();

            try
            {
                string file = "CML.CommonEx.FuncIDNumber.AssiResource.AddressInfo.TXT";

                if (new List<string>(Assembly.GetExecutingAssembly().GetManifestResourceNames()).Contains(file))
                {
                    using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(file))
                    {
                        using (StreamReader sr = new StreamReader(stream, Encoding.UTF8))
                        {
                            while (!sr.EndOfStream)
                            {
                                string[] addInfo = sr.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                                if (addInfo.Length != 3) { continue; }
                                address.Add(new ModAddress(addInfo[0], addInfo[1], addInfo[2]));
                            }
                        }
                    }
                }
            }
            catch { }

            return address.ToArray();
        }
    }
}
