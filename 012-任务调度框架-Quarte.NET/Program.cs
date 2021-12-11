using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System;
using System.Threading.Tasks;

namespace _012_任务调度框架_Quartz.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            WithInterval();

            //AtHourAndMinute();

            //PackageJob();

            for (int i = 0; i < 100; i++)
            {
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine($"循环中{i}……");
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
            JobBuilder jobBuilder = JobBuilder.Create<TestJob1>();
            //创建任务，定义任务的名字和任务组
            //IJobDetail job = jobBuilder.WithIdentity("job1", "grpup1").Build();
            IJobDetail job1 = jobBuilder.Build();

            //--------------------------------3.准备触发器
            //创建触发器构造者：TriggerBuilder
            TriggerBuilder triggerBuilder = TriggerBuilder.Create().StartNow().WithSimpleSchedule(x => x.WithIntervalInSeconds(5).WithRepeatCount(2));//此处RepeatForever()为重复无数次，使用.WithRepeatCount()可以设置重复的次数
            //创建触发器，定义触发器的名字和触发组
            ISimpleTrigger trigger = triggerBuilder.WithIdentity("trigger1", "group2").Build() as ISimpleTrigger;

            //--------------------------------4、将任务与触发器添加到调度器中
            await scheduler.ScheduleJob(job1, trigger);
            //开始执行
            await scheduler.Start();

            //调度器延时调度，但是注意，在调度器延时的时候，定时任务可能计时已经开始了，
            //一旦结束调度器的延时，定时任务会把之前延时的时间中应该执行的任务都一次性执行了
            //await scheduler.StartDelayed(TimeSpan.FromSeconds(20));

        }

        //每天按照指定的时间点执行任务
        public static async void AtHourAndMinute()
        {
            //创建调度器
            IScheduler scheduler = await new StdSchedulerFactory().GetScheduler();

            //创建任务
            //JobDetailImpl job1 = new JobDetailImpl("TestJob1", "group1", typeof(TestJob));//JobDetailImpl是IJobDetail的实现类
            //等价于：
            IJobDetail job1 = JobBuilder.Create<TestJob1>().WithIdentity("Testjob1", "group1").Build();
            //创建触发器
            IMutableTrigger trigger2job1 = CronScheduleBuilder.DailyAtHourAndMinute(22, 22).Build();//每天的22:13重复触发任务
            //IMutableTrigger trigger2job1=CronScheduleBuilder.AtHourAndMinuteOnGivenDaysOfWeek(01, 00, DayOfWeek.Saturday, DayOfWeek.Sunday);//每周中的特定的某几天的某时某分
            //IMutableTrigger trigger2job1 = CronScheduleBuilder.MonthlyOnDayAndHourAndMinute(20, 1, 18).Build();//每月的某天某时某分执行任务
            //IMutableTrigger trigger2job1 = CronScheduleBuilder.CronSchedule("40 18 2 ? * * *").Build();//每年每月的2点18分40秒执行任务
            trigger2job1.Key = new TriggerKey("trigger1");

            //以上的创建触发器都在在单独的为触发器定义一个名字，所以可是使用下面的写法：
            //ITrigger trigger2job1 = TriggerBuilder.Create().StartNow().WithCronSchedule("10 52 18 ? * * *").WithIdentity("trigger1").Build();

            //将任务和触发器添加到调度器中
            await scheduler.ScheduleJob(job1, trigger2job1);

            //开始执行调度者
            await scheduler.Start();
        }

        //调用封装的定时任务
        public static async void PackageJob()
        {
            await Task.Run(() => TestJob2.SendMessage(DateTime.Now.ToString(), "/5 * * ? * *", "新闻", "新冠病毒", "治愈者越来越多", "啦啦啦", "10086"));
        }

    }
}

//quartz的机制是一个job可以有多个trigger，但是一个trigger只能有一个job

