// ==UserScript==
// @name        Redirect to Canonical URL
// @description Redirect page to its canonical URL.
// @namespace   http://www.efanzh.org/user-scripts/
// @include     http*://*/*
// @version     1.0
// ==/UserScript==

"use strict";

var whiteList =
[
    msdn,
    createRegexWhiteListItem(/^https?:\/\/www\.imdb\.com\/(name|title)\//)
];

function createRegexWhiteListItem(regex)
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

function msdn(url)
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
    var canonical = document.querySelector('link[rel="canonical"]');

    if (canonical != null)
    {
        var url = canonical.href;

        for (var i = 0; i < whiteList.length; ++i)
        {
            var result = whiteList[i](url);

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
