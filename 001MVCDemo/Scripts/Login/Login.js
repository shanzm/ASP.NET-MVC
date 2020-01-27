$(document).ready(function () {
    $("#btn").click(function () {
        var userName = $.trim($("#UserName").val());
        var userPwd = $.trim($("#UserPwd").val());
        if (userName == "" || userPwd == "") {
            alert("用户名或者密码不能为空");
        } else {
            $.post("Login", {
                "Name": userName,
                "Pwd": userPwd
            },
                function (data) {
                    if (data == "true") {
                        alert("登录成功");
                    } else {
                        alert("登录失败");
                    }
                });
        }
    })
});