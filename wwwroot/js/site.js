// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

tinymce.init({
    selector: ".tinymce_textarea",
    plugins: 'advlist autolink lists link image charmap print preview hr anchor pagebreak',
    toolbar_mode: 'floating',
    tinycomments_mode: "embedded",
    tinycomments_author: "Bingol",
});

$('.owl-carousel').owlCarousel({
    margin: 10,
    loop: true,
    autoWidth: true,
    items: 4
})
if (window.location.pathname.toLowerCase() == "/dashboard") {
    // Graphs
    var ctx = $('#myChart');
    // eslint-disable-next-line no-unused-vars
    var myChart = new Chart(ctx, {
        type: 'line',
        data: {
            labels: [
                'Sunday',
                'Monday',
                'Tuesday',
                'Wednesday',
                'Thursday',
                'Friday',
                'Saturday'
            ],
            datasets: [{
                data: [
                    15339,
                    21345,
                    18483,
                    24003,
                    23489,
                    24092,
                    12034
                ],
                lineTension: 0,
                backgroundColor: 'transparent',
                borderColor: '#EDAB06',
                borderWidth: 4,
                pointBackgroundColor: '#E6E6FA'
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: false
                    }
                }]
            },
            legend: {
                display: false
            }
        }
    })
}

$(".decreaseVal").click(function () {
    var input_el = $(this).next('input');
    var v = input_el.val() - 1;
    if (v >= input_el.attr('min'))
        input_el.val(v)
});

$(".increaseVal").click(function () {
    var input_el = $(this).prev('input');
    var v = input_el.val() * 1 + 1;
    if (v <= input_el.attr('max'))
        input_el.val(v)
});

$(".on-click-submit").change(function () {
    this.form.submit();
});

$(".buy-button").click(function () {
    var productId = $(this).attr("data-id");
    /*$("#addedCart").modal("show");*/
    alert("Product Added");
});
if (window.location.pathname.toLowerCase() == "/checkoutfail" || window.location.pathname.toLowerCase() == "/checkoutcancel") {
    window.setTimeout(function () {
        location.href = "/cart";
    }, 3000);
}
if (window.location.pathname.toLowerCase() == "/products") {
    const urlParams = new URLSearchParams(window.location.search);
    const sortedParam = urlParams.get('sorted');
    const categoryParam = urlParams.get('category');
    const priceParam = urlParams.get('price');
    const sizeParam = urlParams.get('size');
    const colorParam = urlParams.get('color'); 
    if (sortedParam != null) {
        $('#sort-selection').val(sortedParam).change();
    }
    if (categoryParam != null) {
        $("input[name=category][value=" + categoryParam + "]").attr('checked', 'checked');
    }
    if (priceParam != null) {
        $("#priceRange").val(priceParam).change();
    }
    if (sizeParam != null) {
        $("input[name=size][value=" + sizeParam + "]").attr('checked', 'checked');
    }
    if (colorParam != null) {
        $("input[name=color][value=" + colorParam + "]").attr('checked', 'checked');
    }
}
