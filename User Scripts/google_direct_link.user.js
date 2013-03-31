// ==UserScript==
// @name        Google Search Direct Link
// @description Change Google Search's result links to direct links.
// @namespace   http://www.efanzh.org/google_direct_link/
// @include     http*://www.google.com/search*
// @version     1.0
// ==/UserScript==

var links = document.getElementsByClassName("l");

for (var i in links)
{
    links[i].onmousedown = null;
}
