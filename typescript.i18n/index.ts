/// <reference path="scripts/typings/jquery/jquery.d.ts" />

class Resources {
    //private static cn: { [name: string]: string }={
    //    "role" : "角色",
    //    "user" : "用户"
    //};
    //private static en: { [name: string]: string }={
    //    "role" : "Role",
    //    "user" : "User"
    //}
    private static cn = [
        {
            id : "role",
            text : "角色"
        }, {
            id : "user",
            text : "用户"
        }
    ];
    private static en = [
        {
            id : "role",
            text : "Role"
        }, {
            id : "user",
            text : "User"
        }
    ];
    private static language: string="CN";

    public static changeLanguage = (language: string= null) => {
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
        $.each(collection, (i, c) => {
            $("#" + c.id).text(c.text);
        });
        //$("#role").text(method["role"]);
        //$("#user").text(method["user"]);
    }
}