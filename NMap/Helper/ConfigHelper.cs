﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Reflection;
using NMap.Model;

namespace NMap.Helper
{
    class ConfigHelper
    {
        private static string _xmlDirectory = Path.GetDirectoryName(
                                              Assembly.GetExecutingAssembly().GetModules()[0].FullyQualifiedName) +
                                              @"\..\Parameter Files\NMap";
        private static string _xmlPath = _xmlDirectory + @"\legends.xml";

        public void CreateConfigFile(List<Legend> legend)
        {
            XDocument xdoc = new XDocument();
            xdoc.Declaration = new XDeclaration("1.0", "utf-8", "no"); 

            XElement root = new XElement("Config");

            XElement element = new XElement("Map");
            element.SetAttributeValue("ShowGrid", "On");
            element.SetAttributeValue("BottomAxes", "CD");
            element.SetAttributeValue("MDInverse", false);
            element.SetAttributeValue("CDInverse", false);
            root.Add(element);

            foreach (var item in legend)
            {
                XElement el = new XElement("Legend");
                el.SetAttributeValue("ClassID", item.ClassID);
                el.SetAttributeValue("Name", item.Name);
                el.SetAttributeValue("Color", item.Color);
                el.SetAttributeValue("Shape", "Circle");
                root.Add(el);
            }
            xdoc.Add(root);
            if (!Directory.Exists(_xmlDirectory))
            {
                Directory.CreateDirectory(_xmlDirectory);
            }
            xdoc.Save(_xmlPath);
        }

        public Config GetConfigFile()
        {
            Config config = new Config() { Legends = new List<Legend>() };

            XDocument xdoc = XDocument.Load(_xmlPath);

            // Get map setting
            XElement xmlMap = xdoc.Root.Elements("Map").FirstOrDefault();
            config.ShowMapGrid = xmlMap.Attribute("ShowGrid").Value;
            config.BottomAxes = xmlMap.Attribute("BottomAxes").Value;
            config.MDInverse = Convert.ToBoolean(xmlMap.Attribute("MDInverse").Value);
            config.CDInverse = Convert.ToBoolean(xmlMap.Attribute("CDInverse").Value);

            // Get legend setting
            foreach (var legend in xdoc.Root.Elements("Legend"))
            {
                config.Legends.Add(new Legend()
                {
                    ClassID = legend.Attribute("ClassID").Value,
                    Name = legend.Attribute("Name").Value,
                    Color = legend.Attribute("Color").Value,
                    Shape = legend.Attribute("Shape").Value
                });
            }
            return config;
        }
    }
}
