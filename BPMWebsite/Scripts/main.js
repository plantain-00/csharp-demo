/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/bootstrap/bootstrap.d.ts" />
/// <reference path="typings/bootstrap-notify/bootstrap-notify.d.ts" />
/// <reference path="typings/jstree/jstree.d.ts" />
/// <reference path="base.ts" />
function exit() {
    beforeJump();
    location.href = "/Account/Exit";
}

$(document).ajaxSend(function () {
    $("html").addClass("wait");
});
$(document).ajaxComplete(function () {
    $("html").removeClass("wait");
});

function defaultPage() {
    beforeJump();
    $("#main").html("");
}

function applyNew() {
    $.ajax({
        url: "/Home/NewApplication",
        data: {},
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function myToDo() {
    $.ajax({
        url: "/Home/ToDoList",
        data: {},
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function myToBeRead(page, group) {
    $.ajax({
        url: "/Home/UnreadList",
        data: {
            page: page,
            group: group
        },
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function processHistory() {
    $.ajax({
        url: "/Home/History",
        data: {},
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function allUsersAndDepartments() {
    $.ajax({
        url: "/Account/AllUserAndDepartments",
        data: {},
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function allPermissions() {
    $.ajax({
        url: "/Account/AllPermissions",
        data: {},
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function userManagement(page, group) {
    $.ajax({
        url: "/Account/UserManagement",
        data: {
            page: page,
            group: group,
            name: $("#queryConditionOfName").val(),
            realName: $("#queryConditionOfRealName").val(),
            departmentId: $("#queryConditionOfDepartment").val()
        },
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function departmentManagement(page, group) {
    $.ajax({
        url: "/Account/DepartmentManagement",
        data: {
            page: page,
            group: group,
            name: $("#queryConditionOfName").val(),
            departmentId: $("#queryConditionOfDepartment").val()
        },
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function roleManagement(page, group) {
    $.ajax({
        url: "/Account/RoleManagement",
        data: {
            page: page,
            group: group,
            name: $("#queryConditionOfName").val()
        },
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function permissionManagement(page, group, noPermissionClassProtocol) {
    $.ajax({
        url: "/Account/PermissionManagement",
        data: {
            page: page,
            group: group,
            name: $("#queryConditionOfName").val(),
            permissionClassId: $("#queryConditionOfClass").val()
        },
        cache: false,
        success: function (data) {
            if (data == noPermissionClassProtocol) {
                showTips('请先创建权限类别！');
            } else {
                beforeJump();
                $("#main").html(data);
            }
        }
    });
}

function permissionClassManagement(page, group) {
    $.ajax({
        url: "/Account/PermissionClassManagement",
        data: {
            page: page,
            group: group,
            name: $("#queryConditionOfName").val()
        },
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

function modifyPassword() {
    $.ajax({
        url: "/Account/ModifyPassword",
        data: {},
        cache: false,
        success: function (data) {
            beforeJump();
            $("#main").html(data);
        }
    });
}

var AllPermissions = (function () {
    function AllPermissions() {
    }
    AllPermissions.init = function () {
        $.ajax({
            cache: false,
            data: {},
            success: function (json) {
                $('#allPermissions').jstree({
                    'core': {
                        'data': json
                    }
                });
            },
            url: "/Account/GetAllPermissions"
        });
    };
    return AllPermissions;
})();

var AllUsersAndDepartments = (function () {
    function AllUsersAndDepartments() {
    }
    AllUsersAndDepartments.init = function () {
        $.ajax({
            cache: false,
            data: {},
            success: function (json) {
                $('#allUsersAndDepartments').jstree({
                    'core': {
                        'data': json
                    }
                });
            },
            url: "/Account/GetAllUserAndDepartments"
        });
    };
    return AllUsersAndDepartments;
})();

var DepartmentManagement = (function () {
    function DepartmentManagement() {
    }
    DepartmentManagement.isCorrect = function () {
        if ($.trim($("#name").val()) == "") {
            showTips("名称不能为空！");
            return false;
        }
        return true;
    };

    DepartmentManagement.createNewOne = function () {
        $("#detail").show();
        this.clear();
        location.href = "#detail";
        $("#name").focus();
    };

    DepartmentManagement.cancelThis = function () {
        $("#detail").hide();
        this.clear();
    };

    DepartmentManagement.clear = function () {
        $("#id").val("");
        $("#name").val("");
        $("#parentDepartment").val("");
        $("input[name='roles']:checked").each(function () {
            $(this).prop('checked', false);
        });
    };

    DepartmentManagement.editThis = function (id) {
        $("#detail").hide();
        $.ajax({
            cache: false,
            datatype: "json",
            data: {
                id: id
            },
            success: function (value) {
                $("#id").val(value.id);
                $("#name").val(value.name);
                $("#parentDepartment").val(value.parentId);
                $("input[name='roles']").each(function () {
                    $(this).prop('checked', false);
                    var t = $(this);
                    $.each(value.roles, function (index, role) {
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
            url: "/Account/JsonGetDepartment"
        });
    };

    DepartmentManagement.deleteThis = function (id, name) {
        $("#nameDeleting").val(name);
        $("#idDeleting").val(id);
    };

    DepartmentManagement.confirmDelete = function (page, group) {
        $.ajax({
            cache: false,
            data: { id: $("#idDeleting").val() },
            type: "POST",
            success: function (json) {
                showTips(json.message + ":" + $("#nameDeleting").val());
                if (json.isSuccess == true) {
                    $('#confirm_delete').modal('hide');
                    departmentManagement(page, group);
                }
            },
            url: "/Account/JsonDeleteDepartment"
        });
    };

    DepartmentManagement.saveThis = function (page, group) {
        var roles = new Array();
        $("input[name='roles']:checked").each(function () {
            roles.push($(this).val());
        });
        if (this.isCorrect()) {
            $.post("/Account/JsonModifyDepartment", {
                id: $("#id").val(),
                name: $("#name").val(),
                parentDepartment: $("#parentDepartment").val(),
                roles: JSON.stringify(roles)
            }, function (json) {
                showTips(json.message);
                if (json.isSuccess == true) {
                    departmentManagement(page, group);
                }
            });
        }
    };
    return DepartmentManagement;
})();

var ModifyPassword = (function () {
    function ModifyPassword() {
    }
    ModifyPassword.modifyPassword = function () {
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
            oldPassword: $("#oldPassword").val(),
            newPassword: $("#newPassword").val()
        }, function (json) {
            showTips(json.message);
            if (json.isSuccess) {
                $("#oldPassword").val("");
                $("#newPassword").val("");
                $("#newPasswordAgain").val("");
                defaultPage();
            }
        });
    };
    return ModifyPassword;
})();

var RoleManagement = (function () {
    function RoleManagement() {
    }
    RoleManagement.isCorrect = function () {
        if ($.trim($("#name").val()) == "") {
            showTips('名称不能为空！');
            return false;
        }
        return true;
    };

    RoleManagement.createNewOne = function () {
        $("#detail").show();
        this.clear();
        location.href = "#detail";
        $("#name").focus();
    };

    RoleManagement.cancelThis = function () {
        $("#detail").hide();
        this.clear();
    };

    RoleManagement.clear = function () {
        $("#id").val("");
        $("#name").val("");
        $("input[name='permissions']:checked").each(function () {
            $(this).prop('checked', false);
        });
    };

    RoleManagement.editThis = function (id) {
        $("#detail").hide();
        $.ajax({
            cache: false,
            datatype: "json",
            data: {
                id: id
            },
            success: function (value) {
                $("#id").val(value.id);
                $("#name").val(value.name);
                $("input[name='permissions']").each(function () {
                    $(this).prop('checked', false);
                    var t = $(this);
                    $.each(value.permissions, function (index, permission) {
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
            url: "/Account/JsonGetRole"
        });
    };

    RoleManagement.deleteThis = function (id, name) {
        $("#nameDeleting").val(name);
        $("#idDeleting").val(id);
    };

    RoleManagement.confirmDelete = function (page, group) {
        $.ajax({
            cache: false,
            data: { id: $("#idDeleting").val() },
            type: "POST",
            success: function (json) {
                showTips(json.message + ":" + $("#nameDeleting").val());
                if (json.isSuccess == true) {
                    $('#confirm_delete').modal('hide');
                    roleManagement(page, group);
                }
            },
            url: "/Account/JsonDeleteRole"
        });
    };

    RoleManagement.saveThis = function (page, group) {
        if (this.isCorrect()) {
            var permissions = new Array();
            $("input[name='permissions']:checked").each(function () {
                permissions.push($(this).val());
            });
            $.post("/Account/JsonModifyRole", {
                id: $("#id").val(),
                name: $("#name").val(),
                permissions: JSON.stringify(permissions)
            }, function (json) {
                showTips(json.message);
                if (json.isSuccess == true) {
                    roleManagement(page, group);
                }
            });
        }
    };
    return RoleManagement;
})();

var UserManagement = (function () {
    function UserManagement() {
    }
    UserManagement.isCorrect = function () {
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
    };

    UserManagement.createNewOne = function () {
        $("#detail").show();
        this.clear();
        location.href = "#detail";
        $("#name").focus();
    };

    UserManagement.cancelThis = function () {
        $("#detail").hide();
        this.clear();
    };

    UserManagement.clear = function () {
        $("#id").val("");
        $("#name").val("");
        $("#password").val("");
        $("#realName").val("");
        $("#department").val("");
        $("input[name='roles']:checked").each(function () {
            $(this).prop('checked', false);
        });
    };

    UserManagement.editThis = function (id) {
        $("#detail").hide();
        $.ajax({
            cache: false,
            datatype: "json",
            data: {
                id: id
            },
            success: function (value) {
                $("#id").val(value.id);
                $("#name").val(value.name);
                $("#password").val("");
                $("#realName").val(value.realName);
                $("#department").val(value.departmentId);
                $("input[name='roles']").each(function () {
                    $(this).prop('checked', false);
                    var t = $(this);
                    $.each(value.roles, function (index, role) {
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
            url: "/Account/JsonGetUser"
        });
    };

    UserManagement.deleteThis = function (id, name) {
        $("#nameDeleting").val(name);
        $("#idDeleting").val(id);
    };

    UserManagement.confirmDelete = function (page, group) {
        $.ajax({
            cache: false,
            data: { id: $("#idDeleting").val() },
            type: "POST",
            success: function (json) {
                showTips(json.message + ":" + $("#nameDeleting").val());
                if (json.isSuccess == true) {
                    $('#confirm_delete').modal('hide');
                    userManagement(page, group);
                }
            },
            url: "/Account/JsonDeleteUser"
        });
    };

    UserManagement.saveThis = function (page, group) {
        if (this.isCorrect()) {
            var roles = new Array();
            $("input[name='roles']:checked").each(function () {
                roles.push($(this).val());
            });
            $.post("/Account/JsonModifyUser", {
                id: $("#id").val(),
                name: $("#name").val(),
                password: $("#password").val(),
                realName: $("#realName").val(),
                department: $("#department").val(),
                roles: JSON.stringify(roles)
            }, function (json) {
                showTips(json.message);
                if (json.isSuccess == true) {
                    userManagement(page, group);
                }
            });
        }
    };
    return UserManagement;
})();

var HistoryProcess = (function () {
    function HistoryProcess() {
    }
    HistoryProcess.showDetail = function (id) {
        $.ajax({
            url: "/Process/WorkAssignmentDetail",
            data: {
                id: id
            },
            cache: false,
            success: function (data) {
                beforeJump();
                $("#detail").html(data);
                $("#detail").show();
                location.href = "#detail";
            }
        });
    };
    return HistoryProcess;
})();

var Process = (function () {
    function Process() {
    }
    Process.newWorkAssignment = function () {
        $.ajax({
            url: "/Process/WorkAssignmentStart",
            data: {},
            cache: false,
            success: function (data) {
                beforeJump();
                $("#main").html(data);
            }
        });
    };

    Process.showImage = function () {
        $.ajax({
            url: "/Home/ProcessImage",
            data: {},
            cache: false,
            success: function (data) {
                beforeJump();
                $("#detail").html(data);
                $("#detail").show();
                location.href = "#detail";
            }
        });
    };
    return Process;
})();

var ToDoProcess = (function () {
    function ToDoProcess() {
    }
    ToDoProcess.handleThis = function (id) {
        $.ajax({
            url: "/Process/WorkAssignmentHandle",
            data: {
                id: id
            },
            cache: false,
            success: function (data) {
                beforeJump();
                $("#main").html(data);
            }
        });
    };
    return ToDoProcess;
})();

var UnreadList = (function () {
    function UnreadList() {
    }
    UnreadList.showDetail = function (id) {
        $.ajax({
            url: "/Process/WorkAssignmentDetail",
            data: {
                id: id
            },
            cache: false,
            success: function (data) {
                beforeJump();
                $("#detail").html(data);
                $("#detail").show();
                location.href = "#detail";
            }
        });
    };

    UnreadList.markedAsRead = function (id, page, group) {
        $.post("/Home/Read", {
            id: id
        }, function (data) {
            showTips(data.message);
            if (data.isSuccess) {
                myToBeRead(page, group);
            }
        });
    };
    return UnreadList;
})();

var WorkAssignment = (function () {
    function WorkAssignment() {
    }
    WorkAssignment.check = function (id) {
        $.post("/Process/WorkAssignmentCheckSubmit", {
            id: id
        }, function (data) {
            showTips(data.message);
            if (data.isSuccess) {
                defaultPage();
            }
        });
    };
    return WorkAssignment;
})();
//# sourceMappingURL=main.js.map
