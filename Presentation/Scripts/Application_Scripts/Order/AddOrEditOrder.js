$(document).ready(function () {
    if ($('#OrderId').val() != 0) {
        //Process for editing an order 
        $.ajax({
            type: "GET",
            url: "/Order/GetSalesOrderByIdAsync/" + $('#OrderId').val(),
            data: "{}",
            success: function (data) {
                var orderData = data.data;
                if (orderData != null) {

                    $('#txtOrderNumber').val(orderData.OrderNumber); 
                    $('#txtCustomerName').val(orderData.CustomerName);
                    var createDate = orderData.CreateDate.replace("/", "-").replace("/", "-");
                    $('#txtCreateDate').val(orderData.CreateDate);
                    //$('#txtCreateDate').val(createDate.replace("/", "-"));

                    $.ajax({
                        type: "GET",
                        url: "/Order/GetAllOrderStatusesAsync",
                        data: "{}",
                        success: function (data) {
                            var s = '<option value="-1">Select a Order Status</option>';
                            $.each(data.data, function (k, item) {
                                s += "<option value=" + item.OrderStatusId + ">" + item.OrderStatusDesc + "</option>";
                            });
                            $("#ddlOrderStatus").html(s);
                            $('#ddlOrderStatus').val(orderData.OrderStatusId);
                        }
                    });

                    $.ajax({
                        type: "GET",
                        url: "/Order/GetAllOrderTypesAsync",
                        data: "{}",
                        success: function (data) {
                            var s = '<option value="-1">Select loan Status</option>';
                            $.each(data.data, function (k, item) {
                                s += "<option value=" + item.Id + ">" + item.OrderTypeDesc + "</option>";
                            });
                            $("#ddlOrderTypes").html(s);
                            $('#ddlOrderTypes').val(orderData.OrderTypeId);
                        }
                    });

                }
            }
        })
    }
    else {
        //process for adding an order 
        //get all order statuses
        $.ajax({
            type: "GET",
            url: "/Order/GetAllOrderStatusesAsync",
            data: "{}",
            success: function (data) {
                var s = '<option value="-1">Select a Order Status</option>';
                $.each(data.data, function (k, item) {
                    s += "<option value=" + item.OrderStatusId + ">" + item.OrderStatusDesc + "</option>";
                });
                $("#ddlOrderStatus").html(s);
            }
        });

        //get all order types 
        $.ajax({
            type: "GET",
            url: "/Order/GetAllOrderTypesAsync",
            data: "{}",
            success: function (data) {
                var s = '<option value="-1">Select  a Order Type</option>';
                $.each(data.data, function (k, item) {
                    s += "<option value=" + item.Id + ">" + item.OrderTypeDesc + "</option>";
                });
                $("#ddlOrderTypes").html(s);
            }
        });
    }
});

function AddOrUpdateOrder() {
    //validations 
    if ($('#txtOrderNumber').val() == "" || /^[a-zA-Z0-9- ]*$/.test($('#txtOrderNumber').val()) == false) {
        toastr.error("Please insert an order number or remove special characters");
    }
    else if ($('#ddlOrderTypes').val() == "-1") {
        toastr.error("Please Provide a order type");
    }
    else if ($('#ddlOrderStatus').val() == "-1") {
        toastr.error("Please Provide a order status");
    }
    else if ($('#txtCustomerName').val() == "" || /^[a-zA-Z0-9- ]*$/.test($('#txtCustomerName').val()) == false || /\d/.test($('#txtCustomerName').val())) {
        toastr.error("Please insert a customer name or remove special characters");
    }
    else if ($('#txtCreateDate').val() == "") {
        toastr.error("Please provide a create date");
    }
    else {
        var order = {
            'Id': $('#OrderId').val(),
            'OrderNumber': $('#txtOrderNumber').val(),
            'OrderTypeId': $('#ddlOrderTypes').val(),
            'OrderStatusId': $('#ddlOrderStatus').val(),
            'CustomerName': $('#txtCustomerName').val(),
            'CreateDate': $('#txtCreateDate').val()
        };

        swal({
            title: "Confirm",
            text: "Do you are about to register the order header information",
            icon: "warning",
            buttons: true,
            showCancelButton: true
        }).then(function (isConfirm) {
            if (isConfirm) {
                $.ajax({
                    type: "POST",
                    url: "/Order/AddOrEditOrderHeaderAsync",
                    data: { order: order },
                    success: function (data) {
                        if (data.success) {
                            //the operation went well 
                            window.location.href = "/Order/Index";
                        }
                        else {
                            toastr.error("An error occured while processing your request, please contact the IT team");
                        }
                    }
                });
            }
        });
    }
}