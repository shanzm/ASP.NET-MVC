$(document).ready(function () {
    $("#UserName").blur(function () {
        var userName = $.trim($("#UserName").val());
        //var userPwd = $.trim($("#UserPwd").val());
        if (userName == "" /*|| userPwd == ""*/) {
            alert("用户名或者密码不能为空");
        } else {
            $.post("Login", {
                "Name": userName,
                // "Pwd": userPwd
            },
                function (data) {
                    //var data = JSON.parse(data);因为后台使用Json（）函数传递数据，传递来的就是JavaScript中的Json对象，所以不需要在反序列化
                    //if (data.Success) {
                    //alert(data.Message);
                    //alert(data.Data[1].Name);
                    //var s = "";
                    //$(data.Data).each(function (index) { s += data.Data[index].Name + "-" });
                    //alert(s);
                    $("#tip").show();
                    $("#tip").text(data.Message);
                });
        }
    })
});

//Undone:
//1.为什么我把Controller中接收的参数中改为（Person p）就不可以了，
//按理说表单提交的数据中的name属性值对应的是Person类型的属性名
//2.即使我把所有的Pwd参数改为Password参数也是不可以