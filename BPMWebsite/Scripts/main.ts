/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/bootstrap-notify/bootstrap-notify.d.ts" />
/// <reference path="typings/jstree/jstree.d.ts" />
/// <reference path="base.ts" />
function exit() {
    beforeJump();
    location.href = "/Account/Exit";
}

$(document).ajaxSend(() => {
    $("html").addClass("wait");
});
$(document).ajaxComplete(() => {
    $("html").removeClass("wait");
});

function defaultPage() {
    beforeJump();
    $("#main").html("");
}

function applyNew() {
    $.ajax({
        url : "/Home/NewApplication",
        data : {},
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function myToDo() {
    $.ajax({
        url : "/Home/ToDoList",
        data : {},
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function myToBeRead(page: number, group: number) {
    $.ajax({
        url : "/Home/UnreadList",
        data : {
            page : page,
            group : group
        },
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function processHistory() {
    $.ajax({
        url : "/Home/History",
        data : {},
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function allUsersAndDepartments() {
    $.ajax({
        url : "/Account/AllUserAndDepartments",
        data : {},
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function allPermissions() {
    $.ajax({
        url : "/Account/AllPermissions",
        data : {},
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function userManagement(page: number, group: number) {
    $.ajax({
        url : "/Account/UserManagement",
        data : {
            page : page,
            group : group,
            name : $("#queryConditionOfName").val(),
            realName : $("#queryConditionOfRealName").val(),
            departmentId : $("#queryConditionOfDepartment").val()
        },
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function departmentManagement(page: number, group: number) {
    $.ajax({
        url : "/Account/DepartmentManagement",
        data : {
            page : page,
            group : group,
            name : $("#queryConditionOfName").val(),
            departmentId : $("#queryConditionOfDepartment").val()
        },
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function roleManagement(page: number, group: number) {
    $.ajax({
        url : "/Account/RoleManagement",
        data : {
            page : page,
            group : group,
            name : $("#queryConditionOfName").val()
        },
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function permissionManagement(page: number, group: number, noPermissionClassProtocol: string) {
    $.ajax({
        url : "/Account/PermissionManagement",
        data : {
            page : page,
            group : group,
            name : $("#queryConditionOfName").val(),
            permissionClassId : $("#queryConditionOfClass").val()
        },
        cache : false,
        success : data => {
            if (data == noPermissionClassProtocol) {
                showTips('请先创建权限类别！');
            } else {
                beforeJump();
                $("#main").html(data);
            }
        }
    });
}

function permissionClassManagement(page: number, group: number) {
    $.ajax({
        url : "/Account/PermissionClassManagement",
        data : {
            page : page,
            group : group,
            name : $("#queryConditionOfName").val()
        },
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function modifyPassword() {
    $.ajax({
        url : "/Account/ModifyPassword",
        data : {},
        cache : false,
        success : data => {
            beforeJump();
            $("#main").html(data);
        }
    });
}

class AllPermissions {
    static init() {
        $.ajax({
            cache : false,
            data : {},
            success : json => {
                $('#allPermissions').jstree({
                    'core' : {
                        'data' : json
                    }
                });
            },
            url : "/Account/GetAllPermissions"
        });
    }
}

class AllUsersAndDepartments {
    static init() {
        $.ajax({
            cache : false,
            data : {},
            success : json => {
                $('#allUsersAndDepartments').jstree({
                    'core' : {
                        'data' : json
                    }
                });
            },
            url : "/Account/GetAllUserAndDepartments"
        });
    }
}

class DepartmentManagement {
    static isCorrect() {
        if ($.trim($("#name").val()) == "") {
            showTips("名称不能为空！");
            return false;
        }
        return true;
    }

    static createNewOne() {
        $("#detail").show();
        this.clear();
        location.href = "#detail";
        $("#name").focus();
    }

    static cancelThis() {
        $("#detail").hide();
        this.clear();
    }

    static clear() {
        $("#id").val("");
        $("#name").val("");
        $("#parentDepartment").val("");
        $("input[name='roles']:checked").each(function() {
            $(this).prop('checked', false);
        });
    }

    static editThis(id: string) {
        $("#detail").hide();
        $.ajax({
            cache : false,
            datatype : "json",
            data : {
                id : id
            },
            success : value => {
                $("#id").val(value.id);
                $("#name").val(value.name);
                $("#parentDepartment").val(value.parentId);
                $("input[name='roles']").each(function() {
                    $(this).prop('checked', false);
                    var t = $(this);
                    $.each(value.roles, (index, role) => {
                        if (t.val() == role.id) {
                            t.prop('checked', true);
                            return false;
                        }
                        return true;
                    });
                });
                $("#detail").show();
                location.href = "#detail";
                $("#name").focus();
            },
            url : "/Account/JsonGetDepartment"
        });
    }

    static deleteThis(id: string, name: string) {
        $("#nameDeleting").val(name);
        $("#idDeleting").val(id);
    }

    static confirmDelete(page: number, group: number) {
        $.ajax({
            cache : false,
            data : { id : $("#idDeleting").val() },
            type : "POST",
            success : json => {
                showTips(json.message + ":" + $("#nameDeleting").val());
                if (json.isSuccess == true) {
                    $('#confirm_delete').modal('hide');
                    departmentManagement(page, group);
                }
            },
            url : "/Account/JsonDeleteDepartment"
        });
    }

    static saveThis(page: number, group: number) {
        var roles = new Array();
        $("input[name='roles']:checked").each(function() {
            roles.push($(this).val());
        });
        if (this.isCorrect()) {
            $.post("/Account/JsonModifyDepartment", {
                id : $("#id").val(),
                name : $("#name").val(),
                parentDepartment : $("#parentDepartment").val(),
                roles : JSON.stringify(roles)
            }, json => {
                showTips(json.message);
                if (json.isSuccess == true) {
                    departmentManagement(page, group);
                }
            });
        }
    }
}

class ModifyPassword {
    static modifyPassword() {
        if ($.trim($("#oldPassword").val()) == "") {
            showTips("旧密码不能为空");
            return;
        }
        if ($.trim($("#newPassword").val()) == "") {
            showTips("新密码不能为空");
            return;
        }
        if ($("#newPassword").val() != $("#newPasswordAgain").val()) {
            showTips("新密码不一致");
            return;
        }
        $.post("/Account/JsonModifyPassword", {
            oldPassword : $("#oldPassword").val(),
            newPassword : $("#newPassword").val()
        }, json => {
            showTips(json.message);
            if (json.isSuccess) {
                $("#oldPassword").val("");
                $("#newPassword").val("");
                $("#newPasswordAgain").val("");
                defaultPage();
            }
        });
    }
}

class RoleManagement {
    static isCorrect() {
        if ($.trim($("#name").val()) == "") {
            showTips('名称不能为空！');
            return false;
        }
        return true;
    }

    static createNewOne() {
        $("#detail").show();
        this.clear();
        location.href = "#detail";
        $("#name").focus();
    }

    static cancelThis() {
        $("#detail").hide();
        this.clear();
    }

    static clear() {
        $("#id").val("");
        $("#name").val("");
        $("input[name='permissions']:checked").each(function() {
            $(this).prop('checked', false);
        });
    }

    static editThis(id: string) {
        $("#detail").hide();
        $.ajax({
            cache : false,
            datatype : "json",
            data : {
                id : id
            },
            success : value => {
                $("#id").val(value.id);
                $("#name").val(value.name);
                $("input[name='permissions']").each(function() {
                    $(this).prop('checked', false);
                    var t = $(this);
                    $.each(value.permissions, (index, permission) => {
                        if (t.val() == permission) {
                            t.prop('checked', true);
                            return false;
                        }
                        return true;
                    });
                });
                $("#detail").show();
                location.href = "#detail";
                $("#name").focus();
            },
            url : "/Account/JsonGetRole"
        });
    }

    static deleteThis(id: string, name: string) {
        $("#nameDeleting").val(name);
        $("#idDeleting").val(id);
    }

    static confirmDelete(page: number, group: number) {
        $.ajax({
            cache : false,
            data : { id : $("#idDeleting").val() },
            type : "POST",
            success : json => {
                showTips(json.message + ":" + $("#nameDeleting").val());
                if (json.isSuccess == true) {
                    $('#confirm_delete').modal('hide');
                    roleManagement(page, group);
                }
            },
            url : "/Account/JsonDeleteRole"
        });
    }

    static saveThis(page: number, group: number) {
        if (this.isCorrect()) {
            var permissions = new Array();
            $("input[name='permissions']:checked").each(function() {
                permissions.push($(this).val());
            });
            $.post("/Account/JsonModifyRole", {
                id : $("#id").val(),
                name : $("#name").val(),
                permissions : JSON.stringify(permissions)
            }, json => {
                showTips(json.message);
                if (json.isSuccess == true) {
                    roleManagement(page, group);
                }
            });
        }
    }
}

class UserManagement {
    static isCorrect() {
        if ($.trim($("#name").val()) == "") {
            showTips('名称不能为空！');
            return false;
        }
        if ($.trim($("#password").val()) == "" && $("#id").val() == "") {
            showTips('密码不能为空！');
            return false;
        }
        if ($.trim($("#realName").val()) == "") {
            showTips('姓名不能为空！');
            return false;
        }
        return true;
    }

    static createNewOne() {
        $("#detail").show();
        this.clear();
        location.href = "#detail";
        $("#name").focus();
    }

    static cancelThis() {
        $("#detail").hide();
        this.clear();
    }

    static clear() {
        $("#id").val("");
        $("#name").val("");
        $("#password").val("");
        $("#realName").val("");
        $("#department").val("");
        $("input[name='roles']:checked").each(function() {
            $(this).prop('checked', false);
        });
    }

    static editThis(id: string) {
        $("#detail").hide();
        $.ajax({
            cache : false,
            datatype : "json",
            data : {
                id : id
            },
            success : value => {
                $("#id").val(value.id);
                $("#name").val(value.name);
                $("#password").val("");
                $("#realName").val(value.realName);
                $("#department").val(value.departmentId);
                $("input[name='roles']").each(function() {
                    $(this).prop('checked', false);
                    var t = $(this);
                    $.each(value.roles, (index, role) => {
                        if (t.val() == role.id) {
                            t.prop('checked', true);
                            return false;
                        }
                        return true;
                    });
                });
                $("#detail").show();
                location.href = "#detail";
                $("#name").focus();
            },
            url : "/Account/JsonGetUser"
        });
    }

    static deleteThis(id: string, name: string) {
        $("#nameDeleting").val(name);
        $("#idDeleting").val(id);
    }

    static confirmDelete(page: number, group: number) {
        $.ajax({
            cache : false,
            data : { id : $("#idDeleting").val() },
            type : "POST",
            success : json => {
                showTips(json.message + ":" + $("#nameDeleting").val());
                if (json.isSuccess == true) {
                    $('#confirm_delete').modal('hide');
                    userManagement(page, group);
                }
            },
            url : "/Account/JsonDeleteUser"
        });
    }

    static saveThis(page: number, group: number) {
        if (this.isCorrect()) {
            var roles = new Array();
            $("input[name='roles']:checked").each(function() {
                roles.push($(this).val());
            });
            $.post("/Account/JsonModifyUser", {
                id : $("#id").val(),
                name : $("#name").val(),
                password : $("#password").val(),
                realName : $("#realName").val(),
                department : $("#department").val(),
                roles : JSON.stringify(roles)
            }, json => {
                showTips(json.message);
                if (json.isSuccess == true) {
                    userManagement(page, group);
                }
            });
        }
    }
}

class HistoryProcess {
    static showDetail(id) {
        $.ajax({
            url : "/Process/WorkAssignmentDetail",
            data : {
                id : id
            },
            cache : false,
            success : data => {
                beforeJump();
                $("#detail").html(data);
                $("#detail").show();
                location.href = "#detail";
            }
        });
    }
}

class Process {
    static newWorkAssignment() {
        $.ajax({
            url : "/Process/WorkAssignmentStart",
            data : {},
            cache : false,
            success : data => {
                beforeJump();
                $("#main").html(data);
            }
        });
    }

    static showImage() {
        $.ajax({
            url : "/Home/ProcessImage",
            data : {},
            cache : false,
            success : data => {
                beforeJump();
                $("#detail").html(data);
                $("#detail").show();
                location.href = "#detail";
            }
        });
    }
}

class ToDoProcess {
    static handleThis(id) {
        $.ajax({
            url : "/Process/WorkAssignmentHandle",
            data : {
                id : id
            },
            cache : false,
            success : data => {
                beforeJump();
                $("#main").html(data);
            }
        });
    }
}

class UnreadList {
    static showDetail(id) {
        $.ajax({
            url : "/Process/WorkAssignmentDetail",
            data : {
                id : id
            },
            cache : false,
            success : data => {
                beforeJump();
                $("#detail").html(data);
                $("#detail").show();
                location.href = "#detail";
            }
        });
    }

    static markedAsRead(id: string, page: number, group: number) {
        $.post("/Home/Read", {
            id : id
        }, data => {
            showTips(data.message);
            if (data.isSuccess) {
                myToBeRead(page, group);
            }
        });
    }
}

class WorkAssignment {
    static check(id: string) {
        $.post("/Process/WorkAssignmentCheckSubmit", {
            id : id
        }, data => {
            showTips(data.message);
            if (data.isSuccess) {
                defaultPage();
            }
        });
    }
}