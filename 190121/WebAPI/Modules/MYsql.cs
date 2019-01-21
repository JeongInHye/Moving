using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Modules
{
    public class MYsql
    {
        private MySqlConnection conn;

        public MYsql()
        {
            conn = GetConnection();
        }

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

        public bool ConnectionClose()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool NonQuery(string sql)
        {
            try
            {
                if (conn != null)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    comm.ExecuteNonQuery();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public MySqlDataReader Reader(string sql)
        {
            try
            {
                if (conn != null)
                {
                    MySqlCommand comm = new MySqlCommand(sql, conn);
                    return comm.ExecuteReader();
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public void ReaderClose(MySqlDataReader reader)
        {
            reader.Close();
        }
    }
}
