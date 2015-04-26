// ==UserScript==
// @name        Redirect to Canonical URL
// @description Redirect page to its canonical URL.
// @namespace   http://www.efanzh.org/user-scripts/
// @include     http*://*/*
// @version     1.0
// ==/UserScript==

"use strict";

var filters =
[
    msdnFilter
];

function msdnFilter(url)
{
    if (/^https?:\/\/msdn\.microsoft\.com\/[^\/]*\/library\//.test(url))
    {
        url = url.replace(/msdn\.microsoft\.com\/[^\/]*/g, "msdn.microsoft.com");
        url = url.replace(/\([^\(\)]*\).aspx$/g, ".aspx");

        return { handled: true, url: url };
    }
    else
    {
        return { handled: false };
    }
}

function redirect()
{
    var canonical = document.querySelector("link[rel='canonical']");

    if (canonical != null)
    {
        var url = canonical.href;

        for (var i = 0; i < filters.length; ++i)
        {
            var result = filters[i](url);

            if (result.handled)
            {
                url = result.url;
                break;
            }
        }

        if (window.location.href != url)
        {
            window.location.href = url;
        }
    }
}

redirect();
