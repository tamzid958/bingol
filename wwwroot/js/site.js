// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

tinymce.init({
    selector: ".tinymce_textarea",
    plugins: 'advlist autolink lists link image charmap print preview hr anchor pagebreak',
    toolbar_mode: 'floating',
    tinycomments_mode: "embedded",
    tinycomments_author: "Bingol",
    setup: function (ed) {
        ed.on('keyup', function (e) {
            var count = CountCharacters();
            document.getElementById("character_count").innerHTML = "Characters: " + count;
        });
    }

});

function CountCharacters() {
    var body = tinymce.get("txtTinyMCE").getBody();
    var content = tinymce.trim(body.innerText || body.textContent);
    return content.length;
};
function ValidateCharacterLength() {
    var max = 75;
    var count = CountCharacters();
    if (count > max) {
        alert("Maximum " + max + " characters allowed.")
        return false;
    }
    return;
}
$("#validate-product").click(function () {
    return ValidateCharacterLength();
})
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

var cartproducts = [];

$(".buy-button").click(function (e) {
    e.preventDefault();
    var product_quantity = $("#product-quantity").val();
    var product_color = $("#product-color").val();
    var product_size = $("#product-size").val();
    $("#cart-quantity").val(product_quantity);
    $("#cart-color").val(product_color);
    $("#cart-size").val(product_size);
    this.form.submit();
    /*$("#addedCart").modal("show");*/
    //alert("Product Added");
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
$('.custom-select').select2({
    width: 'resolve'
});
$('#continue-to-checkout').click(function () {
    $("#checkout-data").submit();
});
$(".review-btn").click(function () {
    var productid = $(this).attr("data-product-id");
    var orderid = $(this).attr("data-order-id");
    $("#hidden-product-id").val(productid).change();
    $("#hidden-order-id").val(orderid).change()
})
$("#invoice-btn").click(function () {
    var element = document.getElementById('invoice-paper');
    html2pdf(element);
});