
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using YC.Database;
using YC.Models;

namespace YC.Repository
{
    public class EFOpenDataRepository : IOpenDataRepository
    {
        private OpenDataDbContext OpenDataDbContext { get; set; }

        public EFOpenDataRepository()
        {
            OpenDataDbContext = new OpenDataDbContext();
        }

        public List<OpenData> SelectAll(string name)
        {
            var result = new List<OpenData>();
            var query = OpenDataDbContext.OpenData.AsQueryable();
            if (!string.IsNullOrEmpty(name))
            {
                //query=query.Where(x => x.服務分類 == name);
            }
            return query.ToList();
        }
        public void Insert(OpenData item)
        {
            OpenDataDbContext.OpenData.Add(item);
            OpenDataDbContext.SaveChanges();
        }
        public void Update(OpenData item)
        {
            var exist = OpenDataDbContext.OpenData
                .Where(x => x.id == item.id).SingleOrDefault();
            if (exist == null)
                return;
            /*exist.主要欄位說明 = item.主要欄位說明;
            exist.服務分類 = item.服務分類;
            exist.資料集名稱 = item.資料集名稱;*/
            exist.訓練項目 = item.訓練項目;
            exist.訓練部位 = item.訓練部位;
            exist.組數 = item.組數;
            exist.每組次數 = item.每組次數;
            exist.日期 = item.日期;

            OpenDataDbContext.SaveChanges();
        }
        public void Delete(int id)
        {
            var exist = OpenDataDbContext.OpenData
                .Where(x => x.id == id).SingleOrDefault();
            if (exist == null)
                return;
            OpenDataDbContext.OpenData.Remove(exist);
            OpenDataDbContext.SaveChanges();

        }

    }
}
