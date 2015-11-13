(function () {

    $(document).ready(function () {
        $(".from-now").each(function () {
            var time = $(this).html();
            var fromNow = moment(time).fromNow();
            $(this).html(fromNow);
        });

        $(".data-table").DataTable({
            "language": {
                "url": "https://cdn.datatables.net/plug-ins/1.10.10/i18n/Portuguese-Brasil.json"
            }
        });
    });
})();