(function (d, w) {
    "use strict";
    var main;
    var source_container;
    var source;
    var splitter;
    var result_container;
    var result;
    var main_height_difference;
    var split_ratio = 1 - (Math.sqrt(5) - 1) / 2;
    function $(id) {
        return d.getElementById(id);
    }
    d.addEventListener("DOMContentLoaded", function () {
        main = $("main");
        source_container = $("source-container");
        source = $("source");
        splitter = $("splitter");
        result_container = $("result-container");
        result = $("result");
        w.onresize = function () {
            main.style.height = w.innerHeight - main_height_difference + "px";
            source_container.style.width = Math.ceil((main.clientWidth - splitter.offsetWidth) * split_ratio) + "px";
        };
        splitter.onmousedown = function (e) {
            result.style.pointerEvents = "none";
            var w0 = source_container.offsetWidth;
            var x0 = e.x;
            d.onmousemove = function (e) {
                source_container.style.width = w0 + e.x - x0 + "px";
            };
            d.onmouseup = function (e) {
                split_ratio = source_container.offsetWidth / (main.clientWidth - splitter.offsetWidth);
                result.style.pointerEvents = "";
                d.onmousemove = null;
                d.onmouseup = null;
            };
            return false;
        };
        source.oninput = function () {
            result.contentDocument.open();
            result.contentDocument.write(source.value);
            result.contentDocument.close();
        };
        main.style.height = "10000px";
        main_height_difference = d.documentElement.scrollHeight - main.offsetHeight;
        w.onresize();
    });
})(document, window);
