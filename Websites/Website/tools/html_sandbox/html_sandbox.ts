(d, w) =>
{
    "use strict";

    var main: HTMLElement;
    var source_container: HTMLElement;
    var source: HTMLTextAreaElement;
    var splitter: HTMLDivElement;
    var result_container: HTMLDivElement;
    var result: HTMLIFrameElement;

    var main_height_difference: number;
    var split_ratio: number = 1 - (Math.sqrt(5) - 1) / 2;

    function $(id: string)
    {
        return d.getElementById(id);
    }

    d.addEventListener("DOMContentLoaded", () =>
    {
        main = $("main");
        source_container = $("source-container");
        source = <HTMLTextAreaElement>$("source");
        splitter = <HTMLDivElement>$("splitter");
        result_container = <HTMLDivElement>$("result-container");
        result = <HTMLIFrameElement>$("result");

        w.onresize = () =>
        {
            main.style.height = w.innerHeight - main_height_difference + "px";
            source_container.style.width = Math.ceil((main.clientWidth - splitter.offsetWidth) * split_ratio) + "px";
        };

        splitter.onmousedown = (e: MouseEvent) =>
        {
            result.style.pointerEvents = "none";
            var w0 = source_container.offsetWidth;
            var x0 = e.x;

            d.onmousemove = (e: MouseEvent) =>
            {
                source_container.style.width = w0 + e.x - x0 + "px";
            };

            d.onmouseup = (e: MouseEvent) =>
            {
                split_ratio = source_container.offsetWidth / (main.clientWidth - splitter.offsetWidth);

                result.style.pointerEvents = "";
                d.onmousemove = null;
                d.onmouseup = null;
            };
            return false;
        };

        source.oninput = () =>
        {
            result.contentDocument.open();
            result.contentDocument.write(source.value);
            result.contentDocument.close();
        };

        main.style.height = "10000px"; // I REALLY hate this.
        main_height_difference = d.documentElement.scrollHeight - main.offsetHeight;
        w.onresize();
    });
} (document, window);
