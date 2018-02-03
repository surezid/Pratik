﻿var RND = RND || {};
//Constants Declared
RND.Constants = {
    AccessDenied: "Access denied.",
    AreYouDelete: "Are you sure want to delete it?"
};

function GetRootDirectory() {
    if (location.origin.indexOf('local') > -1)
        return "";
    else {
        var pathArr = location.pathname.substring(1, location.pathname.length).split('/');
        if (pathArr)
            return '/' + pathArr[0];
        else
            return "";
    }
}

function GetToken() {
    var token = $('#hdnToken').val();
    return token;
}