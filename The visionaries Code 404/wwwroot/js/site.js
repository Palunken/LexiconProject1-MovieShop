// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


document.addEventListener('DOMContentLoaded', () => {
    const containers = document.querySelectorAll('.movie-container');

    containers.forEach((container) => {
        let startX, scrollLeft;

        container.addEventListener('touchstart', (e) => {
            startX = e.touches[0].pageX;
            scrollLeft = container.scrollLeft;
        });

        container.addEventListener('touchmove', (e) => {
            const x = e.touches[0].pageX;
            const walk = startX - x;
            container.scrollLeft = scrollLeft + walk;
        });
    });
});
    function AddToCart(movieId) {

        $.ajax({

            type: 'post',

            url: '/Cart/AddToCart',

            dataType: 'json',

            data: { id: movieId },

            success: function (count) {

                $('#cartCount').html(count); // The id ‘cartCount’ refers to an HTML-element

            }

        })

}

window.onscroll = function () {
    var btn = document.getElementById("backToTopBtn");
    if (document.body.scrollTop > 50 || document.documentElement.scrollTop > 50) {
        btn.style.display = "block";
    } else {
        btn.style.display = "none";
    }
};


document.getElementById("backToTopBtn").onclick = function () {
    window.scrollTo({ top: 0, behavior: 'smooth' });
};