using Topshelf;

namespace _018Topshelf
{
    class Program
    {
        #region 说明

        /// 使用Topshelf创建windows服务
        /// 0. 官方文档：http://docs.topshelf-project.com/en/latest/configuration/quickstart.html
        /// 1. 创建控制台项目
        /// 2. nuget:Install-Package Topshelf
        /// 3. 安装服务：管理员运行命令行，转到编辑的exe路径：xxx.exe install
        /// 4. 安装完成后就会在Windows服务下找到并启动它，右键可以看到该服务的描述，显示名称等信息
        ///     以及可以设置当服务失败后执行的操作
        /// 5. serviceName必须唯一，若是需要在一台服务器上同时运行多个相同的服务则：
        ///     我们可以不在程序中显式的设置serverName参数
        ///     而是在安装的时候通过命令行设置服务的名称
        ///     xxx.exe install --servicename xxx1
        ///     xxx.exe install --servicename xxx2
        /// 6. 卸载服务命令,只需要将install命令改为uninstall：xxx.exe uninstall 

        #endregion

        static void Main(string[] args)
        {

            HostFactory.Run(x =>
            {
                x.Service<MyService>();

                //以local system模式运行
                x.RunAsLocalSystem();

                /*
                //启动类型设置
                x.StartAutomatically();//自动
                x.StartAutomaticallyDelayed();// 自动（延迟启动）
                x.StartManually();//手动
                x.Disabled();//禁用
                */
                //常规信息
                x.SetDescription("测试topshelf"); //MyService服务的描述信息
                x.SetDisplayName("MyService"); //MyService服务的显示名称
                x.SetServiceName("MyService"); //MyService服务名称

            });

        }
    }
}
