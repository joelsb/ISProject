using System;

public class Network
{
    public Network()
    {
    }
    public static bool POST()
    {
        return request(url, "POST", xml);
    }
    public static bool PUT()
    {
        return request(url, "PUT", xml);
    }
    public static bool GET()
    {
        return request(url, "GET");
    }
    public static bool DELETE()
    {
        return request(url, "DELETE");
    }
    private static bool request(string url, string method, string xml = null)
    {
        
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = method;
        if (xml != null)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();

            Byte[] byteArray = encoding.GetBytes(xml);
            request.ContentLength = byteArray.Length;
            request.ContentType = @"application/xml";

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
                length = response.ContentLength;
                return true;
            }
        }
        catch (WebException ex)
        {
            // Log exception and throw as for GET example above
            return false;
        }

    }
}
