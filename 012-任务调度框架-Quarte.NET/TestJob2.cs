using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _012_任务调度框架_Quartz.NET
{
    class TestJob2 : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.MergedJobDataMap;
            string tag = dataMap.GetString("tag");
            string title = dataMap.GetString("title");
            string content = dataMap.GetString("content");
            string description = dataMap.GetString("description");
            string tels = dataMap.GetString("tels");

            //执行定时任务 发送短信
            await Task.Run(() => Console.WriteLine("执行任务了……"));

           await context.Scheduler.Shutdown();
        }

        //定义任务
        public static async void SetTask(string tag, string starttime, string title, string content, string description, string tels)
        {
            try
            {
                DateTime time = DateTime.Parse(starttime);
                //下面用到的执行时间要使用Cron表达式(如:"/5 * * ? * *"),下面我会介绍一下
                string cronstr = "/5 * * ? * *";
                IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();

                JobDataMap jobData = new JobDataMap();
                jobData.Add("tag", tag);
                jobData.Add("title", title);
                jobData.Add("content", content);
                jobData.Add("description", description);
                jobData.Add("tels", tels);

                //用时间做组名：DateTime.Now.ToLongDateString()
                IJobDetail job = JobBuilder.Create<TestJob2>().WithIdentity("Testjob1", "group1").SetJobData(jobData).Build();
                ITrigger trigger = TriggerBuilder.Create()
                                            .WithIdentity("triger1", "group1")
                                            .StartNow()                        //现在开始
                                            .WithCronSchedule(cronstr)
                                            .Build();

                await scheduler.ScheduleJob(job, trigger);//把作业，触发器加入调度器。
            }
            catch (Exception ex)
            {
                //记入日志
                Console.WriteLine(ex.Message);
            }
        }
    }
}