$(document).ready(function () {
    LoadOrderlines();
});

function LoadOrderlines(Id) {
    $('#tblOrderlines').DataTable({
        "ajax": {
            "type": 'GET',
            "url": '/Order/GetAllOrderlinesAsync/' + $('#OrderId').val(),
            "data":'{}',
            "contentType": 'application/json; charset=utf-8',
            "datatype": 'json',
            "cache": false,
        },
        "columns": [
            { "data": "OrderlineKey", "visible": false, "searchable": false },
            { "data": "LineNumber", "searchable": false },
            { "data": "ProductCode", "searchable": false },
            { "data": "ProductType", "searchable": true },
            { "data": "ProductCostPrice", "searchable": false  },
            { "data": "ProductSalesPrice", "searchable": false },
            { "data": "Quantity", "searchable": false  },
            {
                "data": "OrderlineKey",
                "render": function (data, type, full) {
                    if (data != "") {
                        return "<a class='btn btn-sm' style='background-color:#8000FF; color:#fff;float: right;' onclick=addOrUpdateOrderlinePopup('" + data + "')><span class ='ti-pencil-alt' style='color:#fff'></span> Edit Orderline</a>";
                    }
                }
            },
            { "data": function (data, type, dataToSet) {
                    if (data != "") {
                        return "<a class='btn btn-danger btn-sm' style='color:#fff;float: left;' onclick=deleteOrderline('" + data.OrderlineKey + "." + data.LineNumber + "')><span class ='ti-trash' style='color:#fff'></span> Delete Orderline</a>";
                    }
                }
            }
        ],
        "scrollY": "2000px",
        "scrollCollapse": true,
        "pageLength": 100
    });
}


function addOrUpdateOrderlinePopup(Id) {
    //here we need to fire the popup
    $('#Orderline').val(Id);
    if (Id != '0') {
        $("#modalAddOrEdit").modal('show');
        $('.modal-title').text("Edit Orderline");
        loadProductTypes();
        //need to call facility that will help us bind the data 
        getOrderlineDetailsById();
    }
    else {
        $("#modalAddOrEdit").modal('show');
        $('.modal-title').text("Add Orderline");
        $('#txtLineNumber').val(numberOfEntriesIntheOrderlinesGrid());
        loadProductTypes();
    }
}

function AddOrUpdateOrderline() {

    if ($('#txtProductCode').val() == "" || /^[a-zA-Z0-9- ]*$/.test($('#txtProductCode').val()) == false) {
        toastr.error("Please insert a product code or remove special characters");
    }
    else if ($('#ddlProductTypes').val() == "-1") {
        toastr.error("Please select a product type");
    }
    else if ($('#txtProductCostPrice').val() == "" || /^[a-zA-Z0-9- ]*$/.test($('#txtProductCostPrice').val()) == false || /[a-zA-Z]/g.test($('#txtProductCostPrice').val()) == true) {
        toastr.error("Please insert a valid product cost price value");
    }
    else if ($('#txtProductSalesPrice').val() == "" || /^[a-zA-Z0-9- ]*$/.test($('#txtProductSalesPrice').val()) == false || /[a-zA-Z]/g.test($('#txtProductSalesPrice').val()) == true) {
        toastr.error("Please insert a valid sales price value");
    }
    else if ($('#txtQuantity').val() == "" || /^[a-zA-Z0-9- ]*$/.test($('#txtQuantity').val()) == false || /[a-zA-Z]/g.test($('#txtQuantity').val()) == true) {
        toastr.error("Please insert a valid quantity value");
    }
    else {
        var orderline = {
            'OrderlineKey': $('#Orderline').val(),
            'SalesOrderKey': $('#OrderId').val(),
            'LineNumber': $('#txtLineNumber').val(),
            'ProductCode': $('#txtProductCode').val(),
            'ProductTypeId': $('#ddlProductTypes').val(),
            'ProductCostPrice': $('#txtProductCostPrice').val(),
            'ProductSalesPrice': $('#txtProductSalesPrice').val(),
            'Quantity': $('#txtQuantity').val(),
        };

        swal({
            title: "Confirm",
            text: "Do you are about to add an orderline to this order",
            icon: "warning",
            buttons: true,
            showCancelButton: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    type: "POST",
                    url: "/Order/AddOrEditOrderlineAsync",
                    data: { orderline: orderline },
                    success: function (data) {
                        if (data.success) {
                            swal({
                                title: "Success",
                                text: "All done. Orderline Added Successfully",
                                icon: "success",
                                buttons: true,
                                showCancelButton: false,
                            }).then(function () {
                                clearAllPopupFields();
                                window.location.href = "/Order/Orderlines/" + $('#OrderId').val();
                            });
                        }
                        else {
                            toastr.error("An error occured while processing your request, please contact the IT team");
                        }
                    }
                });
            }
            else {
                clearAllPopupFields();
            }
        });
    }
}


function loadProductTypes() {
    $.ajax({
        type: "GET",
        url: "/Order/GetAllProductTypesAsync",
        data: "{}",
        success: function (data) {
            var s = '<option value="-1">Select a Product Type</option>';
            $.each(data.data, function (k, item) {
                s += "<option value=" + item.Id + ">" + item.ProductTypeDesc + "</option>";
            });
            $("#ddlProductTypes").html(s);

            //if (Id != "0") {
            //    ("#ddlProductTypes").val(Id);
            //}
        }
    });
}

function numberOfEntriesIntheOrderlinesGrid() {
    var table = new DataTable('#tblOrderlines');

    return table.rows().count() + 1;
}

function getOrderlineDetailsById() {
    $.ajax({
        type: "GET",
        url: "/Order/GetOrderlineByIdAsync/" + $('#Orderline').val(),
        data: "{}",
        success: function (data) {
            if (data.data != null) {
                var orderline = data.data
                $('#txtLineNumber').val(orderline.LineNumber);
                $('#txtProductCode').val(orderline.ProductCode);
                $('#ddlProductTypes').val(orderline.ProductTypeId);
                $('#txtProductCostPrice').val(orderline.ProductCostPrice);
                $('#txtProductSalesPrice').val(orderline.ProductSalesPrice);
                $('#txtQuantity').val(orderline.Quantity);
            }
        }
    });
}

function clearAllPopupFields() {
  /*  $('#txtLineNumber').val("");*/
    $('#txtProductCode').val("");
    $('#ddlProductTypes').val("");
    $('#txtProductCostPrice').val("");
    $('#txtProductSalesPrice').val("");
    $('#txtQuantity').val("");
}

function deleteOrderline(Id) {
    var archives = Id.split(".");
    var orderlineId = archives[0];
    var line = archives[1];

    var info = {
        "OrderId": $('#OrderId').val(),
        "OrderlineId": orderlineId,
        "OrderlineIndex": parseInt(line) - 1
    };

    swal({
        title: "Confirm",
        text: "You want to delete this orderline ?",
        icon: "warning",
        buttons: true,
        showCancelButton: true
    }).then(function (isConfirm) {
        if (isConfirm) {
            $.ajax({
                type: "POST",
                url: "/Order/DeleteOrderlineAsync",
                data: { info: info},
                success: function (data) {
                    swal({
                        title: "Success",
                        text: "You want to delete this orderline ?",
                        icon: "success",
                        buttons: true,
                        showCancelButton: false
                    }).then(function (isConfirm) {
                        clearAllPopupFields();
                        window.location.href = "/Order/Orderlines/" + $('#OrderId').val();
                    });
                }
            });
        }
    });


}

$(function () {
    $("body").click(function (e) {
        if (e.target.id == "modalAddOrEdit" || $(e.target).parents("#modalAddOrEdit").length) {
            //do not do nothing
        } else {
            clearAllPopupFields();
        }
    });
})