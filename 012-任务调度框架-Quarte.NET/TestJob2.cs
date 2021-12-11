using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace _012_任务调度框架_Quartz.NET
{
    class TestJob2 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                JobDataMap dataMap = context.MergedJobDataMap;
                string tag = dataMap.GetString("tag");
                string title = dataMap.GetString("title");
                string content = dataMap.GetString("content");
                string description = dataMap.GetString("description");
                string tels = dataMap.GetString("tels");

                //执行定时任务:模拟发送短信
                await Task.Run(() => Console.WriteLine($"发短信：【{tag}】,{title}：{content },{description},电话：{tels}。"));

                //await context.Scheduler.Shutdown();//表示完成当前的定时任务，关闭调度器

                //记入日志
                Console.WriteLine("执行了一次定时任务，记入日志");
            }
            catch (Exception ex)
            {
                //记入日志Log.Error()
                Console.WriteLine(ex.Message);
            }
        }

        //封装一个定时任务
        public static async void SendMessage(string starttime, string cronStr, string tag, string title, string content, string description, string tels)
        {
            try
            {
                //创建调度器
                IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();


                //为任务准备参数
                DateTime time = DateTime.Parse(starttime);
                //JobDataMap jobData = new JobDataMap();
                //jobData.Add("tag", tag);
                //jobData.Add("title", title);
                //jobData.Add("content", content);
                //jobData.Add("description", description);
                //jobData.Add("tels", tels);

                JobDataMap jobData = new JobDataMap()
                {
                    new KeyValuePair<string, object>("tag", tag),
                    new KeyValuePair<string, object>("title", title),
                    new KeyValuePair<string, object>("content", content),
                    new KeyValuePair<string, object>("description", description),
                    new KeyValuePair<string, object>("tels", tels),
                };

                //创建任务：
                //用时间做组名：DateTime.Now.ToLongDateString()
                IJobDetail job = JobBuilder.Create<TestJob2>()
                                           .WithIdentity("Testjob1", "group1")
                                           .SetJobData(jobData)
                                           .Build();

                //创建触发器
                ITrigger trigger = TriggerBuilder.Create()
                                                 .WithIdentity("triger1", "group1")
                                                 .StartAt(time)//触发器开始时间//.StartNow()现在开始
                                                 .WithCronSchedule(cronStr)
                                                 .Build();

                //将任务和触发器添加到调度器中
                await scheduler.ScheduleJob(job, trigger);
                await scheduler.Start();
                //throw new Exception("error!");
            }
            catch (Exception ex)
            {
                //记入日志
                Console.WriteLine(ex.Message);
            }
        }
    }
}