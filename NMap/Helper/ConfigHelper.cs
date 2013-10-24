using System;
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
                                              @"\..\PlugIns\NMap";
        private static string _xmlPath = _xmlDirectory + @"\legends.xml";

        public void CreateConfigFile(Config config)
        {
            XDocument xdoc = new XDocument();
            xdoc.Declaration = new XDeclaration("1.0", "utf-8", "no"); 

            XElement root = new XElement("Config");

            XElement element = new XElement("Map");
            element.SetAttributeValue("ShowGrid", config.ShowMapGrid ?? "On");
            element.SetAttributeValue("BottomAxes", config.BottomAxes ?? "CD");
            element.SetAttributeValue("MDInverse", config.MDInverse);
            element.SetAttributeValue("CDInverse", config.CDInverse);
            element.SetAttributeValue("BackgroundColor", config.BackgroundColor ?? "#FFFFFF");
            element.SetAttributeValue("GridColor", config.GridColor ?? "#AFAFAF");
            element.SetAttributeValue("XPrecision", config.XPrecision);
            element.SetAttributeValue("YPrecision", config.YPrecision);
            element.SetAttributeValue("ScrollingRange", config.ScrollingRange ?? "");
            root.Add(element);

            foreach (var item in config.Legends)
            {
                XElement el = new XElement("Legend");
                el.SetAttributeValue("ClassID", item.ClassID);
                el.SetAttributeValue("Name", item.Name);
                el.SetAttributeValue("Color", item.Color);
                el.SetAttributeValue("Shape", item.Shape);
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
            // Check config exist
            if (!Directory.Exists(_xmlDirectory) || !File.Exists(_xmlPath))
            {
                return new Config() {
                    BackgroundColor = "#FFFFFF",
                    GridColor = "#AFAFAF",
                    XPrecision = "0",
                    YPrecision = "0",
                    Legends = new List<Legend>()
                };
            }

            Config config = new Config() { Legends = new List<Legend>() };

            XDocument xdoc = XDocument.Load(_xmlPath);

            // Get map setting
            XElement xmlMap = xdoc.Root.Elements("Map").FirstOrDefault();
            config.ShowMapGrid = xmlMap.Attribute("ShowGrid").Value;
            config.BottomAxes = xmlMap.Attribute("BottomAxes").Value;
            config.MDInverse = Convert.ToBoolean(xmlMap.Attribute("MDInverse").Value);
            config.CDInverse = Convert.ToBoolean(xmlMap.Attribute("CDInverse").Value);
            config.BackgroundColor = xmlMap.Attribute("BackgroundColor") == null ? "#FFFFFF" : xmlMap.Attribute("BackgroundColor").Value;
            config.GridColor = xmlMap.Attribute("GridColor") == null ? "#AFAFAF" : xmlMap.Attribute("GridColor").Value;
            config.XPrecision = xmlMap.Attribute("XPrecision") == null ? "0" : xmlMap.Attribute("XPrecision").Value;
            config.YPrecision = xmlMap.Attribute("YPrecision") == null ? "0" : xmlMap.Attribute("YPrecision").Value;
            config.ScrollingRange = xmlMap.Attribute("ScrollingRange") == null ? "" : xmlMap.Attribute("ScrollingRange").Value;

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
