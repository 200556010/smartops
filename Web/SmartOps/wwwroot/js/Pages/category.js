dtList = $('#' + dtTableName).DataTable({
    pageLength: 10,
    searching: true,
    responsive: true,
    processing: false,
    serverSide: true,
    orderCellsTop: true,
    autoWidth: false,
    ajax: {
        "url": listUrl,
        "type": "POST"
    },
    columns: [
        {
            data: "id", orderable: false,
            render: function (data, type, row, meta) {
                return meta.row + meta.settings._iDisplayStart + 1;
            }
        },
        { name: 'Name', data: 'name', orderable: true, export: true },
        { name: 'Description', data: 'description', orderable: true, export: true },
        {
            name: 'Action', data: null, orderable: false, render: function (data) {

                var html = `<div class='text-center d-flex gap-1 justify-content-center'>`;

                html += `<a href=${editUrl.toLowerCase().replace('$id', data.id)} title='Edit'><i class='text-primary fa fa-edit fa-lg pl-2'></i></a>`;
                html += `&nbsp;|&nbsp;`;
                html += `<a href='#' class='btnDelete text-danger' id="${data.id}" title='Delete'><i class='fa fa-trash fa-lg'></i></a>`;

                html += "</div>";
                return html;

            }
        }
    ],
    order: [[1, "asc"]]
});

$('.filter-submit').on('click', function () {
    $('.form-filter').each(function () {
        var columnName = $(this).attr('name');
        var column = dtList.column(columnName + ':name');
        var columnValue = $('.form-filter[name="' + columnName + '"]').val();
        if (column.search() !== columnValue) {
            column.search(columnValue);
        }
    });
    dtList.draw();
});

$('.filter-cancel').on('click', function () {
    dtList.columns().every(function () {
        var that = this;
        that.search('');
    });
    $('input.form-filter, select.form-filter').val('').trigger('change');
    dtList.draw();
});

$('.form-filter').on('keypress', function (event) {
    if (event.which === 13) {
        $('.filter-submit').trigger('click');
    }
});

