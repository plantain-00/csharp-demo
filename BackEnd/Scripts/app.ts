﻿/// <reference path="typings/knockout/knockout.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
$(document).ready(()=> {
    $.getJSON("api/users", {}, data=> {
        ko.applyBindings({
            people: data
        });
    });
});