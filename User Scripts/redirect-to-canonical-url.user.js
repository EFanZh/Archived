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
    msdnFilter,
    createRegexFilter(/^https?:\/\/www\.imdb\.com\/(name|title)\//)
];

function createRegexFilter(regex)
{
    return function (url)
    {
        if (regex.test(url))
        {
            return { handled: true, url: url };
        }
        else
        {
            return { handled: false };
        }
    };
}

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
                if (window.location.href != result.url)
                {
                    window.location.href = result.url;
                }

                return;
            }
        }
    }
}

redirect();
