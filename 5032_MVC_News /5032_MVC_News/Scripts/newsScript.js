$(document).ready(function () {
    // Setup - add a text input to each footer cell
    $('#newsTable tfoot th').each(function () {
        var title = $(this).text();
        $(this).html('<input type="text" placeholder="Search " />');
    });

    // DataTable
    var table = $('#newsTable').DataTable();

    // Apply the search
    table.columns().every(function () {
        var that = this;

        $('input', this.footer()).on('keyup change', function () {
            if (that.search() !== this.value) {
                that
                    .search(this.value)
                    .draw();
            }
        });
    });

    $("#dateField").datepicker();

    $("#checkall").click(function () {
        $(".checkClass").prop('checked', $(this).prop('checked'));
    });

});