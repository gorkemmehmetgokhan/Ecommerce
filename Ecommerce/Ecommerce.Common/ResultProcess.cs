using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ecommerce.Entity;


namespace Ecommerce.Common
{
    public class ResultProcess<T>
    {
        public Result<int> GetResult(DB_EcommerceEntities db)
        {
            Result<int> result = new Result<int>();
            int sonuc = db.SaveChanges();
            if (sonuc > 0)
            {
                result.UserMessage = "işlem başarılı";
                result.IsSuccessed = true;
                result.ProcessResult = sonuc;
            }
            else
            {
                result.UserMessage = "işlem başarısız";
                result.IsSuccessed = false;
                result.ProcessResult = sonuc;
            }
            return result;
        }
        public Result<List<T>> GetListResult(List<T> data)
        {
            Result<List<T>> result = new Result<List<T>>();
            if (data != null)
            {
                result.UserMessage = "işlem başarılı";
                result.IsSuccessed = true;
                result.ProcessResult = data;
            }
            else
            {
                result.UserMessage = "işlem başarısız";
                result.IsSuccessed = false;
                result.ProcessResult = data;
            }
            return result;
        }

        public Result<T> GetT(T data)
        {
            Result<T> result = new Result<T>();
            if (data != null)
            {
                result.IsSuccessed = true;
                result.UserMessage = "işlem başarılı";
                result.ProcessResult = data;
            }
            else
            {
                result.IsSuccessed = false;
                result.UserMessage = "işlem başarısız";
                result.ProcessResult = data;
            }
            return result;
        }
    }
}
