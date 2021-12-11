using Quartz;
using Quartz.Impl;

namespace _018Topshelf_log4net_quartz.net
{

    /// <summary>
    /// Quartz.net 
    /// 任务调度器类，用于创建调度器并配置调度计划
    /// 这里使用饿汉式单例模式构造
    /// </summary>
    public class MyScheduler
    {
        private static readonly MyScheduler myScheduler = new MyScheduler();

        //调度器
        public IScheduler scheduler { private set; get; }
        //任务
        public IJobDetail job { private set; get; }
        //触发器
        public ISimpleTrigger trigger { private set; get; }


        private MyScheduler()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();

            job = JobBuilder.Create<MyJob>().Build();

            trigger = TriggerBuilder.Create()
                .StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())//配置每隔5s执行一次，永久执行下去
                .WithIdentity("trigger", "group").Build() as ISimpleTrigger;

            scheduler.ScheduleJob(job, trigger);

        }

        //单例模式，用于返回单例对象
        public static MyScheduler GetMyScheduler()
        {
            return myScheduler;
        }
    }
}
