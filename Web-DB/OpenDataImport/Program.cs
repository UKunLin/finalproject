using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using YC.Models;

namespace OpenDataImport
{
    class Program
    {
        static void Main(string[] args)
        {
            YC.Service.ImportService importService = YC.Service.ImportService.CreateForEF(); ;
            
            var nodes = importService.FindOpenDataFromDb();
            
            


            showOpenData(nodes);
            Console.ReadKey();

        }

        



        private static void showOpenData(List<OpenData> nodes)
        {

            Console.WriteLine(string.Format("共收到{0}筆的資料", nodes.Count));
            nodes.GroupBy(node => node.服務分類).ToList()
                .ForEach(group =>
                {

                    var key = group.Key;
                    var groupDatas = group.ToList();
                    var message = $"服務分類:{key},共有{groupDatas.Count()}筆資料";
                    Console.WriteLine(message);
                });


        }
    }

}
