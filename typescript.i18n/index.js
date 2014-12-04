/// <reference path="scripts/typings/jquery/jquery.d.ts" />
var Resources = (function () {
    function Resources() {
    }
    Resources.cn = [
        {
            id: "role",
            text: "角色"
        }, {
            id: "user",
            text: "用户"
        }
    ];
    Resources.en = [
        {
            id: "role",
            text: "Role"
        }, {
            id: "user",
            text: "User"
        }
    ];
    Resources.language = "CN";

    Resources.changeLanguage = function (language) {
        if (typeof language === "undefined") { language = null; }
        if (language != null) {
            Resources.language = language;
        }

        //var method;
        //switch (Resources.language) {
        //case "CN":
        //    method = Resources.cn;
        //    break;
        //case "EN":
        //    method = Resources.en;
        //    break;
        //default:
        //    method = Resources.cn;
        //}
        var collection;
        switch (Resources.language) {
            case "CN":
                collection = Resources.cn;
                break;
            case "EN":
                collection = Resources.en;
                break;
            default:
                collection = Resources.cn;
        }
        $.each(collection, function (i, c) {
            $("#" + c.id).text(c.text);
        });
        //$("#role").text(method["role"]);
        //$("#user").text(method["user"]);
    };
    return Resources;
})();
//# sourceMappingURL=index.js.map
