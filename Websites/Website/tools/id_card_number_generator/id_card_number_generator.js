window.onload = window_onload;

var adcInfo;

var ready = false;

function $(id)
{
    return document.getElementById(id);
}

function window_onload()
{
    $("administrative-division-1").onchange = administrative_division_1_onchange;
    $("administrative-division-2").onchange = administrative_division_2_onchange;
    $("administrative-division-3").onchange = administrative_division_3_onchange;
    $("birthday-y").onchange = birthday_y_onchange;
    $("birthday-m").onchange = birthday_m_onchange;
    $("birthday-d").onchange = birthday_d_onchange;
    $("gender").onchange = gender_onchange;
    $("regenerate").onclick = regenerate_onclick;

    var xhr = new XMLHttpRequest();
    xhr.onreadystatechange = function ()
    {
        if (xhr.readyState === 4 && xhr.status === 200)
        {
            parseADC(xhr.responseText);
            initialize();
        }
    };
    xhr.open("GET", "administrative_division_codes.txt", true);
    xhr.send(null);
}

function Division(name, code)
{
    this.name = name;
    this.code = code;
    this.children = [];
}

function getParent(division, n)
{
    var current = division;
    for (var i = 0; i < n; i++)
    {
        current = current.parent;
    }
    return current;
}

function parseADC(text)
{
    var lines = text.split("\r\n");
    var depth = 6;
    var current = new Division("root", "0");
    for (var lineNum in lines)
    {
        var line = lines[lineNum];
        if (line !== "")
        {
            var i = 7;
            while (line[i] === " ")
            {
                i++;
            }
            var newDivision = new Division(line.substring(i), line.substring(0, 6));
            if (i <= depth)
            {
                var parent = getParent(current, depth - i + 1);
                newDivision.parent = parent;
                parent.children.push(newDivision);
            }
            else
            {
                newDivision.parent = current;
                current.children.push(newDivision);
            }
            depth = i;
            current = newDivision;
        }
    }
    adcInfo = getParent(current, depth - 6);
}

function addZero(str, n)
{
    var s = "";
    if (str.length < n)
    {
        for (var i = 0; i < n - str.length; i++)
        {
            s += "0";
        }
        s += str;
    }
    else
    {
        s = str;
    }
    return s;
}

function initialize()
{
    var i;

    var arr = adcInfo.children;
    var s = "";
    for (i in arr)
    {
        s += "<option value=\"" + arr[i].code + "\">" + arr[i].name + " (" + arr[i].code + ")" + "</option>";
    }
    $("administrative-division-1").innerHTML = s;
    administrative_division_1_onchange();

    $("birthday-y").value = (new Date()).getFullYear() - 18;

    s = "";
    for (i = 1; i <= 12; i++)
    {
        s += "<option value=\"" + addZero(i + "", 2) + "\">" + i + "</option>";
    }
    $("birthday-m").innerHTML = s;
    birthday_m_onchange();

    ready = true;
    generate();
}

function findDivision(parent, code)
{
    var arr = parent.children;
    for (var i in arr)
    {
        if (arr[i].code === code)
        {
            return arr[i];
        }
    }
}

function administrative_division_1_onchange()
{
    var arr = findDivision(adcInfo, $("administrative-division-1").value).children;
    var s = "";
    for (var i in arr)
    {
        s += "<option value=\"" + arr[i].code + "\">" + arr[i].name + " (" + arr[i].code + ")" + "</option>";
    }
    $("administrative-division-2").innerHTML = s;
    administrative_division_2_onchange();
}

function administrative_division_2_onchange()
{
    var arr = findDivision(findDivision(adcInfo, $("administrative-division-1").value), $("administrative-division-2").value).children;
    var s = "";
    for (var i in arr)
    {
        s += "<option value=\"" + arr[i].code + "\">" + arr[i].name + " (" + arr[i].code + ")" + "</option>";
    }
    $("administrative-division-3").innerHTML = s;
    administrative_division_3_onchange();
}

function administrative_division_3_onchange()
{
    generate();
}

var days = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];

function getDays(year, month)
{
    if (month === 2)
    {
        if (year % 4 === 0 && year % 100 !== 0 || year % 400 === 0)
        {
            return days[month - 1] + 1;
        }
        else
        {
            return days[month - 1];
        }
    }
    else
    {
        return days[month - 1];
    }
}

function birthday_y_onchange()
{
    birthday_m_onchange();
}

function birthday_m_onchange()
{
    var n = getDays(parseInt($("birthday-y").value, 10), parseInt($("birthday-m").value, 10));
    var s = "";
    for (var i = 1; i <= n; i++)
    {
        s += "<option value=\"" + addZero(i + "", 2) + "\">" + i + "</option>";
    }
    $("birthday-d").innerHTML = s;
    birthday_d_onchange();
}

function birthday_d_onchange()
{
    generate();
}

function gender_onchange()
{
    generate();
}

function regenerate_onclick()
{
    generate();
}

var w = [1, 2, 4, 8, 5, 10, 9, 7, 3, 6, 1, 2, 4, 8, 5, 10, 9, 7];

function generate()
{
    if (ready)
    {
        var s = $("administrative-division-3").value + $("birthday-y").value + $("birthday-m").value + $("birthday-d").value;

        var r = parseInt(Math.random() * 500, 10) * 2;
        if ($("gender").value === "1")
        {
            r++;
        }
        s += addZero(r + "", 3);

        var sum = 0;
        for (var i = 1; i < 18; i++)
        {
            sum += s[17 - i] * w[i];
        }
        var a1 = (12 - (s % 11)) % 11;
        if (a1 === 10)
        {
            s += "X";
        }
        else
        {
            s += a1;
        }

        $("id-card-number").innerHTML = s;
    }
}
