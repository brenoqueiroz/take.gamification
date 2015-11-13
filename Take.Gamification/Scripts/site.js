(function () {

    $(document).ready(function () {
        $(".from-now").each(function () {
            var time = $(this).html();
            var fromNow = moment(time).fromNow();
            $(this).html(fromNow);
        });
    });
})();