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