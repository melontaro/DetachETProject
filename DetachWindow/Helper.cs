using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DetachWindow
{
    public class Helper
    {


        public static void Detach(string ETRoot)
        {

            var serverPath = Path.Combine(ETRoot, "Server");

            string[] filedir = Directory.GetFiles(serverPath, "*.csproj", SearchOption.AllDirectories);
            for (int i = 0; i < filedir.Length; i++)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filedir[i]);
                DirectoryInfo folder = new DirectoryInfo(filedir[i]);
                XmlNodeList schoolNodeList = doc.SelectNodes("/Project");
                if (schoolNodeList == null)
                {
                    continue;
                }

                for (int j = 0; j < schoolNodeList.Count; j++)
                {
                    XmlNode xnode = schoolNodeList.Item(j);
                    XmlNodeList xnode2 = xnode.ChildNodes;
                    for (int k = 0; k < xnode2.Count; k++)
                    {
                        XmlNode node1 = xnode2.Item(k);
                        XmlNodeList xnode3 = node1.ChildNodes;

                        for (int l = 0; l < xnode3.Count; l++)
                        {

                            XmlNode item0 = xnode3.Item(l);


                            XmlAttributeCollection attributes = xnode3?.Item(l)?.Attributes;
                            for (int m = 0; m < attributes.Count; m++)
                            {

                                if (attributes != null && attributes.Count > 0 && attributes[m].Name == "Include" && xnode3?.Item(l)?.Name == "Compile" && xnode3?.Item(l).OuterXml.Contains("Unity") == true)
                                {
                                    if (item0.ChildNodes.Count == 0)
                                    {
                                        string serverFile = attributes[1].Value;
                                        string fullServerPath = Path.Combine(folder.Parent.FullName, serverFile);
                                        DirectoryInfo directoryInfo = new DirectoryInfo(fullServerPath);

                                        if (!directoryInfo.Parent.Exists)
                                        {
                                            Directory.CreateDirectory(directoryInfo.Parent.FullName);
                                        }

                                        string unityPath = attributes[0].Value;
                                        unityPath = unityPath.Replace("..\\", "");
                                        string unityFullPath = Path.Combine(ETRoot, unityPath);
                                        FileInfo serverflinfo = new FileInfo(fullServerPath);
                                        if (!serverflinfo.Exists)
                                        {
                                            FileInfo flinfo = new FileInfo(unityFullPath);
                                            flinfo.CopyTo(fullServerPath, overwrite: false);
                                            // Console.WriteLine(fullServerPath);
                                        }

                                    }
                                    else
                                    {
                                        for (int n = 0; n < item0.ChildNodes.Count; n++)
                                        {

                                            var node4 = item0.ChildNodes[n];
                                            string serverFile = node4.InnerText;
                                            string fullServerPath = Path.Combine(folder.Parent.FullName, serverFile);
                                            DirectoryInfo directoryInfo = new DirectoryInfo(fullServerPath);
                                            //Console.WriteLine(fullServerPath);


                                            if (!directoryInfo.Parent.Exists)
                                            {
                                                Directory.CreateDirectory(directoryInfo.Parent.FullName);
                                            }
                                            string unityPath = attributes[m].InnerText;
                                            unityPath = unityPath.Replace("..\\", "");
                                            string unityFullPath = Path.Combine(ETRoot, unityPath);
                                            FileInfo serverflinfo = new FileInfo(fullServerPath);
                                            if (!serverflinfo.Exists)
                                            {
                                                FileInfo flinfo = new FileInfo(unityFullPath);
                                                flinfo.CopyTo(fullServerPath, overwrite: false);
                                                // Console.WriteLine(fullServerPath);
                                            }

                                        }
                                    }
                                }
                            }


                        }

                        for (int l = 0; l < xnode3.Count; l++)
                        {
                            if (xnode3?.Item(l).OuterXml.Contains("Unity") == true)
                            {
                                node1.RemoveAll();
                                break;
                            }

                        }

                    }
                }

                var xmlPath = Path.Combine(filedir[i]);
                doc.Save(xmlPath);
            }

        }

        public static void Detach4BD(string ETRoot)
        {

            var serverPath = Path.Combine(ETRoot, "Server");

            string[] filedir = Directory.GetFiles(serverPath, "*.csproj", SearchOption.AllDirectories);
            for (int i = 0; i < filedir.Length; i++)
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(filedir[i]);
                DirectoryInfo folder = new DirectoryInfo(filedir[i]);
                XmlNodeList schoolNodeList = doc.SelectNodes("/Project");
                if (schoolNodeList == null)
                {
                    continue;
                }

                for (int j = 0; j < schoolNodeList.Count; j++)
                {
                    XmlNode xnode = schoolNodeList.Item(j);
                    XmlNodeList xnode2 = xnode.ChildNodes;
                    for (int k = 0; k < xnode2.Count; k++)
                    {
                        XmlNode node1 = xnode2.Item(k);
                        XmlNodeList xnode3 = node1.ChildNodes;

                        for (int l = 0; l < xnode3.Count; l++)
                        {

                            XmlNode item0 = xnode3.Item(l);


                            XmlAttributeCollection attributes = xnode3?.Item(l)?.Attributes;
                            for (int m = 0; m < attributes.Count; m++)
                            {

                                if (attributes != null && attributes.Count > 0 && attributes[m].Name == "Include" && xnode3?.Item(l)?.Name == "Compile" && xnode3?.Item(l).OuterXml.Contains("Unity") == true)
                                {
                                    if (item0.ChildNodes.Count == 0)
                                    {
                                        string serverFile = attributes[1].Value;
                                        string fullServerPath = Path.Combine(folder.Parent.FullName, serverFile);
                                        DirectoryInfo directoryInfo = new DirectoryInfo(fullServerPath);

                                        if (!directoryInfo.Parent.Exists)
                                        {
                                            Directory.CreateDirectory(directoryInfo.Parent.FullName);
                                        }

                                        string unityPath = attributes[0].Value;
                                        if (unityPath.Contains(@"Unity\Assets\Model"))
                                        {
                                            unityPath = unityPath.Replace(@"Unity\Assets\Model", @"BDFramework.Core\Assets\ET\Model");
                                        }

                                        if (unityPath.Contains(@"Unity\Assets\Hotfix"))
                                        {
                                            unityPath = unityPath.Replace(@"Unity\Assets\Hotfix", @"BDFramework.Core\Assets\ET\@hotfix");
                                        }

                                        if (unityPath.Contains(@"Unity\Assets\ThirdParty"))
                                        {
                                            unityPath = unityPath.Replace(@"Unity\Assets\ThirdParty", @"BDFramework.Core\Assets\ET\ThirdParty");
                                        }
                                        if (unityPath.Contains(@"Unity\Assets\Plugins"))
                                        {
                                            unityPath = unityPath.Replace(@"Unity\Assets\Plugins", @"BDFramework.Core\Assets\Plugins");
                                        }
                                        attributes[0].Value = unityPath;


                                    }
                                    else
                                    {
                                        for (int n = 0; n < item0.ChildNodes.Count; n++)
                                        {

                                            var node4 = item0.ChildNodes[n];
                                            string serverFile = node4.InnerText;
                                            string fullServerPath = Path.Combine(folder.Parent.FullName, serverFile);
                                            DirectoryInfo directoryInfo = new DirectoryInfo(fullServerPath);
                                            //Console.WriteLine(fullServerPath);


                                            if (!directoryInfo.Parent.Exists)
                                            {
                                                Directory.CreateDirectory(directoryInfo.Parent.FullName);
                                            }
                                            string unityPath = attributes[m].InnerText;

                                            if (unityPath.Contains(@"Unity\Assets\Model"))
                                            {
                                                unityPath = unityPath.Replace(@"Unity\Assets\Model", @"BDFramework.Core\Assets\ET\Model");
                                            }

                                            if (unityPath.Contains(@"Unity\Assets\Hotfix"))
                                            {
                                                unityPath = unityPath.Replace(@"Unity\Assets\Hotfix", @"BDFramework.Core\Assets\ET\@hotfix");
                                            }
                                            if (unityPath.Contains(@"Unity\Assets\ThirdParty"))
                                            {
                                                unityPath = unityPath.Replace(@"Unity\Assets\ThirdParty", @"BDFramework.Core\Assets\ET\ThirdParty");
                                            }
                                            if (unityPath.Contains(@"Unity\Assets\Plugins"))
                                            {
                                                unityPath = unityPath.Replace(@"Unity\Assets\Plugins", @"BDFramework.Core\Assets\Plugins");
                                            }
                                            attributes[m].InnerText = unityPath;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                var xmlPath = Path.Combine(filedir[i]);
                doc.Save(xmlPath);
            }

        }


        /// <summary>
        /// 把一个文件夹下所有文件复制到另一个文件夹下 
        /// </summary>
        /// <param name="sourceDirectory">源目录</param>
        /// <param name="targetDirectory">目标目录</param>
        public static bool DirectoryCopy(string sourceDirectory, string targetDirectory)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirectory);
                //获取目录下（不包含子目录）的文件和子目录
                FileSystemInfo[] fileinfo = dir.GetFileSystemInfos();
                foreach (FileSystemInfo i in fileinfo)
                {
                    if (i is DirectoryInfo)     //判断是否文件夹
                    {
                        if (!Directory.Exists(targetDirectory + "\\" + i.Name))
                        {
                            //目标目录下不存在此文件夹即创建子文件夹
                            Directory.CreateDirectory(targetDirectory + "\\" + i.Name);
                        }
                        //递归调用复制子文件夹
                        DirectoryCopy(i.FullName, targetDirectory + "\\" + i.Name);
                    }
                    else
                    {
                        //不是文件夹即复制文件，true表示可以覆盖同名文件
                        File.Copy(i.FullName, targetDirectory + "\\" + i.Name, true);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static string Readjson(string jsonfile, string key)
        {
            StreamReader file = File.OpenText(jsonfile);
            JsonTextReader reader = new JsonTextReader(file);
            JObject o = (JObject)JToken.ReadFrom(reader);
            return o[key].ToString();
        }
    }
}
