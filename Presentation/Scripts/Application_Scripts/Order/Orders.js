$(document).ready(function () {
    getOrderTypes();
    //call to load orders

    //this is not necessary at this stage - since we know the list will load everything as the filter values will be default
    //however the benefit of doing this is to make our bancked as dry as possible - not catering for all these scenarios of data being parsed or not
    //var filter = {
    //    "OrderNumber": "",
    //    "OrderType": '-1',
    //    "FromDate": "",
    //    "ToDate": ""
    //};
    var table = $("#tblOrders").DataTable();

    if ($.fn.DataTable.isDataTable('#tblOrders')) {
        table.destroy();
    }

    //LoadOrders();
    $('#tblOrders').DataTable({
        "ajax": {
            "type": 'GET',
            "url": '/Order/GetAllOrdersAsync/',
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
                        return "<a class='btn btn-sm' style='background-color:#8000FF; color:#fff;float: right;' onclick=navigateToOrderDetails('" + data + "')><span class ='ti-pencil-alt' style='color:#fff'></span> Edit</a>";
                    }
                }
            },
            {
                "data": "Id",
                "render": function (data, type, full) {
                    if (data != "") {
                        return "<a class='btn btn-sm' style='background-color:#8AB6F9; color:#fff;float: left;' onclick=navigateToOrderlines('" + data + "')><span class ='ti-check-box' style='color:#fff'></span> Orderlines</a>";
                    }
                }
            }
        ],
        "scrollY": "2000px",
        "scrollCollapse": true,
        "pageLength": 100
    });
});

function LoadOrders() {
    //if this table exist lets crush it 

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
                        return "<a class='btn btn-sm' style='background-color:#8000FF; color:#fff;float: right;' onclick=navigateToOrderDetails('" + data + "')><span class ='ti-pencil-alt' style='color:#fff'></span> Edit</a>";
                    }
                }
            },
            {
                "data": "Id",
                "render": function (data, type, full) {
                    if (data != "") {
                        return "<a class='btn btn-sm' style='background-color:#8AB6F9; color:#fff;float: left;' onclick=navigateToOrderlines('" + data + "')><span class ='ti-check-box' style='color:#fff'></span> Orderlines</a>";
                    }
                }
            }
        ],
        "scrollY": "2000px",
        "scrollCollapse": true,
        "pageLength": 100
    });
}

function navigateToOrderDetails(Id) {
    window.location.href = "/Order/AddOrEditOrder/" + Id;
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

function navigateToOrderlines(Id) {
    window.location.href = "/Order/Orderlines/" + Id;
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