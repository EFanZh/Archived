// ==UserScript==
// @name        MSDN Redirect
// @description Redirect MSDN library to .
// @namespace   http://www.efanzh.org/msdn_redirect/
// @include     http*://msdn.microsoft.com/*
// @version     1.0
// ==/UserScript==

function msdn_redirect()
{
    "use strict";

    var url = window.location.href.toLowerCase();

    if (url.indexOf("library") > 0)
    {
        var target_url = url.replace(/msdn.microsoft.com\/..-..\//, "msdn.microsoft.com/").replace(/\(.*\)(.aspx)?$/, ".aspx");

        if (url != target_url)
        {
            window.location.href = target_url;
        }
    }
}

msdn_redirect();
