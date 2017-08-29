/*Function for setting local date on view*/

function getLocalFormatedLastUpdate(value) {
    if (value == "" || value == null) {
        return "";
    }
    else {
        var localdate = changeToLocalDate(value);
        var LocalFormatedLastSystemUpdate = formatDate(localdate, "MM-dd-yyyy hh:mm z");
        return LocalFormatedLastSystemUpdate;
    }
}
/*
Function changeToLocalDate is used to convert datetime value 
to browser's local date. 
*/
function changeToLocalDate(value) {
    var offSet = new Date().getTimezoneOffset() * 60000;

    try {
        var t = new Date(value);

        if (t.toLocaleDateString() == 'Invalid Date')
            return '';

        t.setTime(t.getTime() - offSet);
        return t;

    } catch (e) {
        return '';
    }
}
/*
Function formatDate is used to return given date in provided format.
*/
function formatDate(date, format) {
    if (date == null || date == "") {
        return "";
    }
    else {
        var month = date.getMonth() + 1;
        var count = (format.match(/M/g) || []).length;
        var months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
        getPaddedComp = function (comp) {
            return ((parseInt(comp) < 10) ? ('0' + comp) : comp)
        },
        formattedDate = format,
        o = {
            "y+": date.getFullYear(), // year
            "M+": (count==2)?getPaddedComp(month): months[date.getMonth()], //month
            "d+": getPaddedComp(date.getDate()), //day
            "h+": getPaddedComp((date.getHours() > 12) ? date.getHours() % 12 : date.getHours()), //hour
            "H+": getPaddedComp(date.getHours()), //hour
            "m+": getPaddedComp(date.getMinutes()), //minute
            "s+": getPaddedComp(date.getSeconds()), //second
            "S+": getPaddedComp(date.getMilliseconds()), //millisecond,
            "z+": (date.getHours() >= 12) ? 'PM' : 'AM'
            
        };
        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                formattedDate = formattedDate.replace(RegExp.$1, o[k]);
            }
        }       
        return formattedDate;
       
    }
}





function GetHealthStatusNameByVal(healthstate) {
    var HealthStatusName = '';
    if (healthstate == 0) { HealthStatusName = "No Data"; }
    if (healthstate == 1) { HealthStatusName = "Good"; }
    if (healthstate == 2) { HealthStatusName = "Warning"; }
    if (healthstate == 3) { HealthStatusName = "Error"; }
    if (healthstate == 4) { HealthStatusName = "Planned"; }
    if (healthstate == 5) { HealthStatusName = "UnPlanned"; }
    if (healthstate == 6) { HealthStatusName = "Emergency"; }
    return HealthStatusName;
}

function CurrentFormatedDateTime() {
    var date = new Date();
    return (date.getMonth() + 1) + '_' + date.getDate() + '_' + date.getFullYear() + '_' + date.getHours() + '_' + date.getMinutes() + '_' + date.getSeconds();
}

function LoadObjectNameExcel() {
    var activeLink = $("div").find(".headerLinkActive").text();

    activeLink = activeLink.toLowerCase().replace(/\b[a-z]/g, function (letter) {
        return letter.toUpperCase();
    });
    var str = activeLink.split(' ');
    var role = $("#allToggleSelect :selected").text();
    var selectedTab = $("#homeicons").find(".home_container_selected").text().trim();

    var newStr = "";

    if (str.length > 1) {
        for (var i = 0; i < str.length; i++) {
            newStr += str[i].charAt(0);
        }
    }
    else { newStr = activeLink; }
    if (selectedTab.toLowerCase() == "customers" && role.indexOf("Only") == -1) {
        newStr = "All";
    }
    return newStr + "_";
}
function GetPatchingScheduleStatusNameByVal(schedulestatus) {
    var StatusName = '';
    if (schedulestatus == 1) { StatusName = "Pending"; }
    if (schedulestatus == 2) { StatusName = "Approved"; }
    if (schedulestatus == 3) { StatusName = "Rejected"; }
    if (schedulestatus == 4) { StatusName = "Completed"; }
    if (schedulestatus == 5) { StatusName = "Failed"; }
    if (schedulestatus == 6) { StatusName = "Lapsed"; }
    if (schedulestatus == 6) { StatusName = "Delete"; }
    return StatusName;
}