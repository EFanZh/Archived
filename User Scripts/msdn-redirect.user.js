// ==UserScript==
// @name        MSDN Redirect
// @description Redirect MSDN library to .
// @namespace   http://www.efanzh.org/msdn-redirect/
// @include     http*://msdn.microsoft.com/*
// @version     1.0
// ==/UserScript==

function msdn_redirect()
{
    "use strict";

    var url = document.querySelector("link[rel='canonical']").href;

    if (url.indexOf("library") > 0)
    {
        var target_url = url.replace(/msdn.microsoft.com\/..-..\//, "msdn.microsoft.com/").replace(/\(.*\)(.aspx)?$/, ".aspx");

        if (window.location.href != target_url)
        {
            window.location.href = target_url;
        }
    }
}

msdn_redirect();
