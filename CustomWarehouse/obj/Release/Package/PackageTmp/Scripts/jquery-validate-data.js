//THÔNG BÁO KHI KHÔNG CÓ QUYỀN TRUY CẬP VÀO HÀM POST
var msgPermisstion = "Tài khoản không có quyền truy cập tới chức năng này";

//Sangdd
//hàm validate chuỗi không đc phép rỗng nếu "" đưa thôgn báo 
//p_Name :Dữ liệu vào check xem null hay "" hay không 
//p_MsgErr :Msg thông báo lỗi
//p_Tag :Id thẻ chứa mã lỗi sẽ đc appent vào cuối tag 


function ValidateMsg(p_Name, p_msgErr, p_Tag) {
    $("#divErr").remove();
    if (p_Name == null || p_Name.trim() == "" || p_Name.replace(/&nbsp;/g, '').trim() == "") {
        var _Err = " <div id=\"divErr\" style=\"color:red ;padding: 5px 0\">" + p_msgErr + "</div>";
        var _tagHtml = "#" + p_Tag;
        $(_tagHtml).append(_Err);
        return -1;
    } else {
        return 0;
    }
}
function moneyFormat(money) {
    var strFormat = (money == null) ? "0" : money.toString();
    var formatNumber = strFormat.replace(/\d(?=(?:\d{3})+(?!\d))/g, '$&,');
    return formatNumber;
}

function ShowMsgOnDivErr(p_msgErr, p_Tag) {
    $("#divErr").remove();
    var _Err = " <div id=\"divErr\" style=\"color:red ;padding: 5px 0\">" + p_msgErr + "</div>";
    var _tagHtml = "#" + p_Tag;
    $(_tagHtml).append(_Err);
}

//p_msgErr :Msg thông báo lỗi 
//p_Tag : Tag hiển thị
function DumplicateData(p_msgErr, p_Tag) {
    if ($("#divErr").length > 0) {
        $("#divErr").remove();
    }
    var _Err = " <div id=\"divErr\" style=\"color:red ;padding: 5px 0\">" + p_msgErr + "</div>";
    var _tagHtml = "#" + p_Tag;
    $(_tagHtml).append(_Err);
}


//Sangdd
//ham check số nhập vào có phải là số hay không 
//p_Number :Dữ liệu vào check xem có phải là số hay không 
//p_MsgErr :Msg thông báo lỗi
//p_Tag :Id thẻ chứa mã lỗi sẽ đc appent vào cuối tag 

function ValidateNumber(p_Number, p_MsgErr, p_Tag) {
    $("#divErr").remove();
    var _specialKey = '.,|';
    var text = p_Number.toString();

    for (var i = 0; i < _specialKey.length; i++) {
        if (text.indexOf(_specialKey[i]) != -1) {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        }
    }

    if (isNaN(p_Number)) {
        var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
        var _tagHtml = "#" + p_Tag;
        $(_tagHtml).append(_Err);
        return -1;
    } else {
        if (p_Number < 0) {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        }
        return 0;
    }
}



//Validate Number không cho phép giá trị số nhập vào là 0 
//p_IsMoreThanZero=1 check phải lớn hơn 0
function ValidateNumber(p_Number, p_MsgErr, p_Tag, p_IsMoreThanZero) {
    $("#divErr").remove();
    var _specialKey = '.,|';
    var text = p_Number.toString();
    if (p_IsMoreThanZero == 1 && p_Number == 0) {
        var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
        var _tagHtml = "#" + p_Tag;
        $(_tagHtml).append(_Err);
        return -1;
    }
    for (var i = 0; i < _specialKey.length; i++) {
        if (text.indexOf(_specialKey[i]) != -1) {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        }
    }

    if (isNaN(p_Number)) {
        var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
        var _tagHtml = "#" + p_Tag;
        $(_tagHtml).append(_Err);
        return -1;
    } else {
        if (p_Number < 0) {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        }
        return 0;
    }
}

//Ngay 16-07/2014
//Sangdd viet 
//viet ham goi khi chon anh anh hien thi
//hiển thị ảnh vào tag Image 
//p_btnSelect : Nút phát sinh sự kiện 
//p_txtPath : lưu đường dẫn chọn 
//pIdTagImage :Id add thêm thuộc tính src vào tag Img
function SelectImages(p_btnSelect, p_txtPath, pIdTagImage) {
    var _btn = "#" + p_btnSelect;
    var _txtpath = "#" + p_txtPath;
    var tagImg = "#" + pIdTagImage;
    $(function () {
        $(_btn).click(function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $(_txtpath).val(fileUrl);
                $(tagImg).attr('src', fileUrl);
            };
            finder.popup();
        });
    });
}
// chuyen sang ham nay su dung cac lan sau
function SelectImages(p_txtPath, pIdTagImage) {
    var _txtpath = "#" + p_txtPath;
    var tagImg = "#" + pIdTagImage;
    var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl) {
        $(_txtpath).val(fileUrl);
        $(tagImg).attr('src', fileUrl);
    };
    finder.popup();
}


//Ngay 16-07/2014
//Sangdd viet 
//Mục đich :Đồng hồ chạy 
function Clock() {
    var currentTime = new Date();
    var currentHours = currentTime.getHours();
    var currentMinutes = currentTime.getMinutes();
    var currentSeconds = currentTime.getSeconds();

    // Pad the minutes and seconds with leading zeros, if required
    currentMinutes = (currentMinutes < 10 ? "0" : "") + currentMinutes;
    currentSeconds = (currentSeconds < 10 ? "0" : "") + currentSeconds;

    // Choose either "AM" or "PM" as appropriate
    var timeOfDay = (currentHours < 12) ? "AM" : "PM";

    // Convert the hours component to 12-hour format if needed
    currentHours = (currentHours > 12) ? currentHours - 12 : currentHours;

    // Convert an hours component of "0" to "12"
    currentHours = (currentHours == 0) ? 12 : currentHours;

    // Compose the string for display
    var currentTimeString = currentHours + ":" + currentMinutes + ":" + currentSeconds + " " + timeOfDay;

    // Update the time display
    //document.getElementById("clock").firstChild.nodeValue = currentTimeString;

    var t = setTimeout(function () { Clock() }, 1000);
}

//Ngay 16-07/2014
//Sangdd viet 
//Mục đich :Check Leght text Nếu Leght của pStrContent > pIntMaxLeght
//thông báo lỗi else okie
//pStrContent :Nội dung check 
//pStrMsgErr: Thông báo lỗi 
//pTagHtml : Thẻ chứa thông báo lôi 
//pIntMaxLeght :MaxLeght đạt ngưỡng 
function CheckMaxLenght(pStrContent, pStrMsgErr, pTagHtml, pIntMaxLeght) {
    $("#divErr").remove(); //div hien thi thong bao loi 
    if (pStrContent.length > pIntMaxLeght) {
        var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + pStrMsgErr + "</div>";
        var _tagHtml = "#" + pTagHtml;
        $(_tagHtml).append(_Err);
        return -1;
    } else {
        return 0; //success
    }
}

function CheckMinLenght(pStrContent, pStrMsgErr, pTagHtml, pIntMinLeght) {
    $("#divErr").remove(); //div hien thi thong bao loi 
    if (pStrContent.length < pIntMinLeght) {
        var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + pStrMsgErr + "</div>";
        var _tagHtml = "#" + pTagHtml;
        $(_tagHtml).append(_Err);
        return -1;
    } else {
        return 0; //success
    }
}

//Ngay 16-07/2014
//Sangdd viet 
//ham popup khi nhan vao nut tro giup
function HelpPopUp() {
    $.confirm({
        'message': 'Cán bộ hỗ trợ trực tiếp:	 ABC XYZ	 0976767678 \n Hoặc quý khách có thể liên hệ với:		Bộ phận CSKH	 04.6674.5832 \n  hoặc gửi email tới địa chỉ support@navisoft.com.vn \n ',
        'buttons': {
            'OK': {
                'class': 'blue',
                'action': function () {
                }
            }
        }
    });
}

//Ngay 16-07/2014
//Sangdd viet 
//check session time out 
function CheckSessionTimeOut() {
    try {
        var rBool = false;
        $.ajax({
            type: 'POST',
            url: '/Home/CheckSessionTimeOut',
            dataType: "json",
            traditional: true,
            async: false,//Chuyen ve synchonize đồng bộ
            success: function (data) {
                if (data != null) {
                    if (data["Code"] == -1) {
                        rBool = false;
                        alert(data["Msg"]);
                        window.location.href = "/home/admin";
                    }
                    else {
                        rBool = true;
                    }
                }
                else {
                    rBool = false;
                }
            }
        });

        return rBool;
    } catch (e) {
        alert(e.toString);
        return false;
    }
}

//SANGDD THEM MOI MUC DICH VUA CHECK SESSION TIME OUT VUA CHECK LUON QUYEN TRUY CAP 
//pFunctionName :Ten ham Post hay get tren form 
function CheckSessionTimeOutAndPermistion(pFunctionName) {
    try {
        var ischekc = true;
        $.ajax({
            type: 'POST',
            url: '/Login/CheckSessionTimeOutAndPermistion',
            data: { pFuncName: pFunctionName },
            dataType: "json",
            traditional: true,
            async: false,//Chuyen ve synchonize đồng bộ
            success: function (data) {
                if (data != null && data["Code"] == -1) {
                    alert(data["Msg"]);
                    window.location.href = "/Login/login"
                } else if (data != null && data["Code"] == -99) { //khong co quyen truy cap vao chuc nang 
                    ischekc = false;
                }
            }
        });
        return ischekc;
    } catch (e) {
        alert(e.toString);
    }
}

function DispalyFileUpload(pObjInput, pTagImages) {
    try {
        var _tagImg = "#" + pTagImages;
        if (pObjInput.files && pObjInput.files[0]) {
            var reader = new FileReader();
            reader.onload = function (e) {
                $(_tagImg)
                .attr('src', e.target.result);
            };
            reader.readAsDataURL(pObjInput.files[0]);
        }
    } catch (e) {
        alert('Loi DispalyFileUpload' + e.message);
    }
}

function UploadImages(pbtnUpLoad, pFileUpload, ptagDisplayImg, _txtpathImage, pUrl) {
    try {
        document.getElementById(pbtnUpLoad).disabled = true;
        var btnUpload = '#' + pbtnUpLoad;
        var _tagImg = '#' + ptagDisplayImg;
        var pathName = '#' + _txtpathImage;
        $(document).ready(function () {
            $(btnUpload).click(function () {
                var pfile = document.getElementById(pFileUpload).files[0];//lay du lieu file
                var data = new FormData();// tao ra 1 form data de truyen dữ liệu thông qua nó
                data.append('pfile', pfile);// thêm dữ liệu với name và value - name trùng với name HttpPostFileBase
                $.ajax({
                    url: pUrl,
                    type: 'POST',
                    data: data,
                    enctype: 'multipart/form-data',
                    processData: false,
                    contentType: false,
                    success: function (data) {
                        if (data != null && data.success == 0) {
                            //alert('Upload Successful !');
                            $(_tagImg)
                            .attr('src', data.pathFile);
                            $(pathName).attr('value', data.pathFile);
                            //alert(data.pathFile);
                            document.getElementById(pbtnUpLoad).disabled = false;
                        }
                        else {
                            alert('Upload Fail !');
                            $(_tagImg)
                             .attr('src', '#');
                            $(pathName).attr('value', '');
                            document.getElementById(pbtnUpLoad).disabled = false;
                        }
                    },
                });
            });
        });
        document.getElementById(pbtnUpLoad).disabled = false;
    } catch (e) {
        alert('Loi ham UploadImages ' + e.message);
        document.getElementById(pbtnUpLoad).disabled = false;
    }
}

//Sangdd cap nhat lai Layout khi duoc chon 
function ChangeSourceImages(pCheckBox, pSource, pTagChangeSource) {
    try {
        var _tagImg = '#' + pTagChangeSource;
        var tagCkb = '#' + pCheckBox;
        $(document).ready(function () {
            $(tagCkb).change(function () {
                $(_tagImg)
                 .attr('src', pSource);
            });
        });
    }
    catch (e) {
        alert('Loi DispalyFileUpload ' + e.message);
    }
}

// Sangdd 
// Hàm check dữ liệu nhập vào có chứa kỹ tự kiểu unicode hay không 
// return true nếu ko chứ 
// return false nếu có
function FuncCheckUnikey(pStrString, p_MsgErr, p_Tag) {
    var VietNamKey = "á,à,ạ,ả,ã,â,ấ,ầ,ậ,ẩ,ẫ,ă,ắ,ằ,ặ,ẳ,ẵ,é,è,ẹ,ẻ,ẽ,ê,ế,ề,ệ,ể,ễ,ó,ò,ọ,ỏ,õ,ô,ố,ồ,ộ,ổ,ỗ,ơ,ớ,ờ,ợ,ở,ỡ,ú,ù,ụ,ủ,ũ,ư,ứ,ừ,ự,ử,ữ,í,ì,ị,ỉ,ĩ,đ,ý,ỳ,ỵ,ỷ,ỹ,Á,À,Ạ,Ả,Ã,Â,Ấ,Ầ,Ậ,Ẩ,Ẫ,Ă,Ắ,Ằ,Ặ,Ẳ,Ẵ,É,È,Ẹ,Ẻ,Ẽ,Ê,Ế,Ề,Ệ,Ể,Ễ,Ó,Ò,Ọ,Ỏ,Õ,Ô,Ố,Ồ,Ộ,Ổ,Ỗ,Ơ,Ớ,Ờ,Ợ,Ở,Ỡ,Ú,Ù,Ụ,Ủ,Ũ,Ư,Ứ,Ừ,Ự,Ử,Ữ,Í,Ì,Ị,Ỉ,Ĩ,Đ,Ý,Ỳ,Ỵ,Ỷ,Ỹ";
    $("#divErr").remove();
    for (var i = 0; i < pStrString.length; i++) {
        if (VietNamKey.indexOf(pStrString[i]) != -1) {
            var _Err = " <div id=\"divErr\" style=\"color:red;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_MsgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        }
    }
    return 0;
}

///Sangdd
//Ham chuyen tu tieng viet co dau sang khong dau  
function ReplaceUnicode(str) {
    try {
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
        /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
        str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
        str = str.replace(/^\-+|\-+$/g, "");
        //cắt bỏ ký tự - ở đầu và cuối chuỗi
        return str;
    } catch (e) {
        alert('Loi hàm ReplaceUnicode' + e.message);
    }
};

function ReplaceUnicode(str, pTagSetValues, pTagSetValues2) {
    try {
        var tag1 = '#' + pTagSetValues;
        var tag2 = '#' + pTagSetValues2;
        str = str.toLowerCase();
        str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
        str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
        str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
        str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
        str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
        str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
        str = str.replace(/đ/g, "d");
        str = str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g, "-");
        /* tìm và thay thế các kí tự đặc biệt trong chuỗi sang kí tự - */
        str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
        str = str.replace(/^\-+|\-+$/g, "");
        //cắt bỏ ký tự - ở đầu và cuối chuỗi
        $(tag1).val(str);
        $(tag2).val(str);
    } catch (e) {
        alert('Loi hàm ReplaceUnicode' + e.message);
    }
};

//sangdd 
//ham check ky tu dac biet 
function CheckKyTuDacBiet(str, p_MsgErr, p_Tag) {
    try {
        var _specialKey = '|,}{@+&=!?;/#\"$%^*()<>`~[]\\';
        for (var i = 0; i < _specialKey.length; i++) {

            if (str.indexOf(_specialKey[i]) != -1) {
                var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_MsgErr + "</div>";
                var _tagHtml = "#" + p_Tag;
                $(_tagHtml).append(_Err);
                return -1;
            }
        }


        return 0;
    } catch (e) {
        alert('Loi hàm CheckKyTuDacBiet' + e.message); return -1;
    }
}

function CheckKyTuDacBietShoKyTu(str) {
    try {
        var _specialKey = '|,}{@+&=!?;/#\"$%^*()<>`~[]\\';
        for (var i = 0; i < _specialKey.length; i++) {

            if (str.indexOf(_specialKey[i]) != -1) {
                return _specialKey[i];
            }
        }
        return '';
    } catch (e) {
        alert('Loi hàm CheckKyTuDacBiet' + e.message); return -1;
    }
}

function validate_url(_url) {
    var urlregex = /^(http|https):\/\/(([a-zA-Z0-9$\-_.+!*'(),;:&=]|%[0-9a-fA-F]{2})+@)?(((25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[1-9][0-9]|[0-9])(\.(25[0-5]|2[0-4][0-9]|[0-1][0-9][0-9]|[1-9][0-9]|[0-9])){3})|localhost|([a-zA-Z0-9\-\u00C0-\u017F]+\.)+([a-zA-Z]{2,}))(:[0-9]+)?(\/(([a-zA-Z0-9$\-_.+!*'(),;:@&=]|%[0-9a-fA-F]{2})*(\/([a-zA-Z0-9$\-_.+!*'(),;:@&=]|%[0-9a-fA-F]{2})*)*)?(\?([a-zA-Z0-9$\-_.+!*'(),;:@&=\/?]|%[0-9a-fA-F]{2})*)?(\#([a-zA-Z0-9$\-_.+!*'(),;:@&=\/?]|%[0-9a-fA-F]{2})*)?)?$/;
    return urlregex.test(_url);
}


function GetIndexOf(str) {
    try {
        var _index = -1;
        for (var i = 0; i < str.length; i++) {
            if (str[i] == '/') {
                _index = i;
            }
        }
        return _index;
    } catch (e) {
        alert('Loi hàm GetIndexOf' + e.message); return -1;
    }
}


//Hàm so sánh ngày 
function CompareDate(p_Date1, p_Date2, p_msgErr, p_Tag) {
    $("#divErr").remove();
    if (p_Date1 != "" || p_Date2 != "") {
        var NDK; var NNhanLK;
        var date1 = FunReturnFomatDate(p_Date1);
        if (date1 != null)
            NDK = Date.parse(date1);
        else {
            alert('Ngày đăng ký nhập vào không đúng định dạng');
            return -1;
        }
        var date2 = FunReturnFomatDate(p_Date2);
        if (date2 != null)
            NNhanLK = Date.parse(date2);
        else {
            alert('Ngày đăng ký nhập vào không đúng định dạng');
            return -1;
        }
        if (NDK > NNhanLK) {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        } else {
            return 0;
        }
    }
}
//ham chuyen ve dinh dang format date trong JavaScript 
function FunReturnFomatDate(pDate) {
    var dateParts = pDate.split('/');
    if (dateParts.length != 3)
        return null;
    var year = dateParts[2];
    var month = dateParts[1];
    var day = dateParts[0];
    if (isNaN(day) || isNaN(month) || isNaN(year))
        return null;
    var date = year + "-" + month + "-" + day;
    return date;
}

//Chi cho phep nhap so
function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57))
        return false;
    return true;
}

// Cắt toàn bộ số 0 trước box nhập số
function jsReaplace0AtFirst(nStr, nControl, nBool) {
    var before_length = nControl.value.length; // lấy chiều dài trước khi thay đổi
    var key_index = doGetCaretPosition(nControl); // lấy vị trí con trỏ hiện tại
    nStr = nStr.replace(/[,]/g, '');
    if (nBool) { // nếu cho phép nhập số 0
        while (nStr.length > 1 && nStr.indexOf(0) == '0') {
            nStr = nStr.substring(1);
        }
    }
    else { // chỉ cho nhập từ 1 trở lên
        while (nStr.indexOf(0) == '0') {
            nStr = nStr.substring(1);
        }
    }
    nControl.value = nStr;
    var after_length = nControl.value.length; // lấy thay đổi chiều dài
    setCaretPosition(nControl, key_index + (parseInt(after_length) - parseInt(before_length))); // set vị trí con trỏ
}


//ham tro ve trang truoc 
function GoBack(purl) {
    window.history.back();
    return true;
}

function CheckValidateNumberInt(_number) {
    var _Regex = new RegExp('^[0-9,]+$');
    if (_Regex.test(_number))//neu la so thi lay
    {
        return 1;
    }
    else {
        return -1
    }
}

//ham fomat kieu tien te khi nhap textbox

function formatNumber(nStr) {
    if (nStr.indexOf('.') != -1)//neu nhap vao dang 1.000.000.000 thi doi thanh 1,000,000,000
    {
        // thay tat ca dau '.' bang dau ','         
        while (nStr.indexOf('.') > -1) {
            nStr = nStr.replace('.', '');
        }
    }
    var _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9,]+$');
    //var _RegexChar = new RegExp('[|,}{+&-=!?;/#\"$%^*()<>`~[]\\]+$');     
    for (var i = 0; i < _Vondieule.length - 1; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Vondieule[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    nStr = _Vondieule;

    // cắt toàn bộ số 0 trước trường số
    nStr = nStr.replace(/[,]/g, '');
    while (nStr.indexOf(0) == '0' && nStr.length > 1) {
        nStr = nStr.substring(1);
    }

    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    return x1 + x2;
}

// fomat kiểu number có đấu , ở hàng nghìn và có cả phần thập phân
function jsFormatFloatNumber(nStr, txtControlId) {

    //if (nStr.indexOf('.') != -1)//neu nhap vao dang 1.000.000.000 thi doi thanh 1,000,000,000
    //{
    //    // thay tat ca dau '.' bang dau ','         
    //    while (nStr.indexOf('.') > -1) {
    //        nStr = nStr.replace('.', '');
    //    }
    //}
    var _IndexFloat = nStr.indexOf('.');
    var _PhanThapPhan = "";
    var _count_ = 0;
    var _Alltext = "";
    _count_ = (nStr.split(".").length - 1);
    if (_count_ > 1)// nếu có tồn tại 2 dấu . thì cắt dấu toàn bộ từ dấu . thứ 2 đến hết
    {
        var Fst = nStr.indexOf('.');
        var Snd = nStr.indexOf('.', Fst + 1);// vị trí index của thằng . thứ 2
        nStr = nStr.substring(0, Snd);// xóa hết từ thằng . thứ 2
    }
    if (_IndexFloat >= 0)// nếu có dấu . thì mới làm tiếp
    {
        _PhanThapPhan = nStr.substring(_IndexFloat, nStr.length);
        // cắt lấy phần nguyên để format trước
        nStr = nStr.substring(0, _IndexFloat);
    }
    var _Vondieule = "";
    _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9,.]+$');
    _Alltext = _Vondieule + _PhanThapPhan;
    for (var i = 0; i < _Alltext.length ; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Alltext[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
        else {
            $("input[id=" + txtControlId + "]").val(_tempvondieule);
            return;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    nStr = _Vondieule;

    // cắt toàn bộ số 0 trước trường số
    nStr = nStr.replace(/[,]/g, '');
    while (nStr.indexOf(0) == '0' && nStr.length > 1) {
        nStr = nStr.substring(1);
    }
    var ctrl = document.getElementById(txtControlId);

    var before_length = ctrl.value.length; // lấy chiều dài trước khi thay đổi
    var key_index = doGetCaretPosition(ctrl); // lấy vị trí con trỏ hiện tại

    // 
    //nStr += '';
    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var result = x1 + x2;
    $("input[id=" + txtControlId + "]").val(result + _PhanThapPhan);

    var after_length = document.getElementById(txtControlId).value.length; // lấy thay đổi chiều dài
    setCaretPosition(ctrl, key_index + (parseInt(after_length) - parseInt(before_length))); // set vị trí con trỏ
}

// fomat kiểu number có đấu , ở hàng nghìn và có cả phần thập phân, có thể nhập số âm
function jsFormatFloatNumber_SoAm(nStr, txtControlId) {
    var _So_am = false;
    if (nStr.indexOf('-') != -1) {
        _So_am = true;
    }
    nStr = nStr.replace('-', '');// cat so am di

    var _IndexFloat = nStr.indexOf('.');
    var _PhanThapPhan = "";
    var _count_ = 0;
    var _Alltext = "";
    _count_ = (nStr.split(".").length - 1);
    if (_count_ > 1)// nếu có tồn tại 2 dấu . thì cắt dấu toàn bộ từ dấu . thứ 2 đến hết
    {
        var Fst = nStr.indexOf('.');
        var Snd = nStr.indexOf('.', Fst + 1);// vị trí index của thằng . thứ 2
        nStr = nStr.substring(0, Snd);// xóa hết từ thằng . thứ 2
    }
    if (_IndexFloat >= 0)// nếu có dấu . thì mới làm tiếp
    {
        _PhanThapPhan = nStr.substring(_IndexFloat, nStr.length);
        // cắt lấy phần nguyên để format trước
        nStr = nStr.substring(0, _IndexFloat);
    }
    var _Vondieule = "";
    _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9,.]+$');
    _Alltext = _Vondieule + _PhanThapPhan;
    for (var i = 0; i < _Alltext.length ; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Alltext[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
        else {
            if (_So_am == true) {
                _tempvondieule = "-" + _tempvondieule.toString();
            }
            $("input[id=" + txtControlId + "]").val(_tempvondieule);
            return;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    nStr = _Vondieule;

    // cắt toàn bộ số 0 trước trường số
    nStr = nStr.replace(/[,]/g, '');
    while (nStr.indexOf(0) == '0' && nStr.length > 1) {
        nStr = nStr.substring(1);
    }
    var ctrl = document.getElementById(txtControlId);

    var before_length = ctrl.value.length; // lấy chiều dài trước khi thay đổi
    var key_index = doGetCaretPosition(ctrl); // lấy vị trí con trỏ hiện tại

    // 
    //nStr += '';
    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var result = x1 + x2;
    _tempvondieule = result + _PhanThapPhan
    if (_So_am == true) {
        _tempvondieule = "-" + _tempvondieule.toString();
    }
    $("input[id=" + txtControlId + "]").val(_tempvondieule);

    var after_length = document.getElementById(txtControlId).value.length; // lấy thay đổi chiều dài
    setCaretPosition(ctrl, key_index + (parseInt(after_length) - parseInt(before_length))); // set vị trí con trỏ
}


function jsFormatNumber(nStr, txtControlId) {
    if (nStr.indexOf('.') != -1)//neu nhap vao dang 1.000.000.000 thi doi thanh 1,000,000,000
    {
        // thay tat ca dau '.' bang dau ','         
        while (nStr.indexOf('.') > -1) {
            nStr = nStr.replace('.', '');
        }
    }
    var _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9,]+$');
    //var _RegexChar = new RegExp('[|,}{+&-=!?;/#\"$%^*()<>`~[]\\]+$');     
    for (var i = 0; i < _Vondieule.length - 1; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Vondieule[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
        else {
            $("input[id=" + txtControlId + "]").val(_tempvondieule);
            return;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    nStr = _Vondieule;

    // cắt toàn bộ số 0 trước trường số
    nStr = nStr.replace(/[,]/g, '');
    while (nStr.indexOf(0) == '0' && nStr.length > 1) {
        nStr = nStr.substring(1);
    }
    var ctrl = document.getElementById(txtControlId);

    var before_length = ctrl.value.length; // lấy chiều dài trước khi thay đổi
    var key_index = doGetCaretPosition(ctrl); // lấy vị trí con trỏ hiện tại

    // 
    //nStr += '';
    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var result = x1 + x2;
    $("input[id=" + txtControlId + "]").val(result);

    var after_length = document.getElementById(txtControlId).value.length; // lấy thay đổi chiều dài
    setCaretPosition(ctrl, key_index + (parseInt(after_length) - parseInt(before_length))); // set vị trí con trỏ
}



//------lấy vị trí hiện tại con trỏ trong textbox----------
function doGetCaretPosition(ctrl) {
    var CaretPos = 0;   // IE Support
    if (document.selection) {
        ctrl.focus();
        var Sel = document.selection.createRange();
        Sel.moveStart('character', -ctrl.value.length);
        CaretPos = Sel.text.length;
    }
        // Firefox support
    else if (ctrl.selectionStart || ctrl.selectionStart == '0')
        CaretPos = ctrl.selectionStart;
    return (CaretPos);
}

//------sét vị trí con trỏ trong textbox----------
function setCaretPosition(ctrl, pos) {
    if (ctrl.setSelectionRange) {
        ctrl.focus();
        ctrl.setSelectionRange(pos, pos);
    }
    else if (ctrl.createTextRange) {
        var range = ctrl.createTextRange();
        range.collapse(true);
        range.moveEnd('character', pos);
        range.moveStart('character', pos);
        range.select();
    }
}

function Trimspace(str) {
    return str.replace(/^\s+|\s+$/gm, '');
}


function ReplaceComa(pStr, pKeyReplace) {
    while (pStr.indexOf(pKeyReplace) > 0) {
        pStr = pStr.replace(pKeyReplace, "");
    }
    return pStr;
}

//lay ra loại bài viết
function gAticlesType() {
    try {
        var PreArrUrl = ''; var preurl = window.location.href;
        if (navigator.userAgent.indexOf("Chrome") > 0) {
            //chrome
            PreArrUrl = preurl.split("%7C");
        } else {
            //firefox
            PreArrUrl = preurl.split("|");
        }
        if (PreArrUrl.length == 3) {
            preValues = PreArrUrl[1];
        }
        return parseInt(preValues);
    } catch (e) { }
}

//regex email
function validateEmail(email) {
    var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
    if (reg.test(email)) {
        return true;
    }
    else {
        return false;
    }
}
//regex email
//email :Dữ liệu cần check
//p_MsgErr :Msg thông báo lỗi
//p_Tag :Id thẻ chứa mã lỗi sẽ đc appent vào cuối tag 
function validateEmail(email, p_msgErr, p_Tag) {
    var reg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/
    if (Trimspace(email) !== "") {
        if (reg.test(email)) {
            return 0;
        }
        else {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        }
    }
}
//regex telephone
function validateTel(tel) {
    var reg = /^([^a-zA-Z]+)$/
    var VietNamKey = "á,à,ạ,ả,ã,â,ấ,ầ,ậ,ẩ,ẫ,ă,ắ,ằ,ặ,ẳ,ẵ,é,è,ẹ,ẻ,ẽ,ê,ế,ề,ệ,ể,ễ,ó,ò,ọ,ỏ,õ,ô,ố,ồ,ộ,ổ,ỗ,ơ,ớ,ờ,ợ,ở,ỡ,ú,ù,ụ,ủ,ũ,ư,ứ,ừ,ự,ử,ữ,í,ì,ị,ỉ,ĩ,đ,ý,ỳ,ỵ,ỷ,ỹ,Á,À,Ạ,Ả,Ã,Â,Ấ,Ầ,Ậ,Ẩ,Ẫ,Ă,Ắ,Ằ,Ặ,Ẳ,Ẵ,É,È,Ẹ,Ẻ,Ẽ,Ê,Ế,Ề,Ệ,Ể,Ễ,Ó,Ò,Ọ,Ỏ,Õ,Ô,Ố,Ồ,Ộ,Ổ,Ỗ,Ơ,Ớ,Ờ,Ợ,Ở,Ỡ,Ú,Ù,Ụ,Ủ,Ũ,Ư,Ứ,Ừ,Ự,Ử,Ữ,Í,Ì,Ị,Ỉ,Ĩ,Đ,Ý,Ỳ,Ỵ,Ỷ,Ỹ";
    for (var i = 0; i < tel.length; i++) {
        if (VietNamKey.indexOf(tel[i]) != -1) {
            return false;
        }
    }
    if (reg.test(tel)) {
        return true;
    }
    else {
        return false;
    }
}
//hàm validate phone trả về tag error thông báo lỗi
//tel :Dữ liệu cần check
//p_MsgErr :Msg thông báo lỗi
//p_Tag :Id thẻ chứa mã lỗi sẽ đc appent vào cuối tag 
function ValidateTel(tel, p_msgErr, p_Tag) {
    $("#divErr").remove();
    //không cho nhập chữ - còn lại các ký tự đặc biệt hoặc là số thì ok
    var reg = /^([^a-zA-Z]+)$/
    //var reg = /\(?([0-9]{3})\)?([ .-]?)([0-9]{3})\2([0-9]{4})/
    if (Trimspace(tel) !== "") {
        if (reg.test(tel)) {
            return 0;
        }
        else {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_msgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
            return -1;
        }
    }
}

function ValidateDate(strDate) {
    if (strDate != '') {
        var regex = /^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;
        if (!(regex.test(strDate))) {
            return false;
        }
    }
    return true;
}


 

//hungTD sua ham nay check ca nhung ngay nhu 30/02 hoac nam nhuan
function CheckValiateDate(date) {
    var matches = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(date);
    if (matches == null) return false;
    var m = matches[2];
    var d = matches[1];
    var y = matches[3];
    //var composedDate = new Date(y, m, d);
    var _dateTime = new Date(m + '/' + d + '/' + y);

    var _month = _dateTime.getMonth() + 1;
    var _day = _dateTime.getDate();
    var _year = _dateTime.getFullYear();
    if (m != _month || _day != d || _year != y) {
        return false;
    }
    else {
        return true;
    }
}

function CheckDateArlert(date) {
    var matches = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(date);
    if (matches == null) { alert("Ngày nhập vào không đúng định dạng dd/mm/yyyy"); return false; }
    var m = matches[2];
    var d = matches[1];
    var y = matches[3];
    //var composedDate = new Date(y, m, d);
    var _dateTime = new Date(m + '/' + d + '/' + y);

    var _month = _dateTime.getMonth() + 1;
    var _day = _dateTime.getDate();
    var _year = _dateTime.getFullYear();
    if (m != _month || _day != d || _year != y) {
        alert("Ngày nhập vào không đúng định dạng dd/mm/yyyy");
        return false;
    }
    else {
        return true;
    }
}

//ham validate date tra ve tag error thong bao loi 
//strDate :Dữ liệu cần check
//p_MsgErr :Msg thông báo lỗi
//p_Tag :Id thẻ chứa mã lỗi sẽ đc appent vào cuối tag 
function ValidateDate(strDate, p_msgErr, p_Tag) {
    if (p_msgErr == undefined && p_Tag == undefined) {
        if (strDate != '') {
            var regex = /^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;
            if (!(regex.test(strDate))) {
                return false;
            }
        }
        return true;
    } else {
        $("#divErr").remove();
        if (strDate != '') {
            var regex = /^(((0?[1-9]|[12]\d|3[01])\/(0?[13578]|1[02])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|[12]\d|30)\/(0?[13456789]|1[012])\/((1[6-9]|[2-9]\d)\d{2}))|((0?[1-9]|1\d|2[0-8])\/0?2\/((1[6-9]|[2-9]\d)\d{2}))|(29\/0?2\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$/;
            if (!(regex.test(strDate))) {
                var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_msgErr + "</div>";
                var _tagHtml = "#" + p_Tag;
                $(_tagHtml).append(_Err);
                return -1;
            } else return 0;
        }
    }
}


//_name: ten class
//IsCheckbox : nếu là check box thì truyền vào true- nếu là radio button thì false
// trả về list id những thằng có checked = true : id1|id2|....Idn|
function GetValueControll(_name, IsCheckbox) {
    var _ListControl = $("." + _name);
    var _Control = "";
    for (var i = 0; i < _ListControl.length; i++) {
        var _Item = _ListControl[i];
        if (_Item.checked == true) {
            if (IsCheckbox == false)// neu la radio thi break luon
            {
                _Control = _Item.value;
                break;
            }
            else {
                if (_ListControl.length == 1 || _Control == '') {
                    _Control = "|" + _Item.value + "|";
                }
                else if (i <= _ListControl.length) {
                    _Control = _Control + _Item.value + "|";
                }
            }

        }
    }

    return _Control;
}

//_name: ten class
//IsCheckbox : nếu là check box thì truyền vào true- nếu là radio button thì false
// trả về list id những thằng có checked = false : id1|id2|....Idn|
function GetValueControllUnCheck(_name, IsCheckbox) {
    var _ListControl = $("." + _name);
    var _Control = "";
    for (var i = 0; i < _ListControl.length; i++) {
        var _Item = _ListControl[i];
        if (_Item.checked == false) {
            if (_ListControl.length == 1 || _Control == '') {
                _Control = "|" + _Item.value + "|";
            }
            else if (i <= _ListControl.length) {
                _Control = _Control + _Item.value + "|";
            }
        }
    }

    return _Control;
}


//Thằng menu hover ở danh sách lưới
$(".hiddenMenu").hover(function () {
    var pos_top = $(this).position().top;
    var pos_left = $(this).position().left;
    var wthis = $(this).width();
    $(this).find(".MenuHover").css("display", "inline-block");
    $(this).find(".MenuHover").css("top", pos_top + "px");
    $(this).find(".MenuHover").css("left", (pos_left + wthis) + "px");
}, function () {
    $(this).find(".MenuHover").css("display", "none");
});


//sangdd :focus vào cuối của text
//pIdTag : IDtag Input text cần Focus
function FocusToEnd(pIdTag) {
    var pId = "#" + pIdTag;
    $(pId).focus().val($(pId).val());
}

//sangdd 
//FOCUST TO END TEXT AREA 
//$('#Question').putCursorAtEnd();
jQuery.fn.putCursorAtEnd = function () {

    return this.each(function () {

        $(this).focus()

        // If this function exists...
        if (this.setSelectionRange) {
            // ... then use it (Doesn't work in IE)

            // Double the length because Opera is inconsistent about whether a carriage return is one character or two. Sigh.
            var len = $(this).val().length * 2;

            this.setSelectionRange(len, len);

        } else {
            // ... otherwise replace the contents with itself
            // (Doesn't work in Google Chrome)

            $(this).val($(this).val());

        }

        // Scroll to the bottom, in case we're in a tall textarea
        // (Necessary for Firefox and Google Chrome)
        this.scrollTop = 999999;

    });
};

/*
Purpose:Hàm check show and hide password
Parameters: e: -> nút xảy ra sự kiện, objectPassword: tag html bị tác động bởi sự kiện
Return: input type text or input type password
*/
showpass = function (e, objectPassword) {
    $(e).toggleClass("pass_show");
    if ($(e).hasClass("pass_show")) {
        $(e).text("Ẩn password");
        $(objectPassword).prop("type", "text");
    }
    else {
        $(e).text("Hiển thị password");
        $(objectPassword).prop("type", "password");
    }
};
/*
Purpose:Hàm so sánh ngày
Paramerter: p_date 1, p_date2, p_compare_type :0 so sánh  =, 1 so sánh >, 2 so sánh < , 3 so sánh >= , 4 so sánh <=, 5 so sánh !=
            p_msgErr: thông báo lỗi, p_Tag: tag chứa div err
Return true or false -> submit or no
*/
function checkDateCompare(p_date1, p_date2, p_compare_type, p_msgErr, p_Tag) {

    $("#divErr").remove();
    var arrdate1 = p_date1.split("/");
    var arrdate2 = p_date2.split("/");
    //nếu như tháng < 10 thì + số 0 trước tháng:09
    if (arrdate1[1].length == 1) {
        arrdate1[1] = "0" + arrdate1[1];
    }
    if (arrdate2[1].length == 1) {
        arrdate2[1] = "0" + arrdate2[1];
    }
    //chuyển p_date1 và p_date 2 về cùng định dạng
    var date1 = new Date(arrdate1[2], arrdate1[1] - 1, arrdate1[0]);
    var date2 = new Date(arrdate2[2], arrdate2[1] - 1, arrdate2[0]);


    switch (p_compare_type) {
        case 0:
            {
                if (date1 == date2) {
                    var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
                    var _tagHtml = "#" + p_Tag;
                    $(_tagHtml).append(_Err);
                    return -1;
                }
                return 0;
            }
        case 1:
            {
                if (date1 > date2) {
                    var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
                    var _tagHtml = "#" + p_Tag;
                    $(_tagHtml).append(_Err);
                    return -1;
                }
                return 0;
            }
        case 2:
            {
                if (date1 < date2) {
                    var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
                    var _tagHtml = "#" + p_Tag;
                    $(_tagHtml).append(_Err);
                    return -1;
                }
                return 0;
            }
        case 3:
            {
                if (date1 >= date2) {
                    var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
                    var _tagHtml = "#" + p_Tag;
                    $(_tagHtml).append(_Err);
                    return -1;
                }
                return 0;
            }
        case 4:
            {
                if (date1 <= date2) {
                    var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
                    var _tagHtml = "#" + p_Tag;
                    $(_tagHtml).append(_Err);
                    return -1;
                }
                return 0;
            }
        case 5:
            {
                if (date1 != date2) {
                    var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
                    var _tagHtml = "#" + p_Tag;
                    $(_tagHtml).append(_Err);
                    return -1;
                }
                return 0;
            }
        case 99: //sangdd thêm mới check nhỏ hơn hoặc = loại luôn 
            {
                if (date1 <= date2) {
                    var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px\">" + p_msgErr + "</div>";
                    var _tagHtml = "#" + p_Tag;
                    $(_tagHtml).append(_Err);
                    return -1;
                }
                return 0;
            }
        default:
            break;

    }
};


// trả ra ngày hiện tại với format đd/mm/yyy/
function GetCurrToDDMMYYY() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();
    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }
    today = mm + '/' + dd + '/' + yyyy;
    return today;
}

///Set kieu tien te cho 1 thang textbox nào đấy
function SetMoneyFormat(_controll_id) {
    var txtControlId = _controll_id;
    var nStr = $("#" + txtControlId).val();
    var _So_am = false;
    if (nStr.indexOf('-') != -1) {
        _So_am = true;
    }
    nStr = nStr.replace('-', '');// cat so am di
    if (nStr.indexOf('.') != -1)//neu nhap vao dang 1.000.000.000 thi doi thanh 1,000,000,000
    {
        // thay tat ca dau '.' bang dau ','         
        while (nStr.indexOf('.') > -1) {
            nStr = nStr.replace('.', '');
        }
    }
    var _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9,]+$');
    //var _RegexChar = new RegExp('[|,}{+&-=!?;/#\"$%^*()<>`~[]\\]+$');     
    for (var i = 0; i < _Vondieule.length - 1; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Vondieule[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
        else {
            $("input[id=" + txtControlId + "]").val(_tempvondieule);
            return;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    nStr = _Vondieule;
    var ctrl = document.getElementById(txtControlId);

    var before_length = ctrl.value.length; // lấy chiều dài trước khi thay đổi
    var key_index = doGetCaretPosition(ctrl); // lấy vị trí con trỏ hiện tại

    nStr = nStr.replace(/[,]/g, '');
    //nStr += '';
    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var result = x1 + x2;
    if (_So_am == true) {
        result = "-" + result.toString();
    }
    $("input[id=" + txtControlId + "]").val(result);

}

function ReSetFloatNumberFormat(_controll_id) {
    var txtControlId = _controll_id;
    var nStr = $("#" + txtControlId).val();
    var _IndexFloat = nStr.indexOf('.');
    var _PhanThapPhan = "";
    var _count_ = 0;
    var _Alltext = "";
    _count_ = (nStr.split(".").length - 1);
    if (_count_ > 1)// nếu có tồn tại 2 dấu . thì cắt dấu toàn bộ từ dấu . thứ 2 đến hết
    {
        var Fst = nStr.indexOf('.');
        var Snd = nStr.indexOf('.', Fst + 1);// vị trí index của thằng . thứ 2
        nStr = nStr.substring(0, Snd);// xóa hết từ thằng . thứ 2
    }
    if (_IndexFloat >= 0)// nếu có dấu . thì mới làm tiếp
    {
        _PhanThapPhan = nStr.substring(_IndexFloat, nStr.length);
        // cắt lấy phần nguyên để format trước
        nStr = nStr.substring(0, _IndexFloat);
    }
    var _Vondieule = "";
    _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9,.]+$');
    _Alltext = _Vondieule + _PhanThapPhan;
    for (var i = 0; i < _Alltext.length ; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Alltext[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
        else {
            $("input[id=" + txtControlId + "]").val(_tempvondieule);
            return;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    nStr = _Vondieule;

    // cắt toàn bộ số 0 trước trường số
    nStr = nStr.replace(/[,]/g, '');
    while (nStr.indexOf(0) == '0' && nStr.length > 1) {
        nStr = nStr.substring(1);
    }
    var ctrl = document.getElementById(txtControlId);

    var before_length = ctrl.value.length; // lấy chiều dài trước khi thay đổi
    var key_index = doGetCaretPosition(ctrl); // lấy vị trí con trỏ hiện tại

    // 
    //nStr += '';
    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var result = x1 + x2;
    $("input[id=" + txtControlId + "]").val(result + _PhanThapPhan);

}


// auto fomat kiểu số không có dấu , 
function SetNumberFormat(nStr, txtControlId) {
    var _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9]+$');
    //var _RegexChar = new RegExp('[|,}{+&-=!?;/#\"$%^*()<>`~[]\\]+$');     
    for (var i = 0; i < _Vondieule.length; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Vondieule[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
        else {
            $("input[id=" + txtControlId + "]").val(_tempvondieule);
            return;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
}

function SetFloatNumberFormat(nStr, txtControlId) {
    var _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9.]+$');
    //var _RegexChar = new RegExp('[|,}{+&-=!?;/#\"$%^*()<>`~[]\\]+$');     
    for (var i = 0; i < _Vondieule.length; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Vondieule[i];
        if (_Regex.test(_newtemp)) {
            if (_newtemp.indexOf('.') != _newtemp.lastIndexOf('.')) {
                $("input[id=" + txtControlId + "]").val(_tempvondieule);
                return;
            }
            else {
                _tempvondieule = _newtemp;
            }
        }
        else {
            $("input[id=" + txtControlId + "]").val(_tempvondieule);
            return;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }



}


// lay so thang giua 2 ngay
//< 15 ngay thi ko tinh
//> 16 ngay thi tinh thanh 0.5 thang
function GetMonth(_date1, _date2) {
    // var _date1 = $("#" + _formdate).val();
    //  var _date2 = $("#" + _todate).val();
    var matches1 = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(_date1);
    var m1 = matches1[2];
    var d1 = matches1[1];
    var y1 = matches1[3];
    var matches2 = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(_date2);
    var m2 = matches2[2];
    var d2 = matches2[1];
    var y2 = matches2[3];
    //var composedDate = new Date(y, m, d);

    var _dateTime1 = new Date(m1 + '/' + d1 + '/' + y1);
    var _dateTime2 = new Date(m2 + '/' + d2 + '/' + y2);
    _dateTime1.setDate(_dateTime1.getDate() - 1);// lùi lại 1 ngày
    var months;
    months = _dateTime2.getFullYear() - _dateTime1.getFullYear();
    months = months * 12;
    months -= _dateTime1.getMonth() + 1;
    months += _dateTime2.getMonth();
    //alert(_dateTime2.getDate().toString() + _dateTime1.getDate().toString())

    var _day1 = _dateTime1.getDate();
    var _day2 = _dateTime2.getDate()
    if (_day2 + 1 >= _day1)
        months++;
    if (m1 == 1 || m1 == 3 || m1 == 5 || m1 == 7 || m1 == 8 || m1 == 10 || m1 == 12) {
        if (d2 == 30) {
            if (_day2 - _day1 >= 14)// thang có 31 ngay
            {
                months += 0.5;
            }

        }
        else {
            if (_day2 - _day1 >= 15)// thang có 31 ngay
            {
                months += 0.5;
            }
        }
    }
    else if (m1 == 2) {
        var isleap = (y1 % 4 == 0 && (y1 % 100 != 0 || y1 % 400 == 0));
        if (isleap)// năm nhuận
        {
            if (_day2 == 30) // thang cua date2 co 30 ngay
            {
                if (_day2 - _day1 >= 16)// 
                {
                    months += 0.5;
                }
            }
            else {
                if (_day2 - _day1 >= 17)//thang cua date2 co 31 ngay
                {
                    months += 0.5;
                }
            }

        }
        else// năm ko nhuận
        {
            if (_day2 == 30) {
                if (_day2 - _day1 >= 17)// thang 2 co 28 ngay
                {
                    months += 0.5;
                }
            }
            else {
                if (_day2 - _day1 >= 18)// thang 2 co 28 ngay
                {
                    months += 0.5;
                }
            }
        }
    }
    else// thang co 30 ngay
    {
        if (d2 == 30) {
            if (_day2 - _day1 >= 15)// thang 2 co 30 ngay
            {
                months += 0.5;
            }
        }
        else {
            if (_day2 - _day1 >= 16)// thang 2 co 30 ngay
            {
                months += 0.5;
            }
        }
    }
    //alert(months);
    return months;

}




//-	Tính phí kỳ thanh toán đầu tiên:
//-	Ngày TT đầu tiên từ ngày 1 đến ngày 10 hàng tháng: tính phí tròn 1 tháng cho tháng đầu tiên
//-	Ngày TT đầu tiên từ ngày 11 đến ngày 20 hàng tháng: tính phí tròn ½ tháng cho tháng đầu tiên
//-	Ngày TT đầu tiên từ ngày 21 đến cuối tháng: không tính phí
//_moc1:=  1;
//_moc2:=  10;
//_moc3:=  11;
//_moc4:=  20;
//_moc5:=  21;

function GetMonthOfPeoridic(_date1, _date2, _moc1, _moc2, _moc3, _moc4, _moc5) {
    // var _date1 = $("#" + _formdate).val();
    //  var _date2 = $("#" + _todate).val();
    var _moc6 = 30 + _moc3 / 1; // đây là mốc 1.5 tháng >= 41 ngày thì sẽ tính thành 1.5 tháng
    var _moc7 = 30 + _moc5 / 1; // đây là mốc 2 tháng >= 51 ngày thì sẽ tính thành 2 tháng
    var matches1 = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(_date1);
    var m1 = matches1[2];
    var d1 = matches1[1];
    var y1 = matches1[3];
    var matches2 = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(_date2);
    var m2 = matches2[2];
    var d2 = matches2[1];
    var y2 = matches2[3];
    var months = 0;
    var _totalmonths = 0;
    var _totalDay = 0;
    if (y2 / 1 == y1 / 1 && m2 / 1 == m1 / 1)// trường hợp 2 ngày đầu cuối cùng 1 tháng thì chỉ _totalDay bằng thằng cuối trừ thằng đầu
    {
        _totalDay = d2 / 1 - d1 / 1;
    }
    else {
        if (m1 / 1 == 1 || m1 / 1 == 3 || m1 / 1 == 5 || m1 / 1 == 7 || m1 / 1 == 8 || m1 / 1 == 10 || m1 / 1 == 12)// tháng 31 ngày
        {
            _totalDay = d2 / 1 + (31 - d1 / 1);
        }
        else if (m1 / 1 != 2)// tháng 30 ngày
        {
            _totalDay = d2 / 1 + (30 - d1 / 1);
        }
        else// tháng 2
        {
            var isleap = (y1 % 4 == 0 && (y1 % 100 != 0 || y1 % 400 == 0));
            if (isleap)// năm nhuận
            {
                _totalDay = d2 / 1 + (29 - d1 / 1);
            }
            else // năm không nhuận
            {
                _totalDay = d2 / 1 + (28 - d1 / 1);
            }
        }
    }
    // vì ngày đầu tiên của kỳ thanh toán cũng tính là 1 ngày nên tổng số ngày phải + lên 1 ngày
    _totalDay++;
    if (_totalDay >= _moc3 && _totalDay <= _moc4)// tính thành nửa tháng
    {
        _totalmonths += 0.5;
    }
    else if (_totalDay >= _moc5 && _totalDay < _moc6)// tính thành 1 tháng
    {
        _totalmonths += 1;
    }
    else if (_totalDay >= _moc6 && _totalDay < _moc7)// tính thành 1.5 tháng
    {
        _totalmonths += 1.5;
    }
    else if (_totalDay >= _moc7)// tính thành 2 tháng
    {
        _totalmonths += 2;
    }
    // set lại 2 ngày về ngày đầu tiên của tháng
    d1 = "01";
    d2 = "01";
    // lùi thằng ngày cuối cùng lại 1 tháng vì bên trên đã cộng số ngày lẻ vào rồi
    if (m2 / 1 == 1) {
        m2 = "12";
        y2 = y2 / 1 - 1;
    }
    else {
        m2 = m2 / 1 - 1;
    }
    var _dateTime1 = new Date(m1 + '/' + d1 + '/' + y1);
    var _dateTime2 = new Date(m2 + '/' + d2 + '/' + y2);
    months = _dateTime2.getFullYear() - _dateTime1.getFullYear();
    months = months * 12;
    if (_dateTime2.getMonth() > _dateTime1.getMonth()) {
        months += _dateTime2.getMonth() - _dateTime1.getMonth();
    }
    if (months / 1 > 0) {
        _totalmonths += months;
    }
    //alert(_totalmonths);
    return _totalmonths;
}


//lay ra nam cua datetiem kieu dd/MM/yyyy
function GetYearofddMMyyyDate(_date1) {
    // var _date1 = $("#" + _formdate).val();
    //  var _date2 = $("#" + _todate).val();
    var matches1 = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(_date1);
    var m1 = matches1[2];
    var d1 = matches1[1];
    var y1 = matches1[3];
    //var composedDate = new Date(y, m, d);

    var _dateTime1 = new Date(m1 + '/' + d1 + '/' + y1);
    var _year = _dateTime1.getFullYear();
    return _year;

}

//Kiểm NH Ngày 02-04-2015
//Thêm mấy cái hàm check date kiểu dd/MM/yyyy và chekc thang MM/yyyy
function isDate_ddMMyyyy(strDate) {
    var currVal = strDate;
    if (currVal == '')
        return false;
    var rxDatePattern = /^(\d{2})(\/|-)(\d{2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    if (dtArray == null)
        return false;
    //Checks for dd/mm/yyyy format.
    dtDay = dtArray[1];
    dtMonth = dtArray[3];
    dtYear = dtArray[5];
    if (dtMonth < 1 || dtMonth > 12)
        return false;
    else if (dtDay < 1 || dtDay > 31)
        return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31)
        return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    return true;
}

// CHekc xem có chọn đúng MM/yyyy
function isMonth_MMyyyy(strDate) {
    var currVal = strDate;
    if (currVal == '')
        return false;
    var rxDatePattern = /^(\d{2})(\/|-)(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    if (dtArray == null)
        return false;
    //Checks for MM/yyyy format.
    dtMonth = dtArray[1];
    dtYear = dtArray[3];
    if (dtMonth < 1 || dtMonth > 12)
        return false;
    return true;
}

// CHekc xem có chọn đúng yyyy
function isYear_yyyy(strYear) {
    var currVal = strYear;
    if (currVal == '')
        return false;
    var rxDatePattern = /^(\d{4})$/;
    var dtArray = currVal.match(rxDatePattern); // is format OK?
    if (dtArray == null)
        return false;
    return true;
}

// Check có là số hay không
function isNumber(strNumber) {
    try {
        if (isNaN(parseFloat(strNumber)) || !isFinite(strNumber)) {
            return false;
        }
        return true;
    }
    catch (e) {
        return false;
    }
}

// Check Int number
function isIntNumber(n) {
    return n != "" && !isNaN(n) && Math.round(n) == n;
}
// Check Float number
function isFloatNumber(n) {
    return n != "" && !isNaN(n) && Math.round(n) != n;
}


// check 2 ngày đa thoa man là ngày MM/yyyy
function CheckCompare2Date_MMyyyy(strDate1, strDate2) {
    try {
        var rxDatePattern = /^(\d{2})(\/|-)(\d{4})$/;

        var dtArray_1 = strDate1.match(rxDatePattern); // is format OK?
        var dtArray_2 = strDate2.match(rxDatePattern); // is format OK?

        var _date_1 = new Date(dtArray_1[3], dtArray_1[1], 1);
        var _date_2 = new Date(dtArray_2[3], dtArray_2[1], 1);
        if (_date_1 > _date_2) {
            return false;
        }
        return true;
    }
    catch (e) {
        return false;
    }
}

// check 2 ngày đa thoa man là ngày dd/MM/yyyy
// trả về false nếu ngày Date1 > Date2
function CheckCompare2Date_ddMMyyyy(strDate1, strDate2) {
    try {
        var rxDatePattern = /^(\d{2})(\/|-)(\d{2})(\/|-)(\d{4})$/;

        // Check xem có phải string date không đã
        var dtArray_1 = strDate1.match(rxDatePattern); // is format OK?
        var dtArray_2 = strDate2.match(rxDatePattern); // is format OK?

        if (dtArray_1 == null || dtArray_2 == null)
            return false
        // Convert sang date để so sanhs
        var _date_1 = new Date(dtArray_1[5], dtArray_1[3] - 1, dtArray_1[1]);
        var _date_2 = new Date(dtArray_2[5], dtArray_2[3] - 1, dtArray_2[1]);
        if (_date_1 > _date_2) {
            return false;
        }

        return true;
    }
    catch (e) {
        return false;
    }
}


// Tạo Combobox select Year +- 20
function FillYear2Combobox(cboID, range, crrYear) {
    var c_option = '';
    var nowYear = crrYear;
    for (var i = (nowYear - range) ; i <= (nowYear + range) ; i++) {
        if (i == nowYear) {
            c_option += '<option value="' + i + '" selected="selected">' + i + '</option>';
        }
        else {
            c_option += '<option value="' + i + '">' + i + '</option>';
        }
    }
    $('#' + cboID).append(c_option);
}

// End Kiểm NH ngày 02-04-2015

//kiểm tra 1 mảng có chứa ký truyền vào hay ko
function containsObject(list, obj) {
    var i;
    for (i = 0; i < list.length; i++) {
        if (list[i] === obj) {
            return true;
        }
    }

    return false;
}


function FomartNumberCurency(nStr, txtControlId) {
    var _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9]+$');
    //var _RegexChar = new RegExp('[|,}{+&-=!?;/#\"$%^*()<>`~[]\\]+$');     
    for (var i = 0; i < _Vondieule.length; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Vondieule[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    $("input[id=" + txtControlId + "]").val(_tempvondieule);
    return true;
}


//LAY SESSION ID CUA MOI PAGE MOI ID LA DUY NHAT 
function getSessionID() {
    try {
        var keysession = $("#txtSessionOnTab").val();
        return keysession;
    } catch (e) {
        console.log(e.message);
    }
}


//CREATE BY:SANGDD
//CREATE DATE: 16-04-2015
//KHI EXPORT DU LIEU THI CHON NGON NGU KHI KET XUAT
//Gán ngon ngu vao text de trong controller lay 
//DUNG CHUNG CHO TAT CA CAC FORM CHON NGON NGU KET XUAT 
function GetChoiseLanguage() {
    try {
        var lang = $("#cboChoiLanguage").val();
        $("#txtLanguage").val(lang);
    } catch (e) {
        console.log(e.message);
    }
}

//moi them de phan biet giua cbo cua phần cctt và Export
function GetChoiseLanguageCPETF() {
    try {
        var lang = $("#cboChoiLanguage__").val();
        $("#txtLanguage").val(lang);
    } catch (e) {
        console.log(e.message);
    }
}

//CREATE BY:SANGDD
//CREATE DATE: 16-04-2015
//Trả về trạng thái ddeffault cùa ngôn ngữ kết xuất
function ResetDefaultChoiseLanguage() {
    $("#txtLanguage").val('1');
}


function jsFormatNumberToString(nStr) {
    if (nStr.indexOf('.') != -1)//neu nhap vao dang 1.000.000.000 thi doi thanh 1,000,000,000
    {
        // thay tat ca dau '.' bang dau ','         
        while (nStr.indexOf('.') > -1) {
            nStr = nStr.replace('.', '');
        }
    }
    var _Vondieule = nStr;
    var _tempvondieule = "";
    var _newtemp = "";
    var _Regex = new RegExp('^[0-9,]+$');
    //var _RegexChar = new RegExp('[|,}{+&-=!?;/#\"$%^*()<>`~[]\\]+$');     
    for (var i = 0; i < _Vondieule.length - 1; i++) {// cat het nhung ky tu khong phai la so
        _newtemp += _Vondieule[i];
        if (_Regex.test(_newtemp)) {
            _tempvondieule = _newtemp;
        }
        else {
            return _tempvondieule;
        }
    }
    if (_Regex.test(_Vondieule))//neu la so thi lay
    {
        _Vondieule = nStr;

    }
    else {
        _Vondieule = _tempvondieule;
    }
    nStr = _Vondieule;

    nStr = nStr.replace(/[,]/g, '');
    //nStr += '';
    x = nStr.split(',');
    x1 = x[0];
    x2 = x.length > 1 ? ',' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + ',' + '$2');
    }
    var result = x1 + x2;

    return result;
}

///
/// CHeck xem cái ngyaf ngay nhap vao rồi thông báo lỗi lên form không alert
///
function ValidateDate_ddMMyyyy_Msg(p_Name, p_msgErr, p_Tag) {
    $("#divErr").remove();
    if (isDate_ddMMyyyy(p_Name) == false) {
        var _Err = " <div id=\"divErr\" style=\"color:red ;padding-top:15px;clear:both; padding-bottom:10px ;\">" + p_msgErr + "</div>";
        var _tagHtml = "#" + p_Tag;
        $(_tagHtml).append(_Err);
        return -1;
    } else {
        return 0;
    }
}


///
// CHekc xem cái mật khẩu nhập vào có đủ phức tạp không
///
function checkRegexPassword(password, p_msgErr, p_Tag) {
    try {
        //regular to check password
        var regularExpression = /^(?=.*[0-9])(?=.*[!@#_$%^&*])(?=.*[A-Z])[+a-zA-Z0-9!@#_$%^& *]{6,50}|(?=.*[0-9])(?=.*[!@#_$%^&*])(?=.*[a-z])[+a-zA-Z0-9!@#_$%^& *]{6,50}|(?=.*[a-z])(?=.*[!@#_$%^&*])(?=.*[A-Z])[+a-zA-Z0-9!@#_$%^& *]{6,50}|(?=.*[0-9])(?=.*[A-Z])(?=.*[a-z])[+a-zA-Z0-9!@#_$%^& *]{6,50}$/;
        var isRegexPassword = (regularExpression.test(password)) ? 0 : -1;
        //remove div error
        $("#divErr").remove();
        if (isRegexPassword) {
            var _Err = " <div id=\"divErr\" style=\"color:red ;padding:0 10px;\">" + p_msgErr + "</div>";
            var _tagHtml = "#" + p_Tag;
            $(_tagHtml).append(_Err);
        }
        return isRegexPassword;

    } catch (e) {
        console.log(e.message);
        return false;
    }
};

// chekc chi nhap chu không dau và số + "_"
function CheckUnUnicodeAndSpecChar(nStr) {
    var rxDatePattern = /^[\w]+$/;
    if (nStr.indexOf(" ") >= 0)
        return false;
    var rxVal = nStr.match(rxDatePattern); // is format OK?
    if (rxVal == null || rxVal[0] != nStr)
        return false;

    return true;
};

function OnlyNotAllowedSpaceChar(event) {
    var charCode = (event.which) ? event.which : event.keyCode;
    if (charCode == 32) {
        return false;
    }
    return true;
};

// đánh số thứ tự cái table
function FuncIndexRowTable(table_id) {
    $(table_id).find('tbody').find("tr").each(function () {
        var _index = $(this);
        $(this).find("td:first").text(_index[0].rowIndex);
    });
}

// Sort lại cái bảng data
function FuncSortTable(table_id, _class, _is_number) {
    var asc = "";
    var _th_text = $("." + _class).first().text();
    _th_text = _th_text.replace(/▼/g, '');
    _th_text = _th_text.replace(/▲/g, '');
    if (_class.toString().indexOf("-desc") > 0) {
        asc = "-desc";
        _th_text += " ▼"
    }
    else {
        asc = "-asc";
        _th_text += " ▲"
    }
    //bỏ hết dấu sort ở các th khác của table
    $(table_id).find('th').each(function (i, item) {
        $(this).text($(this).text().replace(/▼/g, ''));
        $(this).text($(this).text().replace(/▲/g, ''));
    });
    // gán dấu sort cho thằng th vừa click
    $("." + _class).first().text(_th_text);
    var tbody = $(table_id).find('tbody');
    tbody.find('tr').sort(function (a, b) {
        if (_is_number == 1)// text
        {
            if (asc == "-asc") {
                return $('.' + _class, a).text().localeCompare($('.' + _class, b).text());
            }
            else {
                return $('.' + _class, b).text().localeCompare($('.' + _class, a).text());
            }
        }
        else if (_is_number == 2)// number
        {
            if (asc == "-asc") {
                return $('.' + _class, a).text().replace(/,/g, '') - $('.' + _class, b).text().replace(/,/g, '');
            }
            else {
                return $('.' + _class, b).text().replace(/,/g, '') - $('.' + _class, a).text().replace(/,/g, '');
            }
        }
        else// datetime
        {
            var _currtext_a = $('.' + _class, a).text();
            var _currtext_b = $('.' + _class, b).text();
            if (_currtext_a.trim() == "N/A") {
                _currtext_a = "01/01/0001";
            }
            if (_currtext_b.trim() == "N/A") {
                _currtext_b = "01/01/0001";
            }
            if (_currtext_a.trim() == "Vô hạn") {
                _currtext_a = "01/01/9999";
            }
            if (_currtext_b.trim().toUpperCase() == "VÔ HẠN") {
                _currtext_b = "01/01/9999";
            }
            if (asc == "-asc") {
                return ConvertddMMyyyToMMddyyy(_currtext_a) - ConvertddMMyyyToMMddyyy(_currtext_b);
            }
            else {
                return ConvertddMMyyyToMMddyyy(_currtext_b) - ConvertddMMyyyToMMddyyy(_currtext_a);
            }
        }
    }).appendTo(tbody);
    // gán lại số thứ tự
    FuncIndexRowTable(table_id);
    // gán lại class sort    
    var _oldclass = _class;
    var _newclass = _class.substring(0, _class.lastIndexOf('-'));
    if (asc == "-asc") {
        _newclass += '-desc'
    }
    else {
        _newclass += '-asc'
    }
    $('.' + _oldclass).each(function (i, item) {
        $(item).removeClass(_oldclass);
        $(item).addClass(_newclass);
    });
}

///convert date dd/mm/yyyy to mm/dd/yyyy
function ConvertddMMyyyToMMddyyy(_date1) {
    _date1 = _date1.trim();
    var matches1 = /^(\d{2})[-\/](\d{2})[-\/](\d{4})$/.exec(_date1);
    var m1 = matches1[2];
    var d1 = matches1[1];
    var y1 = matches1[3];
    var _dateTime1 = new Date(m1 + '/' + d1 + '/' + y1);
    return _dateTime1;
}


//pIdDataTable :ID cua table 
//sử dụng khi scroll ngang table khi cuộn chuột
//var dd = null;
//var delta = 0;
//var ddc = true;

function MouseScrollOnTable(pIdDataTable) {
    var dd = document.getElementById(pIdDataTable).parentElement;
    var delta = 0;
    var ddc = true;

    //FF doesn't recognize mousewheel as of FF 3.x
    var mousewheelevt = (/Firefox/i.test(navigator.userAgent)) ? "DOMMouseScroll" : "mousewheel";
    $(dd).on(mousewheelevt, function (event) {
        ddc = true;
        event.preventDefault();
        if (dd.addEventListener) {
            dd.addEventListener(mousewheelevt, MouseWheelHandler, false);
        }
        else dd.attachEvent("on" + mousewheelevt, MouseWheelHandler);
    });

    function MouseWheelHandler(e) {
        while (ddc == true) {
            var e = window.event || e;
            // FF doesn't recognize event.wheelDelta = -120. Only event.detail = 3
            var _wheelDelta = e.detail ? e.detail * 40 : -e.wheelDelta;

            delta = dd.scrollLeft + (_wheelDelta / 2);


            var this_height = document.getElementById(pIdDataTable).offsetHeight;
            if (this_height > dd.offsetHeight)
                delta = delta < 0 ? 0 : delta > dd.scrollWidth + 17 - dd.offsetWidth ? dd.scrollWidth + 17 - dd.offsetWidth : delta;
            else
                delta = delta < 0 ? 0 : delta > dd.scrollWidth - dd.offsetWidth ? dd.scrollWidth - dd.offsetWidth : delta;

            $(dd).scrollLeft(delta);
            ddc = false;
        }
    }
}

// Ngocdt lay ra phan mo rong cua file upload
function getExtension(filename) {
    var parts = filename.split('.');
    return parts[parts.length - 1];
}
// Ngocdt check dinh dang file anh
function isImage(filename) {
    var ext = getExtension(filename);
    switch (ext.toLowerCase()) {
        case 'jpg':
        case 'gif':
        case 'bmp':
        case 'png':
        case 'jpeg':
        case 'jpx':
        case 'ief':
            //etc
            return true;
    }
    return false;
}
// Ngocdt check dinh dang file video
function isVideo(filename) {
    var ext = getExtension(filename);
    switch (ext.toLowerCase()) {
        case 'ogv':
        case 'vob':
        case 'mp4':
        case 'flv':
        case 'ogg':
        case 'quicktime':
        case 'avi':
        case 'mov':
        case 'wmv':
        case 'asf':
        case 'm4v':
        case '3gp':
        case 'qt':
        case 'webm':
        case 'mkv':
            //etc
            return true;
    }
    return false;
}


//hàm lấy ra danh sách các cột hiển thị và ẩn quy tắc columns|0(1) 0 ẩn 1 hiển thị
function saveShowColList(tagTablesId) {
    try {
        var colShow = "";
        var table = document.getElementById(tagTablesId);
        var cols = table.getElementsByTagName("th");
        for (var i = 0; i < cols.length; i++) {
            var cls = cols[i].getAttribute("class");
            var cssat = $(cols[i]).css("display");
            if (cls != null) {
                if (cssat == "none") {
                    cssat = "0";
                } else {
                    cssat = "1";
                }
                colShow += cls + "|" + cssat + ",";
            }
        }
        //đẩy vào controller khi export dữ liệu ra 
        return colShow;
    } catch (e) {
        alert(e.message);
        return "";
    }
}


// Kiểm NH: Ngày 11/08/2015
// Check press key number and "delimiter" key. ex: "/", "-",..
// Phục vụ cho nhập ngày tháng
function CheckPressCharDateFormat(evt, delimiter) {

    delimiter = delimiter || "/";

    var delimiterCode = delimiter.charCodeAt(0);
    var charCode = (evt.which) ? evt.which : evt.keyCode;
    if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != delimiterCode)
        return false;
    return true;
}



/// Kiểm NH: 2015-08-14
// Chỉnh lại cái header bảng chỉ có fixxed table bằng css không có scroll ngang
function EditTableFixedHeaderCSS(idTable) {
    var table_el = document.getElementById(idTable);

    var body_height = $(table_el).find("tbody").height();
    var scroll_body_height = $(table_el).find("tbody").prop("scrollHeight");

    if (scroll_body_height > body_height) {
        $(table_el).find("thead tr").css("padding-right", "17px");
    }
}

function CreateRollingWaitingIcon(is_display) {
    var html = "<div style=\"height: 100%; width: 100%; top: 0; left: 0; z-index: 2001; position: absolute;\"><div class=\"loader\"><div></div><div></div><div></div><div></div><div></div></div></div>";
    if (is_display) {
        $("body").append(html);
    }
    else {
        $("body").find(".loader").parent().remove();
    }
}

// Kiểm NH: 07-10-2015
// Disable các ngày không phải là thứ 2
function DisableParticularDay_NotMonday(date) {
    try {
        var day = date.getDay();
        if (day != 1) {
            return [false];
        }
        else {
            return [true];
        }
    }
    catch (e) {
    }
}


//Create NgocDT 
//Date : 27.10.2015
//PP : Kiem tra session cua Obiee het thi dang nhap lai 
function checkObieeSession(pLoginStatus) {
    var iframe = document.getElementById('ObieeFrame');
    if ($("ObieeFrame").length != -1) {
        try {
            var hasLoginFrame = 0;
            try {

                hasLoginFrame = iframe.contentWindow.document.getElementsByName('logonForm').length;
                if (hasLoginFrame === undefined) hasLoginFrame = 0;

            } catch (e) {
                pLoginStatus = 1;
                console.log('vao catch ----> ');
                window.location = "/ModuleReportBI/ReportBI_Logon/ReportBI_Logon";
            }
            //var havelogin = iframe.contentWindow.document.getElementsByName('passwd').length;
            if (pLoginStatus == 0 || hasLoginFrame > 0) {
                console.log('dang nhap vao obiee' + hasLoginFrame);
                pLoginStatus = 1;
                window.location = "/ModuleReportBI/ReportBI_Logon/ReportBI_Logon";
            }
        } catch (err) {
            pLoginStatus = 1;
            console.log('vao catch ----> ');
            window.location = "/ModuleReportBI/ReportBI_Logon/ReportBI_Logon";
        }
    }
}

//HUNGTD: hàm kiểm tra dl nhập vào có phải dạng giờ:phút (HH:mm) không?
// trả ra 1 là ok
// -1 là ko đúng định dạng
function CheckValidateTimehhmm(_val) {
    try {
        var _arr = _val.split(':');
        var _validate = 1;
        if (_arr.length != 2) {
            _validate = -1;
        }
        if (isNaN(_arr[0]) == true || isNaN(_arr[01]) == true) {
            _validate = -1;
        }
        if (_arr[0] / 1 > 23 || _arr[1] > 59 || _arr[0] / 1 < 0 || _arr[1] / 1 < 0) {
            _validate = -1;
        }

        return _validate;
    } catch (e) {
        return -1;
    }
}

//hungtd: so sanh 2 gio: hh:mm
//_type = 1: so sanh >=
//type = 2: sosanh >
// thang val1 so sanh >= hoac > val2
// neu dung thi tra ra true
// sai thi tra ra false
function CompareTimehhmm(_val1, _val2, _type) {
    try {
        var _arr1 = _val1.split(':');
        var _arr2 = _val2.split(':');
        var _hh1 = _arr1[0];
        var _mm1 = _arr1[1];
        var _hh2 = _arr2[0];
        var _mm2 = _arr2[1];
        if (_type = 1)
        {
            // so sanh > =
            if (_hh1 / 1 < _hh2 / 1) {
                return false;
            }
            else if (_hh1/1 == _hh2/1) {
                if (_mm1/1 < _mm2/1) {
                    return false;
                }
                else { return true; }
            }
            else return true;
        }
        else  
        {
            // so sanh > 
            if (_hh1 / 1 < _hh2 / 1) {
                return false;
            }
            else if (_hh1 / 1 == _hh2 / 1) {
                if (_mm1 / 1 <= _mm2 / 1) {
                    return false;
                }
                else { return true; }
            }
            else return true;
        }
       
    } catch (e) {
        return false;
    }
}

//HUNGTD: check định dạng Địa chỉ IP
//đúng trả ra 1; sai trả ra -1
function ValidateIPaddress(ipaddress) {
    if (/^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/.test(ipaddress)) {
        return (1)
    }
    return (-1)
}


function jsCheckContainsUnicode(str_input) {
    var VietNamKey = "á,à,ạ,ả,ã,â,ấ,ầ,ậ,ẩ,ẫ,ă,ắ,ằ,ặ,ẳ,ẵ,é,è,ẹ,ẻ,ẽ,ê,ế,ề,ệ,ể,ễ,ó,ò,ọ,ỏ,õ,ô,ố,ồ,ộ,ổ,ỗ,ơ,ớ,ờ,ợ,ở,ỡ,ú,ù,ụ,ủ,ũ,ư,ứ,ừ,ự,ử,ữ,í,ì,ị,ỉ,ĩ,đ,ý,ỳ,ỵ,ỷ,ỹ,Á,À,Ạ,Ả,Ã,Â,Ấ,Ầ,Ậ,Ẩ,Ẫ,Ă,Ắ,Ằ,Ặ,Ẳ,Ẵ,É,È,Ẹ,Ẻ,Ẽ,Ê,Ế,Ề,Ệ,Ể,Ễ,Ó,Ò,Ọ,Ỏ,Õ,Ô,Ố,Ồ,Ộ,Ổ,Ỗ,Ơ,Ớ,Ờ,Ợ,Ở,Ỡ,Ú,Ù,Ụ,Ủ,Ũ,Ư,Ứ,Ừ,Ự,Ử,Ữ,Í,Ì,Ị,Ỉ,Ĩ,Đ,Ý,Ỳ,Ỵ,Ỷ,Ỹ";
    for (var i = 0; i < str_input.length; i++) {
        if (VietNamKey.indexOf(str_input[i]) != -1) {
            return false;
        }
    }
    return true;
}


function validateExtension4FileXLS(v) {
    var allowedExtensions = new Array("xls", "XLS", "xlsx", "XLSX");
    for (var ct = 0; ct < allowedExtensions.length; ct++) {
        sample = v.lastIndexOf(allowedExtensions[ct]);
        if (sample != -1) {
            return 1;
        }
    }
    return 0;
}


function CheckTimeHHMM(_val) {
    try {
        var _arr = _val.split(':');
        var _validate = 0;
        if (_arr.length != 2) {
            _validate = -1;
        }
        if (isNaN(_arr[0]) == true || isNaN(_arr[01]) == true) {
            _validate = -1;
        }
        if (_arr[0] / 1 > 23 || _arr[1] > 59 || _arr[0] / 1 < 0 || _arr[1] / 1 < 0) {
            _validate = -1;
        }

        return _validate;
    } catch (e) {
        return -1;
    }
}

//Check FromTime nhap vao phai nho hon ToTime
function CompareFromTimeToTime(pFromTime, pToTime) {
    try {
        var intFromTime = 0;
        var intToTime = 0;
        var arrFromTime = pFromTime.split(':');
        if (arrFromTime.length != 2) {
            return -1;
        }
        intFromTime = arrFromTime[0] + arrFromTime[1];
        var arrToTime = pToTime.split(':');
        if (arrFromTime.length != 2) {
            return -1;
        }
        intToTime = arrFromTime[0] + arrFromTime[1];

        if (parseInt(intFromTime) > parseInt(intToTime)) {
            return -1;
        }
        return 0;

    } catch (e) {
        return -1;
    }
}

//HUNGTD : check định dạng file ảnh
// ko phải ảnh thìtrả ra -1
function validateExtension4FileImg(v) {
    var allowedExtensions = new Array("JPG", "jpg", "PNG", "png", "JPEG", "jpeg", "bmp", "BMP", "GIF", "gif");
    for (var ct = 0; ct < allowedExtensions.length; ct++) {
        sample = v.lastIndexOf(allowedExtensions[ct]);
        if (sample != -1) {
            return 1;
        }
    }
    return -1;
}

// dangtq check file excel
function validateExtension4File_Excel(v) {
    var allowedExtensions = ["xls", "xlsx"];
    var fileExtension = v.substr(v.lastIndexOf('.') + 1).toLowerCase();
    if (allowedExtensions.indexOf(fileExtension) > -1) {
        return true;
    }
    return false;
}

//Trạng tai bài viết
//SangDD
//Muc dich luu thong tin cac trang thai bai viet: go trong cac ham javascripts
var ArticlesStatus = { ChoDuyet: 1, TraLai: 2, DuyetBai: 3, GoBo: 4, LuuTam: 5 }
var ArticlesLanguage = { vi: 'VI_VN', en: 'EN_US' }
var ArticlesTranslate = { DaDich: 2, ChuaDich: 1 }

function ShowMessButton(_msg) {
    try {
        jAlert("Thông báo", _msg);
    } catch (e) {

    }
}

//DongHT: Định dạng số điện thoại trước khi gửi lên 
function FormatPhone(p_phone) {
    try {
        while (p_phone.indexOf("(+84)") != -1) {
            p_phone = p_phone.replace("(+84)", "0");
        }
        while (p_phone.indexOf("+(84)") != -1) {
            p_phone = p_phone.replace("+(84)", "0");
        }
        while (p_phone.indexOf("-") != -1) {
            p_phone = p_phone.replace("-", "");
        }
        while (p_phone.indexOf(" ") != -1) {
            p_phone = p_phone.replace(" ", "");
        }
        while (p_phone.indexOf(".") != -1) {
            p_phone = p_phone.replace(".", "");
        }
        if (p_phone.indexOf("0") != 0) {
            p_phone = "0" + p_phone;
        }
        return p_phone;
    } catch (e) {
        alert(e);
    }
}