/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/bootstrap-notify/bootstrap-notify.d.ts" />
/// <reference path="base.ts" />
function login(returnUrl) {
    $.post("/Account/JsonLogin", {
        name: $("#name").val(),
        password: $("#password").val(),
        __RequestVerificationToken: $("input[name='__RequestVerificationToken']").val()
    }, function (json) {
        if (json.isSuccess == true) {
            beforeJump();
            location.href = returnUrl;
        } else {
            showTips(json.message);
            $("#name").focus();
        }
    });
    showTips('正在登入...');
}

$(document).ready(function () {
    //$("#name").focus();
});
$(document).ajaxSend(function () {
    $("html").addClass("wait");
    $("#btnLogin").attr("disabled", "disabled");
});
$(document).ajaxComplete(function () {
    $("html").removeClass("wait");
    $("#btnLogin").removeAttr("disabled");
});
//# sourceMappingURL=login.js.map
