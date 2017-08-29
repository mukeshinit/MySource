// set utcoffset
$(document).ready(function () {
    // get offset
    var now = new Date();
    var offSet = now.getTimezoneOffset() * -1;
    // if cookies enabled
    if (checkForCookiesEnabled()) {
        // process UTC
        var o = getCookie("UTCOffset");
        if (o == null || o == "") {
            setCookie("UTCOffset", offSet, 30);
            window.location.href = window.location.href;
        }

        else {
            // check to see if different that last one
            if (o.toString() != offSet.toString()) {
                setCookie("UTCOffset", offSet, 30);
                window.location.href = window.location.href;
            }
        }
    }
});

//Add placeholder text style 
$(document).ready(function () {
    var target = $("#placeholderfield");

    if ((target.attr("placeholder") != target.val()) && target.val() != "") {
        target.removeClass("placeholder")
    }
    else {
        target.addClass("placeholder")
    }
    target.on("focus", function () {
        target.removeClass("placeholder");
    });
    target.on("blur", function () {
        var placeholderText = $("#placeholderfield").attr("placeholder");
        if (target.val() == placeholderText || target.val() == "")
            target.addClass("placeholder");
    });
    target.on("keypress", function (e) {
        //remove column if the user enters 
        var recipeientsvalue = target.val();
        $("#placeholderfield").val(recipeientsvalue.replace(",", ""));
    });
});

// Add listeners for elements
$(document).ready(function () {

    // Top bar
    $("li.icon_item").mouseover(function () {
        //$(this).attr('class') = "icon_item iconBarOver";
        $(this).removeClass().toggleClass("iconBarOver");
    }).mouseout(function () {
        $(this).removeClass().removeClass("iconBarOver");
    });

    $("li.icon_item").click(function () {
        var id = $(this).attr("id");
        id = id.replace("headericon_", "");
        $("ul.icon_bar").find("div").removeClass("selected");
        $(this).find("div").toggleClass("selected");
        // show section
        $("#main-content-actual").children().each(function () {
            // hide all except object header
            var subid = $(this).attr("id");
            if (!subid) subid = "";
            if (subid.indexOf("detailheader") == -1) {
                $(this).hide();
            }
        });
        $("#detailsection-" + id).show();
        resizeWindow();
    });



    // Top bar
    $("div.subsection_collapse").click(function () {
        $(this).toggleClass("subsection_expand").toggleClass("subsection_collapse");
        var id = $(this).attr("id");
        $("#" + id + "_content").toggle();
    });

    // home icons
    // home icons
    //$("div.home_container").mouseover(function () {
    //    $(this).css("color", "#3c827a");
    //}).mouseout(function () {
    //    $(this).css("color", "");
    //    $(this).css("text-decoration", "");
    //    $(this).css("font-weight", "");
    //});

    //$("div.report_container").mouseover(function () {
    //    $(this).css("color", "#fff");
    //    $(this).css("text-decoration", "underline");
    //    $(this).css("font-weight", "");
    //}).mouseout(function () {
    //    $(this).css("color", "");
    //    $(this).css("text-decoration", "");
    //    $(this).css("font-weight", "");
    //});


    $(window).resize(function () {
        resizeWindow();
    });

    if ($("#adminMenu").length > 0) {
        $("#settings").show();
    }
    // resizeWindow();
});


function resizeWindow() {

    var windowHeight = $(window).height();
    var documentHeight = $(document).height();
    var headerHeight = $("#header").height();
    //footerHeight = $("footer").height();
    var iconHeaderHeight = $("#iconheader").height();
    var homeIconsHeight = $("#homeicons").height();
    var sectionTitleHeight = $("#sectionTitle").height();
    var gridTabHeight = $(".k-tabstrip").height();
    var gridHeaderHeight = $(".k-grid-header").height();
    var checkBoxHeight = $("div.checkbox").height();
    var gridPagerHeight = $(".k-grid-pager").height();
    var serverTabHeight = 0;
    var p4serviceTabHeight = 0;
    var notificationTabHeight = 0 ;
    var serverTab = $("#serverliststrip").data("kendoTabStrip");
    if (serverTab) {
        serverTabHeight = 80;
    }

    if ($("#activeToggle").length > 0) {
        var activeToggleHeight = 38;
        headerHeight = (activeToggleHeight > headerHeight ? activeToggleHeight : headerHeight);
    }

    var p4serviceTab = $("#p4serviceliststrip").data("kendoTabStrip");
    if (p4serviceTab) {
        p4serviceTabHeight = 80;
    }

    var notificationTab = $("#NotificationListStrip").data("kendoTabStrip");
    if (notificationTab) {
        notificationTabHeight = 45;
    }

    //$("#mainshell").height(windowHeight - headerHeight - footerHeight - 80);
    var objectHeaderHeight = 0;
    $("div[id^='detailheader']").each(function () {
        objectHeaderHeight = $(this).height();
    });

    var shellHeight = windowHeight - headerHeight - homeIconsHeight - sectionTitleHeight - gridTabHeight - gridHeaderHeight - gridPagerHeight - checkBoxHeight - objectHeaderHeight;
    //$("#mainshell").css("min-height", shellHeight);

    $("div[id='grid']").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 30);

    $("div[id^='gridhome']").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 210);

    $("div[id^='gridmockup']").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 257);


    // server and p4service grids is different due to tabs
    if (iconHeaderHeight != null) {
        if (parseInt(objectHeaderHeight) > 0) { 
            $("div[id^='gridlarge']").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 70 );
            $("#gridlargeserver").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 63 - serverTabHeight );
            $("#gridId").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 30 - notificationTabHeight );
        } 
        else { 
            $("#gridlargeserver").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 70 - serverTabHeight );
            $("#gridId").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 70 - serverTabHeight );
            $("div[id^='gridlarge']").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 105 );
        }
        $("#gridlargeservice").find(".k-grid-content").height(shellHeight - iconHeaderHeight - serverTabHeight - 180 );
        $("#gridlargeproject").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 150 - p4serviceTabHeight);
        $("#gridlargep4service").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 100 - p4serviceTabHeight);
        $("#gridlargeinstance").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 75);
        $("#gridpatchingschedule").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 100);
    }
    else {
        $("div[id^='gridlarge']").find(".k-grid-content").height(shellHeight - serverTabHeight - 40);
        $("#gridlargeinstance").find(".k-grid-content").height(shellHeight - 80);
        $("#gridlargeservice").find(".k-grid-content").height(shellHeight - serverTabHeight - 80);
        $("#gridlargeserver").find(".k-grid-content").height(shellHeight - serverTabHeight);
        $("#gridlargep4service").find(".k-grid-content").height(shellHeight - p4serviceTabHeight);
        $("#gridlargeproject").find(".k-grid-content").height(shellHeight - 80);
        $("#gridlargetpc").find(".k-grid-content").height(shellHeight - 80);
        $("#gridlargeCustomer").find(".k-grid-content").height(shellHeight - 80);
        $("#gridlargerules").find(".k-grid-content").height(shellHeight - 80);
        $("#gridId").find(".k-grid-content").height(shellHeight - 50 - notificationTabHeight);
        $("#gridlargelicense").find(".k-grid-content").height(shellHeight - 85);
        $("#gridpatchingschedule").find(".k-grid-content").height(shellHeight - 100);
        

    }



    // researchjobs has hori scroll, to add more
    $("#gridlargeIndexerStatus").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 50 - 30);

    // ping config grid
    $("#gridlargeGoldenMonitor").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 122 - serverTabHeight);
    $("#gridlargeTestList").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 122 - serverTabHeight);
    $("#gridlargePingGroupList").find(".k-grid-content").height(shellHeight - iconHeaderHeight - 122 - serverTabHeight);


    $("#notesGrid").height(shellHeight - iconHeaderHeight - 40 - 180);

    //remove the scroll bar when there is NO record in the grid
    $("div[id^='gridlarge']").each(function () {
        var gridId = $(this).attr("id");
        var grid = $("#" + gridId).data("kendoGrid");
        if (grid) {
            var count = grid.dataSource.total();
            if (count == 0) {
                $("#" + gridId + " .k-grid-content").css("overflow-x", "hidden");
            }
        }
    });

}

// defined function to add hover effect and remove it when row is clicked

addExtraStylingToGrid = function (e) {
    resizeWindow();
    if ($("#serverliststrip").length > 0) {
        if (firstServerData) {
            firstServerData = false;
            /*
            if (firstCategoryName != null) {
                filterServerGridList(firstCategoryName);
            }
            */
        }
    }
    $(".k-grid > table > tbody > tr").off("mouseover mouseout").on("mouseover  mouseout", function () { $(this).toggleClass("k-state-hover"); });
    $(".k-grid-content > table > tbody > tr").off("mouseover mouseout").on("mouseover  mouseout", function () { $(this).toggleClass("k-state-hover"); });
};

var adminShown = false;
function showAdminMenu() {
    toggleMenu("indexicon", "linkMenu", true);
    toggleMenu("helpicon", "helpMenu", true);
    toggleMenu("settings", "adminMenu");
}

function showLinkMenu() {
    toggleMenu("settings", "adminMenu", true);
    toggleMenu("helpicon", "helpMenu", true);
    toggleMenu("indexicon", "linkMenu");
}
function showHelpMenu() {
    toggleMenu("settings", "adminMenu", true);
    toggleMenu("indexicon", "linkMenu", true);
    toggleMenu("helpicon", "helpMenu");
}

function HideMenus() {
    toggleMenu("settings", "adminMenu", true);
    toggleMenu("indexicon", "linkMenu", true);
    toggleMenu("helpicon", "helpMenu", true);
}

function toggleMenu(iconName, menuName, forceHide) {

    if ($("#" + menuName).length == 0) return;
    if ($("#" + iconName).length == 0) return;

    var menuwidth = $("#" + menuName).width();
    var menuHeight = $("#" + menuName).height();
    var settingsWidth = $("#" + iconName).height();
    var p = $("#" + iconName).position();
    $("#" + menuName).offset({ right: p.right - settingsWidth - 6 });
    var p2 = $("#" + menuName).position();
    // if top is zero, it is shown
    if (p2.top == 0 || forceHide) {
        $("#" + menuName).animate({ top: -menuHeight - 50 }, 200);
    }
    else {
        $("#" + menuName).animate({ top: 0 }, 200);
    }
}

function onEditBegin() {
    $("#frmEdit").hide();
    $("#saveResults").hide();
    $("#formLoader").show();
}
function onEditComplete() {
    $("#formLoader").hide();
    $("#saveResults").show();
    $("#frmEdit").show();
}

function checkForCookiesEnabled() {
    var cookieEnabled = (navigator.cookieEnabled) ? true : false;

    if (typeof navigator.cookieEnabled == "undefined" && !cookieEnabled) {
        document.cookie = "testcookie";
        cookieEnabled = (document.cookie.indexOf("testcookie") != -1) ? true : false;
    }
    return (cookieEnabled);
}

function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=");
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1;
            c_end = document.cookie.indexOf(";", c_start);
            if (c_end == -1) c_end = document.cookie.length;
            return unescape(document.cookie.substring(c_start, c_end));
        }
    }
    return "";
}

function setCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else var expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function showQuickSearch() {
    $('#divQuickSearchContainer').show();
    $('#txtQuickSearch').focus();
 
    $('#divHeartBeatContainer').hide();
}

function Healthstate(element) {
    var healthstatesource = GetHealthStatusSourcelist();
    element.kendoDropDownList({
        dataSource: healthstatesource,
        dataValueField: "HealthStateid"
        , dataTextField: "HealthState",
        optionLabel: "--Select Value--"
    });
}
function GetHealthStatusSourcelist() {
    return [
      { "HealthStateid": 0, "HealthState": "No Data" },
      { "HealthStateid": 1, "HealthState": "Good" },
      { "HealthStateid": 2, "HealthState": "Warning" },
      { "HealthStateid": 3, "HealthState": "Error" },
      { "HealthStateid": 4, "HealthState": "Planned" },
       { "HealthStateid": 5, "HealthState": "UnPlanned" },
        { "HealthStateid": 6, "HealthState": "Emergency" },
    ];
}
function VIPStatus(element) {
    var statussource = GetVIPStatusList();
    element.kendoDropDownList({
        dataSource: statussource,
        dataValueField: "Stateid"
        , dataTextField: "State",
        optionLabel: "--Select Value--"
    });
}
function GetVIPStatusList() {
    return [
      { "Stateid": 0, "State": "No Data" },
      { "Stateid": 1, "State": "Enabled" },
      { "Stateid": 2, "State": "Disabled" },
      { "Stateid": 3, "State": "Error" },
    ];
}



