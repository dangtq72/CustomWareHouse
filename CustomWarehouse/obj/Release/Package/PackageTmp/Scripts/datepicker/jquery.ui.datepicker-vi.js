jQuery(function (e) { e.datepicker.regional["vi"] = { closeText: "Đóng", prevText: "&#x3c;Trước", nextText: "Tiếp&#x3e;", currentText: "Hôm nay", monthNames: ["Tháng Một", "Tháng Hai", "Tháng Ba", "Tháng Tư", "Tháng Năm", "Tháng Sáu", "Tháng Bảy", "Tháng Tám", "Tháng Chín", "Tháng Mười", "Tháng Mười Một", "Tháng Mười Hai"], monthNamesShort: ["Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12"], dayNames: ["Chủ Nhật", "Thứ Hai", "Thứ Ba", "Thứ Tư", "Thứ Năm", "Thứ Sáu", "Thứ Bảy"], dayNamesShort: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"], dayNamesMin: ["CN", "T2", "T3", "T4", "T5", "T6", "T7"], weekHeader: "Tu", dateFormat: "dd/mm/yy", firstDay: 0, isRTL: false, showMonthAfterYear: false, yearSuffix: "" }; e.datepicker.setDefaults(e.datepicker.regional["vi"]) })

//disable datepicker
//$("#txtToDate").datepicker('disable');
//$("#txtFromDate").datepicker('disable');
//Cachs dùng khai báo Class = "InputDate" vào text 
$(document).ready(function () {
    $(".InputDate").datepicker({
        showOtherMonths: true,
        selectOtherMonths: true,
        changeMonth: true,
        changeYear: true,
        dateFormat: "dd/mm/yy",
        showAnim: 'slide',
        yearRange: '-100:+100',
        showOn: "button",
        buttonImage: "/Content/themes/base/images/calendar-gray-icon.png",
        buttonImageOnly: true,
        buttonText: "Chọn ngày dd/MM/yyyy",
        onSelect: function () {
            $(this).focus();
        }
    }, $.datepicker.regional['vi']);


    $('.InputYear').datepicker({
        changeYear: true,
        dateFormat: 'yy',
        yearRange: '-100:+100',
        showAnim: 'slide',
        showOn: "button",
        buttonImage: "/Content/datepicker/ui-lightness/images/calendar-gray-icon.png",
        buttonImageOnly: true,
        buttonText: "Chọn năm",
        stepMonths: 12,
        onClose: function () {
            var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
            $(this).datepicker('setDate', new Date(year, 1, 1));
        },
        beforeShow: function () {
            if ((selectDate = $(this).val()).length > 0) {
                $(this).datepicker('option', 'defaultDate', new Date(selectDate, 1, 1));
                $(this).datepicker('setDate', new Date(selectDate, 1, 1));
            }
        }
    }, $.datepicker.regional['vi']);

     
});
//DongHT: Override lại hàm keydown của nó 
$.extend($.datepicker, {
    _doKeyDown: function (event) {
        var inst = $.datepicker._getInst(event.target);
        var handled = true;
        var isRTL = inst.dpDiv.is('.ui-datepicker-rtl');
        inst._keyEvent = true;
        if ($.datepicker._datepickerShowing)
            switch (event.keyCode) {
                case 9: $.datepicker._hideDatepicker();
                    handled = false;
                    break; // hide on tab out
                case 13: var sel = $('td.' + $.datepicker._dayOverClass + ':not(.' +
                                    $.datepicker._currentClass + ')', inst.dpDiv);
                    if (sel[0])
                        $.datepicker._selectDay(event.target, inst.selectedMonth, inst.selectedYear, sel[0]);
                    var onSelect = $.datepicker._get(inst, 'onSelect');
                    if (onSelect) {
                        var dateStr = $.datepicker._formatDate(inst);

                        // trigger custom callback
                        onSelect.apply((inst.input ? inst.input[0] : null), [dateStr, inst]);
                        $.datepicker._hideDatepicker();
                    }
                    else
                        $.datepicker._hideDatepicker();
                    //$(this).attr("onkeypress", "funcKiemTra()");
                    return false; // don't submit the form
                    break; // select the value on enter
                case 27: $.datepicker._hideDatepicker();
                    break; // hide on escape
                case 33: $.datepicker._adjustDate(event.target, (event.ctrlKey ?
                            -$.datepicker._get(inst, 'stepBigMonths') :
                            -$.datepicker._get(inst, 'stepMonths')), 'M');
                    break; // previous month/year on page up/+ ctrl
                case 34: $.datepicker._adjustDate(event.target, (event.ctrlKey ?
                            +$.datepicker._get(inst, 'stepBigMonths') :
                            +$.datepicker._get(inst, 'stepMonths')), 'M');
                    break; // next month/year on page down/+ ctrl
                case 35: if (event.ctrlKey || event.metaKey) $.datepicker._clearDate(event.target);
                    handled = event.ctrlKey || event.metaKey;
                    break; // clear on ctrl or command +end
                case 36: if (event.ctrlKey || event.metaKey) $.datepicker._gotoToday(event.target);
                    handled = event.ctrlKey || event.metaKey;
                    break; // current on ctrl or command +home
                case 37: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, (isRTL ? +1 : -1), 'D');
                    handled = event.ctrlKey || event.metaKey;
                    // -1 day on ctrl or command +left
                    if (event.originalEvent.altKey) $.datepicker._adjustDate(event.target, (event.ctrlKey ?
                                -$.datepicker._get(inst, 'stepBigMonths') :
                                -$.datepicker._get(inst, 'stepMonths')), 'M');
                    // next month/year on alt +left on Mac
                    break;
                case 38: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, -7, 'D');
                    handled = event.ctrlKey || event.metaKey;
                    break; // -1 week on ctrl or command +up
                case 39: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, (isRTL ? -1 : +1), 'D');
                    handled = event.ctrlKey || event.metaKey;
                    // +1 day on ctrl or command +right
                    if (event.originalEvent.altKey) $.datepicker._adjustDate(event.target, (event.ctrlKey ?
                                +$.datepicker._get(inst, 'stepBigMonths') :
                                +$.datepicker._get(inst, 'stepMonths')), 'M');
                    // next month/year on alt +right
                    break;
                case 40: if (event.ctrlKey || event.metaKey) $.datepicker._adjustDate(event.target, +7, 'D');
                    handled = event.ctrlKey || event.metaKey;
                    break; // +1 week on ctrl or command +down
                default: handled = false;
            }
        else if (event.keyCode == 36 && event.ctrlKey) // display the date picker on ctrl+home
            $.datepicker._showDatepicker(this);
        else {
            handled = false;
        }
        if (handled) {
            event.preventDefault();
            event.stopPropagation();
        }
    }
});
