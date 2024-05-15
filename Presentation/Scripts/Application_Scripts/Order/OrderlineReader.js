$(document).ready(function () {
    LoadOrderlines();
});

function LoadOrderlines() {
    $('#tblOrderlines').DataTable({
        "ajax": {
            "type": 'GET',
            "url": '/Order/GetAllOrderlinesAsync/' + $('#OrderId').val(),
            "data": '{}',
            "contentType": 'application/json; charset=utf-8',
            "datatype": 'json',
            "cache": false,
        },
        "columns": [
            { "data": "OrderlineKey", "visible": false, "searchable": false },
            { "data": "LineNumber" },
            { "data": "ProductCode" },
            { "data": "ProductType" },
            { "data": "ProductCostPrice" },
            { "data": "ProductSalesPrice" },
            { "data": "Quantity" }
        ],
        "scrollY": "2000px",
        "scrollCollapse": true,
        "pageLength": 100
    });
}
