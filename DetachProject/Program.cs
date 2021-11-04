using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;
using Newtonsoft.Json.Linq;

namespace DetachProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string rootPath = Directory.GetCurrentDirectory();
            string jsongfile = Path.Combine(rootPath, "config.json");
            string valueRoot = Readjson(jsongfile, "RootPath");
            DirectoryInfo directorySverInfo = new DirectoryInfo(valueRoot);
            string ETRoot = directorySverInfo.Parent.FullName;
            string[] filedir = Directory.GetFiles(valueRoot, "*.csproj", SearchOption.AllDirectories);
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

        public static string Readjson(string jsonfile, string key)
        {
            using StreamReader file = File.OpenText(jsonfile);
            using JsonTextReader reader = new JsonTextReader(file);
            JObject o = (JObject)JToken.ReadFrom(reader);
            return o[key]!.ToString();
        }
    }
}