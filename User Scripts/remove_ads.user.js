// ==UserScript==
// @name        Remove Ads
// @description Remove Ads
// @namespace   RemoveAds
// @include      *://www.narutom.com/*
// @include      *://narutom.com/*
// @version     1.0
// ==/UserScript==

function remove(id)
{
    var elem = document.getElementById(id);
    if (elem != null)
    {
        elem.parentNode.removeChild(elem);
    }
}

remove("ulink_fmt");
remove("xcy_fmt");
remove("a_z_8");
remove("a_z_40");
