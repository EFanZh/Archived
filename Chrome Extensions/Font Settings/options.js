var default_font_script_code = "Zyyy";
var preview_text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";

var font_scripts =
{
    "Zyyy": "Code for undetermined script",
    "Afak": "Afaka",
    "Arab": "Arabic",
    "Armi": "Imperial Aramaic",
    "Armn": "Armenian",
    "Avst": "Avestan",
    "Bali": "Balinese",
    "Bamu": "Bamum",
    "Bass": "Bassa Vah",
    "Batk": "Batak",
    "Beng": "Bengali",
    "Blis": "Blissymbols",
    "Bopo": "Bopomofo",
    "Brah": "Brahmi",
    "Brai": "Braille",
    "Bugi": "Buginese",
    "Buhd": "Buhid",
    "Cakm": "Chakma",
    "Cans": "Unified Canadian Aboriginal Syllabics",
    "Cari": "Carian",
    "Cham": "Cham",
    "Cher": "Cherokee",
    "Cirt": "Cirth",
    "Copt": "Coptic",
    "Cprt": "Cypriot",
    "Cyrl": "Cyrillic",
    "Cyrs": "Cyrillic (Old Church Slavonic variant)",
    "Deva": "Devanagari (Nagari)",
    "Dsrt": "Deseret (Mormon)",
    "Dupl": "Duployan shorthand, Duployan stenography",
    "Egyd": "Egyptian demotic",
    "Egyh": "Egyptian hieratic",
    "Egyp": "Egyptian hieroglyphs",
    "Elba": "Elbasan",
    "Ethi": "Ethiopic (Geʻez)",
    "Geok": "Khutsuri (Asomtavruli and Nuskhuri)",
    "Geor": "Georgian (Mkhedruli)",
    "Glag": "Glagolitic",
    "Goth": "Gothic",
    "Gran": "Grantha",
    "Grek": "Greek",
    "Gujr": "Gujarati",
    "Guru": "Gurmukhi",
    "Hang": "Hangul (Hangŭl, Hangeul)",
    "Hani": "Han (Hanzi, Kanji, Hanja)",
    "Hano": "Hanunoo (Hanunóo)",
    "Hans": "Han (Simplified variant)",
    "Hant": "Han (Traditional variant)",
    "Hebr": "Hebrew",
    "Hluw": "Anatolian Hieroglyphs (Luwian Hieroglyphs, Hittite Hieroglyphs)",
    "Hmng": "Pahawh Hmong",
    "Hung": "Old Hungarian (Hungarian Runic)",
    "Inds": "Indus (Harappan)",
    "Ital": "Old Italic (Etruscan, Oscan, etc.)",
    "Java": "Javanese",
    "Jpan": "Japanese (alias for Han + Hiragana + Katakana)",
    "Jurc": "Jurchen",
    "Kali": "Kayah Li",
    "Khar": "Kharoshthi",
    "Khmr": "Khmer",
    "Khoj": "Khojki",
    "Knda": "Kannada",
    "Kpel": "Kpelle",
    "Kthi": "Kaithi",
    "Lana": "Tai Tham (Lanna)",
    "Laoo": "Lao",
    "Latf": "Latin (Fraktur variant)",
    "Latg": "Latin (Gaelic variant)",
    "Latn": "Latin",
    "Lepc": "Lepcha (Róng)",
    "Limb": "Limbu",
    "Lina": "Linear A",
    "Linb": "Linear B",
    "Lisu": "Lisu (Fraser)",
    "Loma": "Loma",
    "Lyci": "Lycian",
    "Lydi": "Lydian",
    "Mand": "Mandaic, Mandaean",
    "Mani": "Manichaean",
    "Maya": "Mayan hieroglyphs",
    "Mend": "Mende",
    "Merc": "Meroitic Cursive",
    "Mero": "Meroitic Hieroglyphs",
    "Mlym": "Malayalam",
    "Mong": "Mongolian",
    "Moon": "Moon (Moon code, Moon script, Moon type)",
    "Mroo": "Mro, Mru",
    "Mtei": "Meitei Mayek (Meithei, Meetei)",
    "Mymr": "Myanmar (Burmese)",
    "Narb": "Old North Arabian (Ancient North Arabian)",
    "Nbat": "Nabataean",
    "Nkgb": "Nakhi Geba (\"Na-\"Khi ²Ggŏ-¹baw, Naxi Geba)",
    "Nkoo": "N’Ko",
    "Nshu": "Nüshu",
    "Ogam": "Ogham",
    "Olck": "Ol Chiki (Ol Cemet’, Ol, Santali)",
    "Orkh": "Old Turkic, Orkhon Runic",
    "Orya": "Oriya",
    "Osma": "Osmanya",
    "Palm": "Palmyrene",
    "Perm": "Old Permic",
    "Phag": "Phags-pa",
    "Phli": "Inscriptional Pahlavi",
    "Phlp": "Psalter Pahlavi",
    "Phlv": "Book Pahlavi",
    "Phnx": "Phoenician",
    "Plrd": "Miao (Pollard)",
    "Prti": "Inscriptional Parthian",
    "Rjng": "Rejang (Redjang, Kaganga)",
    "Roro": "Rongorongo",
    "Runr": "Runic",
    "Samr": "Samaritan",
    "Sara": "Sarati",
    "Sarb": "Old South Arabian",
    "Saur": "Saurashtra",
    "Sgnw": "SignWriting",
    "Shaw": "Shavian (Shaw)",
    "Shrd": "Sharada, Śāradā",
    "Sind": "Khudawadi, Sindhi",
    "Sinh": "Sinhala",
    "Sora": "Sora Sompeng",
    "Sund": "Sundanese",
    "Sylo": "Syloti Nagri",
    "Syrc": "Syriac",
    "Syre": "Syriac (Estrangelo variant)",
    "Syrj": "Syriac (Western variant)",
    "Syrn": "Syriac (Eastern variant)",
    "Tagb": "Tagbanwa",
    "Takr": "Takri, Ṭākrī, Ṭāṅkrī",
    "Tale": "Tai Le",
    "Talu": "New Tai Lue",
    "Taml": "Tamil",
    "Tang": "Tangut",
    "Tavt": "Tai Viet",
    "Telu": "Telugu",
    "Teng": "Tengwar",
    "Tfng": "Tifinagh (Berber)",
    "Tglg": "Tagalog (Baybayin, Alibata)",
    "Thaa": "Thaana",
    "Thai": "Thai",
    "Tibt": "Tibetan",
    "Tirh": "Tirhuta",
    "Ugar": "Ugaritic",
    "Vaii": "Vai",
    "Visp": "Visible Speech",
    "Wara": "Warang Citi (Varang Kshiti)",
    "Wole": "Woleai",
    "Xpeo": "Old Persian",
    "Xsux": "Cuneiform, Sumero-Akkadian",
    "Yiii": "Yi",
    "Zmth": "Mathematical notation",
    "Zsym": "Symbols"
};

var generic_families = ["standard", "sansserif", "serif", "fixed", "cursive", "fantasy"];

document.addEventListener("DOMContentLoaded", document_OnDOMContentLoaded);

function Semapohre(resource)
{
    this.resource = resource;
    this.p = function ()
    {
        this.resource--;
    };
    this.v = function ()
    {
        this.resource++;
    };
}

function $(id)
{
    return document.getElementById(id);
}

function setContent(id, value)
{
    $(id).innerHTML = value;
}

function getMessage(name)
{
    return chrome.i18n.getMessage(name);
}

function setI18nContent(id, name)
{
    setContent(id, getMessage(name));
}

function getValue(id, value)
{
    return $(id).value;
}

function setValue(id, value)
{
    $(id).value = value;
}

function document_OnDOMContentLoaded()
{
    var i;

    // Set document title.
    document.title = getMessage("page_title");
    setI18nContent("font-settings-title", "page_title");

    // Set font family settings section.
    setI18nContent("font-family-settings-title", "page_font_family_settings");

    // Font script
    setContent("font-script-title", getMessage("page_font_script") + ":");
    for (i in font_scripts)
    {
        var option = document.createElement("option");
        option.text = font_scripts[i];
        option.value = i;
        $("font-script").add(option);
    }
    $("font-script").onchange = function ()
    {
        this.disabled = true;
        var sem = new Semapohre(-generic_families.length);
        for (var i in generic_families)
        {
            var details = {};
            if (this.value !== default_font_script_code)
            {
                details.script = this.value;
            }
            details.genericFamily = generic_families[i];
            chrome.fontSettings.getFont(details, getGetFontCallbackHandler(generic_families[i], sem));
        }
    };
    for (i in generic_families)
    {
        var row = $("font-family-settings-table").insertRow(-1);
        var cell = row.insertCell(-1);
        var label = document.createElement("label");
        var id = "generic-font-family-" + generic_families[i];
        label.htmlFor = id;
        label.innerHTML = getMessage("page_generic_font_family_" + generic_families[i]) + ":";
        cell.appendChild(label);
        cell = row.insertCell(-1);
        var select = document.createElement("select");
        select.id = id;
        cell.appendChild(select);
        cell = row.insertCell(-1);
        var button = document.createElement("button");
        button.id = "clear-generic-font-family-" + generic_families[i];
        button.type = "button";
        button.onclick = getClearGenericFontFamilyOnClickHandler(generic_families[i]);
        button.innerHTML = getMessage("page_clear");
        cell.appendChild(button);
        cell = row.insertCell(-1);
        cell.id = id + "-preview";
        cell.innerHTML = preview_text;
    }
    chrome.fontSettings.getFontList(function (font_names)
    {
        for (var i in generic_families)
        {
            var select = $("generic-font-family-" + generic_families[i]);
            for (var j in font_names)
            {
                var option = document.createElement("option");
                option.value = font_names[j].fontId;
                option.text = font_names[j].displayName;
                select.add(option);
            }
            select.onchange = getGenericFontFamilyOnChangeHandler(generic_families[i]);
        }

        // Set font family settings section.
        setI18nContent("font-size-settings-title", "page_font_size_settings");

        // Default Font Size
        setContent("default-font-size-title", getMessage("page_default_font_size") + ":");
        setI18nContent("clear-default-font-size", "page_clear");
        setContent("default-font-size-preview", preview_text);
        $("default-font-size").onchange = function ()
        {
            this.disabled = true;
            chrome.fontSettings.setDefaultFontSize({ pixelSize: parseInt(this.value, 10) }, function ()
            {
                $("default-font-size").disabled = false;
            });
        };
        $("clear-default-font-size").onclick = function ()
        {
            this.disabled = true;
            chrome.fontSettings.clearDefaultFontSize({}, function ()
            {
                $("clear-default-font-size").disabled = false;
            });
        };

        // Default Fixed Font Size
        setContent("default-fixed-font-size-title", getMessage("page_default_fixed_font_size") + ":");
        setI18nContent("clear-default-fixed-font-size", "page_clear");
        setContent("default-fixed-font-size-preview", preview_text);
        $("default-fixed-font-size").onchange = function ()
        {
            this.disabled = true;
            chrome.fontSettings.setDefaultFixedFontSize({ pixelSize: parseInt(this.value, 10) }, function ()
            {
                $("default-fixed-font-size").disabled = false;
            });
        };
        $("clear-default-fixed-font-size").onclick = function ()
        {
            this.disabled = true;
            chrome.fontSettings.clearDefaultFixedFontSize({}, function ()
            {
                $("clear-default-fixed-font-size").disabled = false;
            });
        };

        // Minimum Font Size
        setContent("minimum-font-size-title", getMessage("page_minimum_font_size") + ":");
        setI18nContent("clear-minimum-font-size", "page_clear");
        setContent("minimum-font-size-preview", preview_text);
        $("minimum-font-size").onchange = function ()
        {
            this.disabled = true;
            chrome.fontSettings.setMinimumFontSize({ pixelSize: parseInt(this.value, 10) }, function ()
            {
                $("minimum-font-size").disabled = false;
            });
        };
        $("clear-minimum-font-size").onclick = function ()
        {
            this.disabled = true;
            chrome.fontSettings.clearMinimumFontSize({}, function ()
            {
                $("clear-minimum-font-size").disabled = false;
            });
        };

        // Chrome Events & Get Values
        chrome.fontSettings.onFontChanged.addListener(function (details)
        {
            if ((details.script === undefined && getValue("font-script") === default_font_script_code) || (details.script === getValue("font-script")))
            {
                var id = "generic-font-family-" + details.genericFamily;
                setValue(id, details.fontId);
                $(id + "-preview").style.fontFamily = details.fontId;
                if (details.genericFamily === "standard")
                {
                    $("default-font-size-preview").style.fontFamily = details.fontId;
                    $("minimum-font-size-preview").style.fontFamily = details.fontId;
                }
                else if (details.genericFamily === "fixed")
                {
                    $("default-fixed-font-size-preview").style.fontFamily = details.fontId;
                }
            }
        });
        for (i in generic_families)
        {
            var details = {};
            var script = getValue("font-script");
            if (script !== default_font_script_code)
            {
                details.script = script;
            }
            details.genericFamily = generic_families[i];
            chrome.fontSettings.getFont(details, getGetFontCallbackHandler(generic_families[i], null));
        }

        var updateDefaultFontSize = function (details)
        {
            var id = "default-font-size";
            setValue(id, details.pixelSize);
            var size_px = details.pixelSize + "px";
            for (var i in generic_families)
            {
                $("generic-font-family-" + generic_families[i] + "-preview").style.fontSize = size_px;
            }
            $(id + "-preview").style.fontSize = size_px;
        };
        chrome.fontSettings.onDefaultFontSizeChanged.addListener(updateDefaultFontSize);
        chrome.fontSettings.getDefaultFontSize({}, updateDefaultFontSize);

        var updateDefaultFixedFontSize = function (details)
        {
            var id = "default-fixed-font-size";
            setValue(id, details.pixelSize);
            $(id + "-preview").style.fontSize = details.pixelSize + "px";
        };
        chrome.fontSettings.onDefaultFixedFontSizeChanged.addListener(updateDefaultFixedFontSize);
        chrome.fontSettings.getDefaultFixedFontSize({}, updateDefaultFixedFontSize);

        var updateMinimumFontSize = function (details)
        {
            var id = "minimum-font-size";
            setValue(id, details.pixelSize);
            $(id + "-preview").style.fontSize = details.pixelSize + "px";
        };
        chrome.fontSettings.onMinimumFontSizeChanged.addListener(updateMinimumFontSize);
        chrome.fontSettings.getMinimumFontSize({}, updateMinimumFontSize);

        // Clear font settings section.
        setI18nContent("clear-font-settings-title", "page_clear_font_settings");
        setContent("clear-all-font-settings-title", getMessage("page_clear_all_font_settings") + ":");
        setI18nContent("clear-all-font-settings", "page_clear");

        // Set footer text;
        var p = document.createElement("p");
        p.innerHTML = getMessage("page_footer_text");
        $("footer").appendChild(p);
    });

    $("clear-all-font-settings").onclick = function ()
    {
        this.disabled = true;
        var task_count = 3;
        for (var i in font_scripts)
        {
            task_count += generic_families.length;
        }
        var sem = new Semapohre(-task_count);
        for (i in font_scripts)
        {
            for (var j in generic_families)
            {
                var details = {};
                var script = i;
                if (script !== default_font_script_code)
                {
                    details.script = script;
                }
                details.genericFamily = generic_families[j];
                chrome.fontSettings.clearFont(details, getClearFontSettingsCallbackHandler("clear-all-font-settings", sem));
            }
        }
        chrome.fontSettings.clearDefaultFontSize({}, getClearFontSettingsCallbackHandler("clear-all-font-settings", sem));
        chrome.fontSettings.clearDefaultFixedFontSize({}, getClearFontSettingsCallbackHandler("clear-all-font-settings", sem));
        chrome.fontSettings.clearMinimumFontSize({}, getClearFontSettingsCallbackHandler("clear-all-font-settings", sem));
    };
}

function getGetFontCallbackHandler(generic_family, sem)
{
    return function (details)
    {
        var id = "generic-font-family-" + generic_family;
        setValue(id, details.fontId);
        $(id + "-preview").style.fontFamily = details.fontId;
        if (generic_family === "standard")
        {
            $("default-font-size-preview").style.fontFamily = details.fontId;
            $("minimum-font-size-preview").style.fontFamily = details.fontId;
        }
        else if (generic_family === "fixed")
        {
            $("default-fixed-font-size-preview").style.fontFamily = details.fontId;
        }
        if (sem !== null)
        {
            sem.v();
            if (sem.resource === 0)
            {
                $("font-script").disabled = false;
            }
        }
    };
}

function getClearGenericFontFamilyOnClickHandler(generic_family)
{
    return function ()
    {
        this.disabled = true;
        var details = {};
        var script = getValue("font-script");
        if (script !== default_font_script_code)
        {
            details.script = script;
        }
        details.genericFamily = generic_family;
        chrome.fontSettings.clearFont(details, function ()
        {
            $("clear-generic-font-family-" + generic_family).disabled = false;
        });
    };
}

function getGenericFontFamilyOnChangeHandler(generic_family)
{
    return function ()
    {
        this.disabled = true;
        var details = {};
        var script = getValue("font-script");
        if (script !== default_font_script_code)
        {
            details.script = script;
        }
        details.genericFamily = generic_family;
        details.fontId = this.value;
        chrome.fontSettings.setFont(details, function ()
        {
            $("generic-font-family-" + generic_family).disabled = false;
        });
    };
}

function getClearFontSettingsCallbackHandler(id, sem)
{
    return function ()
    {
        sem.v();
        if (sem.resource === 0)
        {
            $(id).disabled = false;
        }
    };
}
