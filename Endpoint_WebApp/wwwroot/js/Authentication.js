document.querySelector('.img__btn').addEventListener('click', function () {
    document.querySelector('.cont').classList.toggle('s--signup');
});

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