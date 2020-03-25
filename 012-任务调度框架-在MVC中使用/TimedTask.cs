using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace _012_任务调度框架_在MVC中使用
{
    public class TimedTask : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                //每个5秒，往根目录中的log.txt添加一个记录
                string path = HostingEnvironment.MapPath("~/log.txt");
                await Task.Run(() => File.AppendAllText(path, DateTime.Now.ToString() + "\r\n"));
                //await context.Scheduler.Shutdown();//表示完成当前的定时任务，关闭调度器
                //记入日志
                //Console.WriteLine("执行了一次定时任务，记入日志");
            }
            catch (Exception ex)
            {
                //记入日志Log.Error()
                Console.WriteLine(ex.Message);
            }
        }

        //封装一个定时任务
        public static async void ExcuteTimedTask(string starttime, string cronStr)
        {
            try
            {
                //创建调度器
                IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();


                //为任务准备参数
                DateTime time = DateTime.Parse(starttime);

                //创建任务：
                //用时间做组名：DateTime.Now.ToLongDateString()
                IJobDetail job = JobBuilder.Create<TimedTask>()
                                           .WithIdentity("Testjob1", "group1")
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