// ==UserScript==
// @name        Google Search Direct Link
// @description Change Google Search's result links to direct links.
// @namespace   http://www.efanzh.org/google_direct_link/
// @include     http*://www.google.com/search*
// @version     1.0
// ==/UserScript==
(function ()
{
    "use strict";

    var links = document.getElementsByClassName("r");

    for (var i in links)
    {
        var a = links[i].firstElementChild;
        if (a !== undefined)
        {
            a.onmousedown = null;
        }
    }
})();
