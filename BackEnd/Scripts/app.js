/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
$(document).ready(function () {
    $.getJSON("api/users", {}, function (data) {
        ko.applyBindings({
            people: data
        });
    });
});
//# sourceMappingURL=app.js.map
