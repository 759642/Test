using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using GoogleMaps.DistanceMatrix;

/*
namespace DistanceMatrixExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Google Distance Matrix API 的金鑰
            string apiKey = "Your_API_Key";

            // 要查詢的起點地址
            string origin = "New York, NY";

            // 要查詢的終點地址列表
            string[] destinations = { "Los Angeles, CA", "Chicago, IL", "Houston, TX" };

            // 建立 DistanceMatrixService 物件
            var service = new DistanceMatrixService(apiKey);

            // 呼叫 API 進行距離和時間查詢
            var result = service.GetDistanceMatrix(origin, destinations);

            // 檢查 API 回傳的狀態
            if (result.Status == DistanceMatrixStatusCodes.OK)
            {
                // 迭代處理每個終點地址的距離和時間
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    var distance = result.Rows[i].Elements[0].Distance.Text;
                    var duration = result.Rows[i].Elements[0].Duration.Text;

                    // 輸出結果
                    Console.WriteLine("終點地址: " + destinations[i]);
                    Console.WriteLine("距離： " + distance);
                    Console.WriteLine("時間： " + duration);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("距離和時間查詢失敗。狀態： " + result.Status);
            }

            Console.ReadLine();
        }
    }
}
*/