



$("#btnUpdatePassword").on('click', function () {
    UpdatePassword();

});

$('#ChangePasswordForm').validate({
    rules: {
        currentPassword: {
            required: true,
        },
        passwordConfirm: {
            required: true,
            equalTo: "#password"
        },
        Password: {
            required: true,
            minlength: 6
        },
    },
    messages: {

        currentPassword: {
            required: "Don't forget your password",

        },
        passwordConfirm: {
            required: "Don't forget your password",
            equalTo: "Don't forget your password"

        },
        Password: {
            required: "Don't forget your password",
            minlength: 'Password is required (6 characters is minimum)',
        },
    },
    errorPlacement: function (error, element) {
        error.insertAfter(element);
    }
});

function restData() {
    $("#currentpassword").val('');
    $("#password").val('');
    $("#passwordconfirm").val('');
}

function UpdatePassword() {
    debugger;

    var IsValid = $("#ChangePasswordForm").valid();

    if (IsValid) {

        var data = {
            newpassword: $("#password").val(),
            oldpassword: $("#currentpassword").val()
        };

        $.ajax
            ({
                /*url: "@Url.Action("ChangeUserPassword", "Account")",*/
                url: "/Account/ChangeUserPassword",
                type: "Post",
                data: data,
                dataType: 'Json',
                success: function (data) {
                    if (data == 'true') {
                        toastr["success"]("Your Password Changed Successfuly", "Change Password");
                        restData();
                    } else if (data == 'false') {
                        toastr["error"]("No data has been saved", "Change Password");
                    }
                    else {
                        toastr["error"]("Wrong Password", "Change Password");
                    }
                },
                error: function (error) {
                    toastr["error"]("Fail", "Change Password");
                }
            });
    }
};