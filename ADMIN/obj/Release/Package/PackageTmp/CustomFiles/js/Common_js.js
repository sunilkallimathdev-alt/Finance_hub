
//Only Integers
var specialKeys = new Array();
specialKeys.push(8); //Backspace
function IsNumeric(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = (((keyCode >= 48 && keyCode <= 57) || specialKeys.indexOf(keyCode) != -1) && keyCode != 32);
    return ret;
}

//Only Decimal
var specialKeys = new Array();
specialKeys.push(8); //Backspace
function IsNumericDecimal(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = (((keyCode >= 48 && keyCode <= 57) || keyCode == 46 || specialKeys.indexOf(keyCode) != -1) && keyCode != 32);
    return ret;
}

//Only Integers + Alphabets
var specialKeys = new Array();
specialKeys.push(8); //Backspace
function IsNumericorAlphabets(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = (((keyCode >= 48 && keyCode <= 57) || (keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || specialKeys.indexOf(keyCode) != -1) && keyCode != 32);
    return ret;
}

//Only Alphabets
var specialKeys = new Array();
specialKeys.push(8); //Backspace
specialKeys.push(32); //Space
function IsAlphabets(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = ((keyCode >= 65 && keyCode <= 90) || (keyCode >= 97 && keyCode <= 122) || specialKeys.indexOf(keyCode) != -1);
    return ret;
}

//No Key
var specialKeys = new Array();
function Nokey(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = (specialKeys.indexOf(keyCode) != -1);
    return ret;
}

//No HTML tags
function NotanHTML(e) {
    var keyCode = e.which ? e.which : e.keyCode
    var ret = (keyCode != 60 && keyCode != 62);
    return ret;
}

function readFile(file, onLoadCallback) {
    var reader = new FileReader();
    reader.readAsDataURL(file)
    reader.onload = onLoadCallback;
}

$.validator.addMethod('filesize', function (value, element, param) {
    return this.optional(element) || (element.files[0].size <= param)
}, 'File size must be less than 2MB');

function BlockPreviousDate(FieldID) {
    var dtToday = new Date();
    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();
    var maxDate = year + '-' + month + '-' + day;
    $('[name="' + FieldID + '"]').attr('min', maxDate);
}

function SetTodaysDate(FieldID) {
    var dtToday = new Date();
    var month = dtToday.getMonth() + 1;
    var day = dtToday.getDate();
    var year = dtToday.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();
    var hour = dtToday.getHours();
    var min = dtToday.getMinutes();
    var ss = dtToday.getSeconds();
    if (hour < 10)
        hour = '0' + hour.toString();
    if (min < 10)
        min = '0' + min.toString();
    if (ss < 10)
        ss = '0' + ss.toString();
    //var Today = year + '-' + month + '-' + day + "T" + hour + ":" + min + ":" + ss;
    var Today = year + '-' + month + '-' + day;
    $('[name="' + FieldID + '"]').val(Today);
}

function SetTime(FieldID) {
    var dtToday = new Date();
   
    var hour = dtToday.getHours();
    var min = dtToday.getMinutes();
    var ss = dtToday.getSeconds();
    if (hour < 10)
        hour = '0' + hour.toString();
    if (min < 10)
        min = '0' + min.toString();
    if (ss < 10)
        ss = '0' + ss.toString();
    var Today = hour + ":" + min;
    //var Today = year + '-' + month + '-' + day;
    $('[name="' + FieldID + '"]').val(Today);
}

function DeliveryDate() {
    var dtToday =  new Date();
    dtToday =   $('[name="PickUPDate"]').val();
    var n = 3; //number of days to add. 
    var requiredDate = new Date(dtToday.getFullYear(), dtToday.getMonth(), dtToday.getDate() + n);
    var month = requiredDate.getMonth() + 1;
    var day = requiredDate.getDate();
    var year = requiredDate.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();
    var hour = requiredDate.getHours();
    var min = requiredDate.getMinutes();
    var ss = requiredDate.getSeconds();
    if (hour < 10)
        hour = '0' + hour.toString();
    if (min < 10)
        min = '0' + min.toString();
    if (ss < 10)
        ss = '0' + ss.toString();
    var Today = year + '-' + month + '-' + day;
    $('[name="DeliveryDate"]').val(Today);
}

function SetDeliveryDate(FieldID) {
    var dtToday = new Date();
    var n = 4; //number of days to add. 
    var requiredDate = new Date(dtToday.getFullYear(), dtToday.getMonth(), dtToday.getDate() + n);
    var month = requiredDate.getMonth() + 1;
    var day = requiredDate.getDate();
    var year = requiredDate.getFullYear();
    if (month < 10)
        month = '0' + month.toString();
    if (day < 10)
        day = '0' + day.toString();
    var hour = requiredDate.getHours();
    var min = requiredDate.getMinutes();
    var ss = requiredDate.getSeconds();
    if (hour < 10)
        hour = '0' + hour.toString();
    if (min < 10)
        min = '0' + min.toString();
    if (ss < 10)
        ss = '0' + ss.toString();
    var Today = year + '-' + month + '-' + day;
    $('[name="' + FieldID + '"]').val(Today);
}


function SetDateTime(DatTime) {

    if (DatTime != null && DatTime != "" && DatTime != "null") {
       // var dtToday = new Date();
        var month = DatTime.getMonth() + 1;
        var day = DatTime.getDate();
        var year = DatTime.getFullYear();
        if (month < 10)
            month = '0' + month.toString();
        if (day < 10)
            day = '0' + day.toString();
        var hour = DatTime.getHours();
        var min = DatTime.getMinutes();
        var ss = DatTime.getSeconds();
        if (hour < 10)
            hour = '0' + hour.toString();
        if (min < 10)
            min = '0' + min.toString();
        if (ss < 10)
            ss = '0' + ss.toString();
        var da = year + '-' + month + '-' + day + "T" + hour + ":" + min + ":" + ss;
        return da;
    }
}

function SplitDateTime(DatTime) {
    if (DatTime != null && DatTime != "" && DatTime != "null") {
        var da = DatTime;
        d = da.split('T')[0];
        var day = d.split('-')[2];
        var month = d.split('-')[1];
        var year = d.split('-')[0];
        d = d = da.split('T')[1];
        da = month + '/' + day + '/' + year;
        return da;
    }
    else
        return "";
}

function SplitDateDB(DatTime) {
    if (DatTime != null && DatTime != "" && DatTime != "null") {
        var da = DatTime;
        d = da.split('T')[0];
        //var day = d.split('-')[2];
        //var month = d.split('-')[1];
        //var year = d.split('-')[0];
        //d = d = da.split('T')[1];
        //da = month + '/' + day + '/' + year;
        return d;
    }
    else
        return "";
}

function SplitDate(DatTime) {
    if (DatTime == "" || DatTime == null) {

        return "";
    }
    else {
        var MyDate_String_Value = DatTime;
        var value = new Date
            (
            parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, ''))
            );
        var dat = value.getDate() +
            "/" + (value.getMonth() +
                1)
            +
            "/" +
            value.getFullYear();
        return dat;
    }
}
function SplitTime(DatTime) {
    if (DatTime != null && DatTime != "" && DatTime != "null") {
        var da = DatTime;
        d = da.split('T')[1];
       
        return d.split(':')[0] + ':' + d.split(':')[1];
    }
    else
        return "";
}

$.validator.addMethod("greaterThan",
    function (value, element, param) {
        var $otherElement = $(param);
        return parseInt(value, 10) > parseInt($otherElement.val(), 10);
    });

function tConvert(time) {
    // Check correct time format and split into components
    if (time == "" || time == null) {
        return "";
    }
    else {
        time = time.toString().match(/^([01]\d|2[0-3])(:)([0-5]\d)(:[0-5]\d)?$/) || [time];

        if (time.length > 1) { // If time format correct
            time = time.slice(1);  // Remove full string match value
            time[5] = +time[0] < 12 ? ' AM' : ' PM'; // Set AM/PM
            time[0] = +time[0] % 12 || 12; // Adjust hours
        }
        return time.join(''); // return adjusted time or original string
    }
}