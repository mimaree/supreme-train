using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;

public static class XmlSupport
{
    public static ConvertXML LoadFromFile(string fileName)
    {
        using (var stream = new FileStream(fileName, FileMode.Open))
        {
            var XML = new XmlSerializer(typeof(ConvertXML));
            return (ConvertXML)XML.Deserialize(stream);
        }
    }
}
