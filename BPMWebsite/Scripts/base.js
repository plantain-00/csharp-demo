/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/bootstrap-notify/bootstrap-notify.d.ts" />
function beforeJump() {
    showTips("正在跳转...");
}

function showTips(message) {
    $('#tips').notify({
        message: { text: message }
    }).show();
}
//# sourceMappingURL=base.js.map
