using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Modules
{
    public class Data
    {
        public string mNo
        {
            set;
            get;
        }

        public string nTitle
        {
            set;
            get;
        }

        public string nContents
        {
            set;
            get;
        }
        public string nNo
        {
            set;
            get;
        }
    }

    public static class Query
    {
        public static ArrayList GetInsert(Data data)
        {
            MYsql my = new MYsql();
            string sql = string.Format("INSERT INTO Notice (mNo,nTitle,nContents) VALUES ({0},'{1}','{2}');", data.mNo, data.nTitle, data.nContents);
            if (my.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetUpdate(Data data)
        {
            MYsql my = new MYsql();
            string sql = string.Format("update Notice SET nTitle = '{0}',nContents='{1}' WHERE nNo = '{2}';", data.nTitle, data.nContents, data.nNo);
            if (my.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetDelete(Data data)
        {
            MYsql my = new MYsql();
            string sql = string.Format("update Notice SET delYn = 'Y' WHERE nNo = '{0}';", data.nNo);
            if (my.NonQuery(sql))
            {
                return GetSelect();
            }
            else
            {
                return new ArrayList();
            }
        }

        public static ArrayList GetSelect()
        {
            MYsql my = new MYsql();
            string sql = string.Format("select n.nNo,n.nTitle,n.nContents,m.mName,DATE_FORMAT(n.regDate, '%Y-%m-%d') as regDate,DATE_FORMAT(n.modDate, '%Y-%m-%d') as modDate from Notice as n  inner join Member as m  on (n.mNo = m.mNo and m.delYn = 'N') where n.delYn = 'N';");
            MySqlDataReader sdr = my.Reader(sql);
            ArrayList list = new ArrayList();
            while (sdr.Read())
            {
                Hashtable ht = new Hashtable();
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    ht.Add(sdr.GetName(i), sdr.GetValue(i));
                }
                list.Add(ht);
            }
            return list;
        }
    }
}
