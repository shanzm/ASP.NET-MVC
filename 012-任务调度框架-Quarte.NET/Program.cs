using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _012_任务调度框架_Quartz.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            //WithInterval();


            for (int i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("循环中……");
            }
            Console.ReadKey();
        }

        //间隔一定时间重复一次执行指定的任务
        public static async void WithInterval()
        {
            //--------------------------------1.准备调度者
            //创建调度器工厂
            ISchedulerFactory factory = new StdSchedulerFactory();
            //创建调度者
            IScheduler scheduler = await factory.GetScheduler();

            //--------------------------------2.准备任务
            //创建任务构造这：JobBuilder
            JobBuilder jobBuilder = JobBuilder.Create<TestJob>();
            //创建任务，定义任务的名字和任务组
            //IJobDetail job = jobBuilder.WithIdentity("job1", "grpup1").Build();
            IJobDetail job1 = jobBuilder.Build();

            //--------------------------------3.准备触发器
            //创建触发器构造者：TriggerBuilder
            TriggerBuilder triggerBuilder = TriggerBuilder.Create().StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever());
            //创建触发器，定义触发器的名字和触发组
            //ISimpleTrigger trigger1 = triggerBuilder.WithIdentity("trigger1","group2").Build() as ISimpleTrigger;
            ITrigger trigger = triggerBuilder.Build();


            //--------------------------------4、将任务与触发器添加到调度器中
            await scheduler.ScheduleJob(job1, trigger);
            //开始执行
            await scheduler.Start();
        }

       
    }
}

//quartz的机制是一个job可以有多个trigger，但是一个trigger只能有一个job

