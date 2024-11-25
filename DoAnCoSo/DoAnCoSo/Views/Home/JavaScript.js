<script>
    $(document).ready(function(){
        $(".owl-carousel").owlCarousel({
            items: 1,
            loop: true,
            autoplay: true,
            autoplayTimeout: 5000, // Thời gian chờ giữa các slide (đơn vị là mili giây)
            autoplayHoverPause: true, // Tạm dừng autoplay khi rê chuột vào
            nav: true, // Hiển thị nút điều hướng (prev/next)
            navText: ['<i class="fa fa-angle-left"></i>', '<i class="fa fa-angle-right"></i>'], // Biểu tượng của nút điều hướng
            responsive: {
                0: {
                    items: 1
                },
                600: {
                    items: 1
                },
                1000: {
                    items: 1
                }
            }
        });
    });
</script>
