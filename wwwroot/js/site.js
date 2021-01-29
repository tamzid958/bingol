// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.owl-carousel').owlCarousel({
    margin: 10,
    loop: true,
    autoWidth: true,
    items: 4
})

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

$(".pass-warning").hide();
$(".confirm-pass-warning").hide();

$(".pass").keyup(function () {
    var passLength = $(".pass").val().length;
    if (passLength <= 6) {
        $(".pass-warning").show();
    }
    else {
        $(".pass-warning").hide();
    }
});

$(".confirm-pass").keyup(function () {
    var passValue = $(".pass").val();
    var confrimPassValue = $(".confirm-pass").val();
    if (passValue != confrimPassValue) {
        $(".confirm-pass-warning").show();
    }
    else {
        $(".confirm-pass-warning").hide();
    }
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