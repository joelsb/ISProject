using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using static App_clienntA.ClientA;

public class Network
{
    public static string defaultNamespace = "http://schemas.datacontract.org/2004/07/Somiod.Models";
    public static string baseUrl = @"http://localhost:44382/api/somiod";
    public Network()
    {
    }
    public static XmlDocument POST(string url, object model)
    {
        return request(url, "POST", model);
    }
    public static XmlDocument PUT(string url, object model)
    {
        return request(url, "PUT", model);
    }
    public static XmlDocument GET(string url)
    {
        return request(url, "GET");
    }
    public static XmlDocument DELETE(string url)
    {
        return request(url, "DELETE");
    }
    private static XmlDocument request(string url, string method, object model = null)
    {
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = method;
        request.ContentType = @"application/xml";
        if (model != null)
        {
            string xml;
            using (StringWriter stringWriter = new Utf8StringWriter()) // Use Utf8StringWriter instead of StringWriter
            {
                new XmlSerializer(model.GetType(), defaultNamespace).Serialize(stringWriter, model);
                xml = stringWriter.ToString();
            }
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            Byte[] byteArray = encoding.GetBytes(xml);
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }
        }
        else
        {
            request.ContentLength = 0;
        }
        long length = 0;
        try
        {
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(responseStream);

                    return xmlDoc;

                }
            }
        }
        catch (WebException ex)
        {
            // Log exception and throw as for GET example above
            return null;
        }
    }
}
