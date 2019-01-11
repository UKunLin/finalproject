
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YC.Models;

namespace YC.Repository
{
    public class OpenDataRepository: IOpenDataRepository
    {
        public string ConnectionString
        {
            get
            {
                var path=System.IO.Path.Combine(Directory.GetCurrentDirectory(), @"App_Data\Database.mdf");

                return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={path};Integrated Security=True";
                //return @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename="+ Directory.GetCurrentDirectory() + @"\App_Data\nodeDB.mdf;Integrated Security=True";
            }
            
        }

        public List<OpenData> SelectAll(string name)
        {
            var result = new List<OpenData>();

            var connection = new SqlConnection(ConnectionString);
            connection.Open();


            var command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"
Select Id,訓練項目,訓練部位,組數,每組次數,日期
From OpenData");
            /*command.CommandText = string.Format(@"
Select Id,服務分類, 資料集名稱, 主要欄位說明
From OpenData");*/
            if (!string.IsNullOrEmpty(name))
                command.CommandText =
                    $"{command.CommandText} Where 訓練項目=N'{name}'";
            //command.CommandText + "Where 服務分類=N'" + name+"'";
            //command.Parameters.Add(new SqlParameter("p1", name));

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var item = new OpenData();
                item.id= reader.GetInt32(0);
                item.訓練項目= reader.GetString(1);
                item.訓練部位 = reader.GetString(2);
                item.組數 = reader.GetString(3);
                item.每組次數 = reader.GetString(4);
                item.日期 = reader.GetString(5);
                /*item.資料集名稱 = reader.GetString(2);
                item.主要欄位說明 = reader.GetString(3);*/
                result.Add(item);
            }



            connection.Close();
            return result;
        }
        public void Insert(OpenData item)
        {
            var newItem = item;
            var connection = new SqlConnection(ConnectionString);
            connection.Open();


            var command = new SqlCommand("", connection);
            command.CommandText = string.Format(@"
INSERT INTO OpenData(訓練項目,訓練部位,組數,每組次數,日期) 
VALUES             (N'{0}',N'{1}',N'{2}',N'{3}',N'{4}')
            ", newItem.訓練項目, newItem.訓練部位, newItem.組數, newItem.每組次數, newItem.日期);

            /* command.CommandText = string.Format(@"
INSERT INTO OpenData(服務分類, 資料集名稱, 主要欄位說明) 
VALUES              (N'{0}',N'{1}',N'{2}')
            ", newItem.服務分類, newItem.資料集名稱, newItem.主要欄位說明);*/

            command.ExecuteNonQuery();


            connection.Close();
        }



        public void Update(OpenData item)
        {
            var updateItem = item;
            var connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            connection.Open();


            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
UPDATE [OpenData]
   SET 
      [訓練項目] = N'{0}'
        ,[訓練部位] = N'{1}'
        ,[組數] = N'{2}'
        ,[每組次數] = N'{3}'
        ,[日期] = N'{4}'
      
 WHERE ID=N'{5}'
            ", updateItem.訓練項目, updateItem.訓練部位, updateItem.組數, updateItem.每組次數, updateItem.日期, updateItem.id);

            /*command.CommandText = string.Format(@"
UPDATE [OpenData]
   SET 
      [服務分類] = N'{0}'
      ,[資料集名稱] = N'{1}'
      ,[主要欄位說明] = N'{2}'
 WHERE ID=N'{3}'
            ", updateItem.服務分類, updateItem.資料集名稱, updateItem.主要欄位說明, updateItem.id);*/

            command.ExecuteNonQuery();


            connection.Close();
        }



        public void Delete(int id)
        {
            var connection = new System.Data.SqlClient.SqlConnection(ConnectionString);
            connection.Open();


            var command = new System.Data.SqlClient.SqlCommand("", connection);
            command.CommandText = string.Format(@"
        DELETE FROM [OpenData]
         WHERE ID=N'{0}'
                    ", id);

            command.ExecuteNonQuery();


            connection.Close();
        }
    }
}
