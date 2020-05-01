# 简单说明

## 架构说明

0. 三层架构，使用EF，实现的MVC项目，使用过滤器实现简单的权限验证
1. 使用EF ORM，所有的EF实体类在Entities类库项目中
2. DemoDal：数据获取层。该层操作数据库，使用Linq to EF进行增删公查， 查询结果封装在DTO对象中（data transfer object）
3. DemoBll：业务逻辑层。转送DTO对象
4. MVC（UI层）：将DTO数据在Controller中解析，封装在ViewModel中传递到View中，显示给用户。

## 调试说明

1. 准备数据库

新建数据库，建表:

```cs
CREATE TABLE [dbo].[T_Users](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Age] [int] NULL,
	[Password] [nvarchar](50) NOT NULL,
	[IsDeleted] [bit] NULL
) ON [PRIMARY]
```

在Web.Config中修改连接字符串
```xml
 <connectionStrings >
    <add name ="connStr" connectionString ="server=.;database=;uid=;pwd=" providerName ="System.Data.SqlClient"/>
</connectionStrings>
 ```
2. 右键解决方案-->还原NuGet程序包

3. 整个表中的用户都可以登录，比如，用户名：admin1，password:123456
