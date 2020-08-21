using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using Newtonsoft.Json;

namespace Consolescheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            string url =  System.Configuration.ConfigurationManager.AppSettings["URLREST"];

            while (true)
            {
                char ch;
                Console.WriteLine("Enter A to refresh and B to Exit");
                ch = Convert.ToChar(Console.ReadLine());

                if (ch == 'A')
                {
                    string details = CallRestMethod(url);
                    List<Messages> list = JsonConvert.DeserializeObject<List<Messages>>(details);
                    Console.WriteLine("Message Details");

                    foreach (Messages msg in list)
                    {
                        if (msg.delivered_dt > DateTime.Now)
                        {
                            Console.WriteLine("ID :" + msg.Id.ToString());
                            Console.WriteLine("Message :" + msg.mes_content);
                            Console.WriteLine("Delivery Date and Time :" + msg.delivered_dt.ToString());
                            Console.WriteLine("-------------------------------");
                            break;
                        }
                    }
                }
                if (ch == 'B')
                    break;

            }            
        }

        public static string CallRestMethod(string url)
        {
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.Method = "GET";
            webrequest.ContentType = "application/x-www-form-urlencoded";
            //webrequest.Headers.Add("Username", "xyz");
            //webrequest.Headers.Add("Password", "abc");
            HttpWebResponse webresponse = (HttpWebResponse)webrequest.GetResponse();
            Encoding enc = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader responseStream = new StreamReader(webresponse.GetResponseStream(), enc);
            string result = string.Empty;
            result = responseStream.ReadToEnd();
            webresponse.Close();
            return result;
        }









    }
}
