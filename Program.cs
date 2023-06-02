using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using Data = Google.Apis.Sheets.v4.Data;


/*
namespace SheetsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // 設定 Google Sheets API 的認證憑證
            GoogleCredential credential = GoogleCredential.FromFile("C:\\Users\\jackl\\source\\repos\\QuartzNetConsole\\oceanic-monitor-366608-02a88decbc73.json")
                .CreateScoped(new[] { SheetsService.Scope.SpreadsheetsReadonly });

            // 建立 SheetsService 物件
            SheetsService service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Sheets Sample"
            });

            // 設定試算表的 ID
            string spreadsheetId = "1mwBR_YSG6T4LAa87PoWgIqCAsixYsunwtM-R1uaVmjY";
            // 設定要擷取的工作表名稱列表
            List<string> sheetNames = new List<string> { "台北", "桃園", "新竹" };

            // 迴圈處理每個工作表
            foreach (string sheetName in sheetNames)
            {
                // 設定要擷取的範圍
                string range = $"{sheetName}!A:D";

                // 呼叫 Sheets API 取得資料
                SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
                ValueRange response = request.Execute();

                // 擷取到的資料
                IList<IList<object>> values = response.Values;

                // 輸出資料到控制台
                if (values != null && values.Count > 0)
                {
                    Console.WriteLine($"工作表：{sheetName}");
                    Console.WriteLine("擷取到的資料：");
                    foreach (var row in values)
                    {
                        Console.WriteLine(string.Join(", ", row));
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"找不到資料。 工作表：{sheetName}");
                }
                Console.WriteLine();
                Console.WriteLine(new string('=', 80));
            }

            Console.ReadLine();
        }
    }
}
*/

namespace SheetsSample
{
    public class FetchDataJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            // 設定 Google Sheets API 的認證憑證
            GoogleCredential credential = GoogleCredential.FromFile("C:\\Users\\jackl\\source\\repos\\QuartzNetConsole\\oceanic-monitor-366608-02a88decbc73.json")
                .CreateScoped(new[] { SheetsService.Scope.SpreadsheetsReadonly });

            // 建立 SheetsService 物件
            SheetsService service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Google Sheets Sample"
            });

            // 設定試算表的 ID
            string spreadsheetId = "1mwBR_YSG6T4LAa87PoWgIqCAsixYsunwtM-R1uaVmjY";
            // 設定要擷取的工作表名稱列表
            List<string> sheetNames = new List<string> { "台北", "桃園", "新竹" };

            // 迴圈處理每個工作表
            foreach (string sheetName in sheetNames)
            {
                // 設定要擷取的範圍
                string range = $"{sheetName}!A:D";

                // 呼叫 Sheets API 取得資料
                SpreadsheetsResource.ValuesResource.GetRequest request = service.Spreadsheets.Values.Get(spreadsheetId, range);
                ValueRange response = request.Execute();

                // 擷取到的資料
                IList<IList<object>> values = response.Values;

                // 輸出資料到控制台
                if (values != null && values.Count > 0)
                {
                    Console.WriteLine($"工作表：{sheetName}");
                    Console.WriteLine("擷取到的資料：");
                    foreach (var row in values)
                    {
                        Console.WriteLine(string.Join(", ", row));
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine($"找不到資料。 工作表：{sheetName}");
                }
                Console.WriteLine();
                Console.WriteLine(new string('=', 80));
            }

            Console.ReadLine();
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            RunScheduler().GetAwaiter().GetResult();
        }

        static async Task RunScheduler()
        {
            // 配置 Quartz.Net 設定
            var schedulerFactory = new StdSchedulerFactory();
            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.Start();

            // 建立 Job
            var job = JobBuilder.Create<FetchDataJob>()
                .WithIdentity("fetchDataJob", "group1")
                .Build();

            // 建立 Trigger，每隔一分鐘執行一次
            var trigger = TriggerBuilder.Create()
                .WithIdentity("fetchDataTrigger", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever())
                .Build();

            // 將 Job 和 Trigger 加入 Scheduler
            await scheduler.ScheduleJob(job, trigger);

            // 停止 Scheduler
            await Task.Delay(TimeSpan.FromSeconds(60));
            await scheduler.Shutdown();
        }
    }
}

