$(document).ready(function () {
    getOrderTypes();

    var table = $("#tblOrders").DataTable();

    if ($.fn.DataTable.isDataTable('#tblOrders')) {
        table.destroy();
    }

    $('#tblOrders').DataTable({
        "ajax": {
            "type": 'GET',
            "url": '/Order/GetAllOrdersAsync',
            "data": JSON.stringify({
                "OrderNumber": "",
                "OrderType": '-1',
                "FromDate": "",
                "ToDate": ""
            }),
            "contentType": 'application/json; charset=utf-8',
            "datatype": 'json',
            "cache": false,
        },
        "columns": [
            { "data": "Id", "visible": false, "searchable": false },
            { "data": "OrderNumber" },
            { "data": "OrderType" },
            { "data": "OrderStatus" },
            { "data": "CustomerName" },
            { "data": "CreateDate" },
            {
                "data": "Id",
                "render": function (data, type, full) {
                    if (data != "") {
                        return "<a class='btn btn-sm' style='background-color:#8000FF; color:#fff;float: right;' onclick=navigateToOrderlineReader('" + data + "')><span class ='ti-eye' style='color:#fff'></span> View Orderlines</a>";
                    }
                }
            }
        ],
        "scrollY": "2000px",
        "scrollCollapse": true,
        "pageLength": 100
    });
})

function LoadOrders() {
    var table = $("#tblOrders").DataTable();

    if ($.fn.DataTable.isDataTable('#tblOrders')) {
        table.destroy();
    }



    $('#tblOrders').DataTable({
        "ajax": {
            "type": 'GET',
            "url": '/Order/GetAllOrdersAsync',
            "data": {
                "OrderNumber": $('#txtOrderNumberFilter').val(),
                "OrderType": $('#ddlOrderTypeFilter').val(),
                "FromDate": $('#txtFromDateFilter').val(),
                "ToDate": $('#txtToDateFilter').val()
            },
            "contentType": 'application/json; charset=utf-8',
            "datatype": 'json',
            "cache": false,
        },
        "columns": [
            { "data": "Id", "visible": false, "searchable": false },
            { "data": "OrderNumber" },
            { "data": "OrderType" },
            { "data": "OrderStatus" },
            { "data": "CustomerName" },
            { "data": "CreateDate" },
            {
                "data": "Id",
                "render": function (data, type, full) {
                    if (data != "") {
                        return "<a class='btn btn-sm' style='background-color:#8000FF; color:#fff;float: right;' onclick=navigateToOrderlineReader('" + data + "')><span class ='ti-eye' style='color:#fff'></span> View Orderlines</a>";
                    }
                }
            }
        ],
        "scrollY": "2000px",
        "scrollCollapse": true,
        "pageLength": 100
    });
}

function navigateToOrderlineReader(Id) {
    window.location.href = "/Order/OrderlineReader/" + Id;
}

function getOrderTypes() {
    $.ajax({
        type: "GET",
        url: "/Order/GetAllOrderTypesAsync",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Select Order Type</option>';
            $.each(data.data, function (k, item) {
                s += "<option value=" + item.Id + ">" + item.OrderTypeDesc + "</option>";
            });
            $("#ddlOrderTypeFilter").html(s);
        }
    });
}

function filter() {
    //var filter = {
    //    "OrderNumber": $('#txtOrderNumberFilter').val(),
    //    "OrderType": $('#ddlOrderTypeFilter').val(),
    //    "FromDate": $('#txtFromDateFilter').val(),
    //    "ToDate": $('#txtToDateFilter').val()
    //};

    LoadOrders();
}