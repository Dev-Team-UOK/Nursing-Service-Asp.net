


window.addEventListener('load', function () {
    setTimeout(function () {
        const loader = document.getElementById('site-loader');
        if (loader) {
            loader.style.opacity = '0';
            setTimeout(() => loader.style.display = 'none', 600);
        }
    }, 2500); // مدت زمان نمایش لودر (۲.۵ ثانیه)
});