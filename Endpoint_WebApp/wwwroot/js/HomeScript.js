document.addEventListener('DOMContentLoaded', function () {
    const header = document.querySelector('.main-header');
    const navLinks = document.querySelectorAll('nav a');
    let scrollTimeout;

    // کنترل اسکرول
    window.addEventListener('scroll', function () {
        // اگر در بالای صفحه هستیم، هدر همیشه نمایش داده شود
        if (window.scrollY === 0) {
            header.classList.remove('header-hide');
            clearTimeout(scrollTimeout);
            return;
        }

        // در غیر این صورت، هنگام اسکرول هدر را نمایش بده
        header.classList.remove('header-hide');

        // اگر اسکرول متوقف شد، هدر را مخفی کن
        clearTimeout(scrollTimeout);
        scrollTimeout = setTimeout(function () {
            header.classList.add('header-hide');
        }, 900); // مدت زمان توقف (قابل تنظیم)
    });

    // کنترل کلیک روی منوها
    navLinks.forEach(link => {
        link.addEventListener('click', function (e) {
            // اگر روی "خانه" کلیک شد (href برابر # یا /)
            if (this.getAttribute('href') === '#' || this.textContent.trim() === 'خانه') {
                header.classList.remove('header-hide');
                return;
            }
            // برای سایر منوها، هدر مخفی شود
            setTimeout(() => {
                header.classList.add('header-hide');
            }, 500);
        });
    });
});


//  دکمه های اسکرول
const scrollAmount = 200;

const teamMembers = document.getElementById('team-members');
const prevBtn = document.getElementById('team-scroll-prev');
const nextBtn = document.getElementById('team-scroll-next');

// اسکرول به راست (قبلی)
prevBtn.addEventListener('click', function () {
    teamMembers.scrollBy({ left: -scrollAmount, behavior: 'smooth' });
});

// اسکرول به چپ (بعدی)
nextBtn.addEventListener('click', function () {
    teamMembers.scrollBy({ left: scrollAmount, behavior: 'smooth' });
});

// ثبت درخواست توسط بازدید کننده وبسایت
function CreateForm() {
    var FullName = $("#FormFullName").val();
    var PhoneNumber = $("#FormPhoneNumber").val();
    var Address = $("#FormAddress").val();
    var ServiceName = $("#FormService").val()

    var postData = {
        'FullName': FullName,
        'PhoneNumber': PhoneNumber,
        'Address': Address,
        'ServiceName': ServiceName
    };

    $.ajax({
        contentType: 'application/x-www-form-urlencoded',
        dataType: 'json',
        type: "POST",
        url: "/Home/CreateForm",
        data: postData,
        success: function (data) {
            if (data.isSuccess == true) {
                swal.fire(
                    'موفق!',
                    data.message,
                    'success'
                ).then(function (isConfirm) {
                    window.location.replace("/AdminDashboard/Index");
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