using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.IO;

namespace ClassLibrary
{
    public class DataBase
    {
        public MySqlConnection GetConnection()
        {
            try
            {
                MySqlConnection conn = new MySqlConnection();

                string path = "/public/DBInfo.json";
                StreamReader sr = new StreamReader(path);
                string result = sr.ReadToEnd();

                JObject j = JsonConvert.DeserializeObject<JObject>(result);
                Hashtable hashtable = new Hashtable();
                foreach (JProperty col in j.Properties())
                {
                    hashtable.Add(col.Name, col.Value);
                }

                string strConnection1 = string.Format("server={0};user={1};password={2};database={3}", hashtable["server"], hashtable["user"], hashtable["password"], hashtable["database"]);

                conn.ConnectionString = strConnection1;
                conn.Open();
                return conn;
            }
            catch (MySqlException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
