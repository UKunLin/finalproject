using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using YC.Models;
using YC.Repository;

namespace YC.Service
{
    public class ImportService
    {

        public static ImportService CreateForEF()
        {
            return new ImportService(new EFOpenDataRepository());
        }

        public IOpenDataRepository Repository { get; set; }
        private ImportService(EFOpenDataRepository repository) { Repository = repository;  }

        public List<OpenData> FindOpenDataFromXml()
        {
            List<OpenData> result = new List<OpenData>();

            string baseDir = Directory.GetCurrentDirectory();



            var xml = XElement.Load(System.IO.Path.Combine(baseDir, "App_Data/datagovtw_dataset_20181005.xml"));


            //XNamespace gml = @"http://www.opengis.net/gml/3.2";
            //XNamespace twed = @"http://twed.wra.gov.tw/twedml/opendata";
            var nodes = xml.Descendants("node").ToList();


            result = nodes
                .Where(x => !x.IsEmpty).ToList()
                .Select(node =>
                {
                    OpenData item = new OpenData();
                    item.id = int.Parse(getValue(node, "id"));
                    item.訓練項目 = getValue(node, "訓練項目");
                    item.訓練部位 = getValue(node, "訓練部位");
                    item.組數 = getValue(node, "組數");
                    item.每組次數 = getValue(node, "每組次數");
                    item.日期 = getValue(node, "日期");


                    /*item.id = int.Parse(getValue(node, "id"));
                    item.資料集名稱 = getValue(node, "資料集名稱");
                    item.服務分類 = getValue(node, "服務分類");
                    item.主要欄位說明 = getValue(node, "主要欄位說明");*/
                    return item;
                }).ToList();
            return result;

        }
        public List<OpenData> FindOpenDataFromDb(string name = null)
        {
            return Repository.SelectAll(name);
        }


        public void ImportToDb(List<OpenData> openDatas)
        {

            openDatas.ForEach(item =>
            {
                Repository.Insert(item);
            });

        }


        private string getValue(XElement node, string propertyName)
        {
            return node.Element(propertyName)?.Value?.Trim();

        }

    }
}
