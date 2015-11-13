(function () {

    $(document).ready(function () {
        $(".from-now").each(function () {
            var time = $(this).html();
            var fromNow = moment.utc(time).fromNow();
            $(this).html(fromNow);
        });

        $(".data-table").DataTable({
            "language": {
                "url": "https://cdn.datatables.net/plug-ins/1.10.10/i18n/Portuguese-Brasil.json"
            }
        });
    });

    $(".add-merit").click(function () {
        var meridId = $(this).attr("merit");
        var userId = $(this).attr("user");

        $.ajax("/UserAccounts/DoMerit?meritId=" + meridId + "&userId=" + userId, {
            success: function (data) {
                toastr.success(data);
            },
            error: function (data) {
                toastr.error(data);
            }
        });
    });

    $(document).ajaxStart(function () {
        $.blockUI({ message: '<h1><img src="/Content/imgs/loading.gif" /></h1>' });
    }).ajaxStop(function () {
        $.unblockUI();
    });
})();