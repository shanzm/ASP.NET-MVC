# 关于ASP.NET MVC模式

1. MVCDEMO项目

* 1.1 初识MVC--TestController.cs
* 1.2 简单的增删改查--PersonController.cs
* 1.3 关于layout--LayoutTestController.cs

2. 02DropDownList

* 1.1 自己实现DropDownList选中--DropDownlist1Controller.cs
* 1.2 使用Html辅助类：@Html.DropDownList--DropDown2Controller.cs

3. 数据检验（02DropDownList）

* 1.1 判断请求是否来自Ajax--IsAjaxRequestController.cs
* 1.2 数据检验--使用Attribute
* 1.3 输出数据错误信息--ModelState
* 1.3 自定义特性进行数据验证--在Common文件夹中添加自定义的特性类

4. 04FilterTest1

* 1.1 检验是否登录--CheckAuthorFilter.cs
* 1.2 日志：记录访问信息--LogActionFilter.cs
* 1.3 记录异常信息--ExceptionFilter.cs
