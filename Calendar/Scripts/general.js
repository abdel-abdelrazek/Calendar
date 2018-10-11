$(function () {
    $('#dvStartDate').datetimepicker();
    $('#dvEndDate').datetimepicker({
        useCurrent: false
    });
    $("#dvStartDate").on("change.datetimepicker", function (e) {
        $('#dvEndDate').datetimepicker('minDate', e.date);

        $('#dvEndDate').datetimepicker('date', e.date.add(1, 'week'));
        
    });
    $("#dvEndDate").on("change.datetimepicker", function (e) {
        $('#dvStartDate').datetimepicker('maxDate', e.date);
    });

});




