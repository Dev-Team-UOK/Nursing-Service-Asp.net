// Removed old .img__btn listener which caused JS error
function SignInUser() {
    var Email = $("#Email_SignIn").val();
    var Password = $("#Password_SignIn").val();

    var postData = {
        'Email': Email,
        'Password': Password,
    };

    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "/Authentication/SignIn",
        data: postData,
        success: function (data) {
            if (data.isSuccess == true) {
                swal.fire(
                    'موفق!',
                    data.message,
                    'success'
                ).then(function (isConfirm) {
                    // Redirect based on data.message value
                    if (data.message === "مدیر وارد شدید") {
                        window.location.replace("/AdminDashboard/Reports");
                    } else if (data.message === "پرستار وارد شدید") {
                        window.location.replace("/NurseDashboard/Index");
                    } else if (data.message === "سوپر وایزر وارد شدید") {
                        window.location.replace("/SuperVisorDashboard/Index");
                    } else if (data.message === "اپراتور وارد شدید") {
                        window.location.replace("/OperatorDashboard/homepage");
                    }
                });
            }
            else {
                swal.fire(
                    'هشدار!',
                    data.message,
                    'warning'
                );
            }
        },
        error: function (request, status, error) {
            swal.fire(
                'هشدار!',
                request.responseText,
                'warning'
            );
        }
    });
}

function SignUpUser() {

    var Name = $("#Name_SignUp").val();
    var LastName = $("#LastName_SignUp").val();
    var Email = $("#Email_SignUp").val();
    var Password = $("#Password_SignUp").val();
    var UserName = $("#UserName_SignUp").val();
    var Phone = $("#Phone_SignUp").val();
    var Role = $("#Role_SignUp").val();
    var NurseNumber = $("#NurseNumber_SignUp").val();
    var NurseWorkYear = $("#NurseWorkYear_SignUp").val();
    var Shift = $("#Shift_SignUp").val();
    var DoService = $('#ServiceList_SignUp').val() || [];

    var postData = {
        'Name': Name,
        'LastName': LastName,
        'Email': Email,
        'Password': Password,
        'UserName': UserName,
        'Phone': Phone,
        'Role': Role,
        'NurseNumber': NurseNumber,
        'NurseWorkYear': NurseWorkYear,
        'Shift': Shift,
        'NurseDoService': DoService
    };

    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "/Authentication/SignUp",
        data: postData,
        success: function (data) {
            if (data.isSuccess == true) {
                swal.fire(
                    'موفق!',
                    data.message,
                    'success'
                ).then(function (isConfirm) {
                    // Redirect based on data.message value
                    if (data.message === "پرستار با موفقیت ایجاد شد.") {
                        window.location.replace("/NurseDashboard/Index");
                    } else if (data.message === "سوپر وایزر با موفقیت ایجاد شد.") {
                        window.location.replace("/SuperVisorDashboard/Index");
                    } else if (data.message === "اپراتور با موفقیت ایجاد شد.") {
                        window.location.replace("/OperatorDashboard/Index");
                    }
                });
            }
            else {

                swal.fire(
                    'هشدار!',
                    data.message,
                    'warning'
                );
            }
        },
        error: function (request, status, error) {
            swal.fire(
                'هشدار!',
                request.responseText,
                'warning'
            );
        }
    });
}

$(function () {
    $('#Role_SignUp').on('change', function () {
        var role = $(this).val();
        var $extra = $('#ExtraFields_SignUp');
        $extra.empty();
        if (role === 'nurse') {
            $extra.append(`
                <label>
                    <span>شماره پرستاری</span>
                    <input type="text" id="NurseNumber_SignUp"/>
                </label>
                <label>
                    <span>تعداد سال سابقه کاری</span>
                    <input type="number" id="NurseWorkYear_SignUp"/>
                </label>
                <label>
                    <span>سرویس‌های قابل ارائه:</span>
                    <select id="ServiceList_SignUp" multiple class="styled-select"></select>
                </label>
            `);

            // Fetch services from API
            $.getJSON('https://localhost:7010/api/Service/GetAll', function (services) {
                console.log('services:', services);
                var $serviceList = $('#ServiceList_SignUp');
                $serviceList.empty();
                services.forEach(function (service) {
                    $serviceList.append(
                        `<option value="${service.id}">${service.name}</option>`
                    );
                });
            });
        } else if (role === 'supervisor') {
            $extra.append(`
                    <label>
                        <span>شیفت</span>
                        <select id="Shift_SignUp" class="styled-select">
                            <option value="morning">شیفت صبح</option>
                            <option value="evening">شیفت عصر</option>
                            <option value="night">شیفت شب</option>
                        </select>
                    </label>
                `);
        }
    });
    $('#Role_SignUp').trigger('change');
});

function sendTestSMS() {
    var phone = $("#testPhoneInput").val();
    if (!phone) {
        swal.fire('خطا', 'لطفا شماره همراه را وارد کنید', 'error');
        return;
    }

    var phoneRegex = /^0\d{10}$/;
    if (!phoneRegex.test(phone)) {
        swal.fire('خطا', 'فرمت شماره همراه وارد شده معتبر نیست', 'error');
        return;
    }
    
    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "/Authentication/SendSMS",
        data: { 'phone': phone },
        success: function (data) {
            if (data.isSuccess == true) {
                swal.fire('موفق!', data.message, 'success');
                $('#smsModal').modal('hide');
            } else {
                swal.fire('هشدار!', data.message, 'warning');
            }
        },
        error: function (request, status, error) {
            swal.fire('هشدار!', request.responseText || 'خطا در ارتباط با سرور', 'warning');
        }
    });
}

function sendTestEmail() {
    var email = $("#testEmailInput").val();
    if (!email) {
        swal.fire('خطا', 'لطفا ایمیل را وارد کنید', 'error');
        return;
    }
    // اعتبارسنجی ایمیل
    var emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    if (!emailRegex.test(email)) {
        swal.fire('خطا', 'فرمت ایمیل وارد شده معتبر نیست', 'error');
        return;
    }

    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "/Authentication/SendEmail",
        data: { 'email': email },
        success: function (data) {
            if (data.isSuccess == true) {
                swal.fire('موفق!', data.message, 'success');
                $('#emailModal').modal('hide');
            } else {
                swal.fire('هشدار!', data.message, 'warning');
            }
        },
        error: function (request, status, error) {
            swal.fire('هشدار!', request.responseText || 'خطا در ارتباط با سرور', 'warning');
        }
    });
}

// Info Modal Logic
$(document).ready(function() {
    // Show the info modal when page loads
    var infoModalEl = document.getElementById('infoModal');
    if (infoModalEl) {
        var infoModal = new bootstrap.Modal(infoModalEl);
        infoModal.show();
    }
});

function nextInfoStep() {
    $('#infoStep1').addClass('d-none');
    $('#infoStep2').removeClass('d-none');
}