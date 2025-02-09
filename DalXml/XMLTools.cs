﻿using System;
using System.IO;
using System.Xml.Linq;

namespace DalXml
{
    class XMLTools
    {
        private static string dirPath = @"Data\";
        static XMLTools()
        {
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        }

        #region SaveLoadWithXElement
        public static void SaveListToXmlElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(dirPath + filePath);
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        public static XElement LoadListFromXmlElement(string filePath)
        {
            try
            {
                if (File.Exists(dirPath + filePath))
                {
                    return XElement.Load(dirPath + filePath);
                }
                else
                {
                    string rootName = filePath.Split(".")[0];
                    XElement rootElem = new XElement(rootName);
                    rootElem.Save(dirPath + filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion

        //#region SaveLoadWithXMLSerializer
        //public static void SaveListToXmlSerializer<T>(IEnumerable<T> list, string filePath)
        //{
        //    try
        //    {
        //        FileStream file = new FileStream(dirPath + filePath, FileMode.Create);
        //        XmlSerializer x = new XmlSerializer(list.GetType());
        //        x.Serialize(file, list);
        //        file.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DO.XMLFileLoadCreateException(filePath, $"fail to create xml file: {filePath}", ex);
        //    }
        //}
        //public static List<T> LoadListFromXmlSerializer<T>(string filePath)
        //{
        //    try
        //    {
        //        if (File.Exists(dirPath + filePath))
        //        {
        //            List<T> list;
        //            XmlSerializer x = new XmlSerializer(typeof(List<T>));
        //            FileStream file = new FileStream(dirPath + filePath, FileMode.Open);
        //            list = (List<T>)x.Deserialize(file);
        //            file.Close();
        //            return list;
        //        }
        //        else
        //            return new List<T>();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new DO.XMLFileLoadCreateException(filePath, $"fail to load xml file: {filePath}", ex);
        //    }
        //}
        //#endregion
    }
}

