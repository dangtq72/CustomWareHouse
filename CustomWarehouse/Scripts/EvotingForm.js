//Check định dạng
function checkDate(p_name, p_id, p_val) {
    try {
        var isPass = isDate_ddMMyyyyHT(p_val);
        if (p_val == "") {
            jAlert("THÔNG BÁO", p_name + " không được để trống!", function () {
                $(p_id).focus();
            });
            return false;
        }
        if (isPass == 0) {
            jAlert("THÔNG BÁO", p_name + " " + p_val + " sai định dạng ngày dd/mm/yyyy!", function () {
                $(p_id).focus();
            });
            return false;
        }
        if (isPass == 1) {
            jAlert("THÔNG BÁO", p_name + " " + p_val + " không tồn tại!", function () {
                $(p_id).focus();
            });
            return false;
        }
        return true;
    }
    catch (e) {
        alert(e);
        return false;
    }
}

//Tạo ngày theo định dạng MM/dd/yyyy
function formatDate(p_date) {
    try {
        var date = p_date;
        var p_day = date.substr(0, 2);
        var p_month = date.substr(3, 2);
        var p_year = date.substr(6, 4);

        var new_date = new Date(p_year, p_month - 1, p_day);
        return new_date;
    } catch (e) {
        alert("Có lỗi xảy ra formatDate");
        return null;
    }
}
//Tạo ngày kèm theo giờ phút
function formatDateHT(p_date, p_hour, p_minute) {
    try {
        var date = p_date;
        var p_day = date.substr(0, 2);
        var p_month = date.substr(3, 2);
        var p_year = date.substr(6, 4);

        var new_date = new Date(p_year, p_month - 1, p_day, p_hour, p_minute);
        return new_date;
    } catch (e) {
        alert("Có lỗi xảy ra formatDateHT");
        return null;
    }
}
//So sánh 2 ngày với nhau
//type = 1: ngày 1 có lớn hơn ngày 2 ko
//type = 2: ngày 1 có nhỏ hơn ngày 2 ko
//type = 3: ngày 1 có lớn hơn ngày 2 ko bằng cũng đc
//type = 4: ngày 1 có nhỏ hơn ngày 2 ko bằng cũng đc
function compare2DateHT(p_date1, p_date2, type) {
    try {
        var date1 = p_date1.getTime();
        var date2 = p_date2.getTime();
        var result = date1 - date2;
      
        switch (type) {
            case 1:
                return (result >= 0);
                break;
            case 2:
                return (result <= 0);
                break;
            case 3:
                return (result > 0);
                break;
            case 4:
                return (result < 0);
                break;
        }
    } catch (e) {
        alert("Lỗi compare2Date");
        return null;
    }
}


function isValid(str) {
    return !/[~`@@!#$%\^&*+=\-\[\]\\';,/{}|\\":<>\?]/g.test(str);
}
//0: Sai định dạng 1: Ngày không tồn tại 2: Thành công
function isDate_ddMMyyyyHT(strDate) {
    var currVal = strDate;
    //Theo bug 78919 
    //var rxDatePattern = /^(\d{2})(\/|-)(\d{2})(\/|-)(\d{4})$/;
    var rxDatePattern = /^(\d{2})(\/)(\d{2})(\/)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    if (dtArray == null) {
        return 0;
    }
    dtDay = dtArray[1];
    dtMonth = dtArray[3];
    dtYear = dtArray[5];
    if (dtYear < 1000)
        return 1;
    else if (dtMonth < 1 || dtMonth > 12)
        return 1;
    else if (dtDay < 1 || dtDay > 31)
        return 1;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return 1;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return 1;
    }
    return 2;
}



function FormatSpace(p_str) {
    if (p_str == "") return null;
    var str_fun = p_str.trim();
    var char_search = ' ';
    var index_start = str_fun.indexOf(char_search);
    var index_end = 0;
    var str_result = "";

    while (index_start != -1) {
        for (i = index_start; i < str_fun.length; i++) {
            if (str_fun.charAt(i) != ' ') {
                index_end = i;
                if (index_end - index_start >= 2) {
                    str_result += str_fun.substr(0, index_start + 1);
                    str_fun = str_fun.substr(index_end, str_fun.length)
                }
                break;
            }
        }
        if (str_fun.indexOf('  ') == -1) {
            break;
        }
        index_start = str_fun.indexOf(char_search);
    }

    alert(str_result);
}

function splitString(p_str, p_start, p_end) {
    var part1 = p_str.substr(0, p_start + 1);
    var part2 = p_str.substr(p_end, p_str.length);
    return (part1 + part2);
}

function checkUnicode(p_val) {
    var p_val_lower = p_val.toLowerCase();
    var VietNamKey = "áàạảãâấầậẩẫăắằặẳẵéèẹẻẽêếềệểễóòọỏõôốồộổỗơớờợởỡúùụủũưứừựửữíìịỉĩđýỳỵỷỹ";
    for (var i = 0; i < p_val.length; i++) {
        if (VietNamKey.indexOf(p_val_lower[i]) != -1) {
            return false;
        }
    }
    return true;
}
function checkPassword(p_val) {
    var re = /(?=.*\d)(?=.*[A-z]).{8,}/;
    return re.test(p_val);
}

function checkCapsLock(e, div) {
    var capsLockON;
    keyCode = e.keyCode ? e.keyCode : e.which;
    shiftKey = e.shiftKey ? e.shiftKey : ((keyCode == 16) ? true : false);
    if (((keyCode >= 65 && keyCode <= 90) && !shiftKey) || ((keyCode >= 97 && keyCode <= 122) && shiftKey)) {
        capsLockON = true;
    } else {
        capsLockON = false;
    }
    if (!capsLockON)
        $(div).hide();
    else
        $(div).show();
}

function plus2Time(p_val) {
    if (p_val.length == 1) {
        return ("0" + p_val);
    }
    return p_val;
}