(function (d)
{
    "use strict";

    var data_source_script = null;
    var data_source;
    var data_source_load_status;

    var data_source_list;
    var data_source_selected_index = 0;
    var hero_data;
    var translate = false;
    var skill_count = 4;

    function $(id)
    {
        return d.getElementById(id);
    }

    d.addEventListener("DOMContentLoaded", function ()
    {
        data_source = $("data-source");
        data_source.onchange = function ()
        {
            this.disabled = true;
            if (data_source_script !== null)
            {
                d.head.removeChild(data_source_script);
            }
            data_source_selected_index = this.selectedIndex;
            async_load(this.value, load_success, load_error);
        };

        data_source_load_status = $("data-source-load-status");

        $("translate").onchange = function ()
        {
            translate = this.checked;
            build_hero_table();
        };

        window.onresize = layout;

        $("footer").style.left = $("footer").offsetLeft + "px";
        $("footer").style.right = $("footer").style.left;

        layout();

        var xhr = new XMLHttpRequest();
        xhr.open("GET", "hero_data_source_list.json", false);
        xhr.send();
        data_source_list = JSON.parse(xhr.responseText);
        for (var i in data_source_list)
        {
            var option = d.createElement("option");
            option.value = data_source_list[i].script;
            option.appendChild(d.createTextNode(option.value));
            data_source.add(option);
        }

        async_load(data_source.value, load_success, load_error);
    });

    function layout()
    {
        $("dota-heroes-data-content").style.height = window.innerHeight - $("dota-heroes-data-content").offsetTop - $("footer").offsetHeight + "px";
    }

    function async_load(src, onload, onerror)
    {
        data_source.disabled = true;
        data_source_load_status.innerHTML = "Loading...";
        data_source_script = d.createElement("script");
        data_source_script.src = src;
        data_source_script.onload = onload;
        data_source_list.onerror = onerror;
        d.head.appendChild(data_source_script);
    }

    function load_success()
    {
        build_hero_data();
        build_hero_table();
        data_source.disabled = false;
        data_source_load_status.innerHTML = "Load success.";
    }

    function load_error()
    {
        data_source.disabled = false;
        data_source_load_status.innerHTML = "Load error.";
    }

    function build_hero_data()
    {
        hero_data = [];

        var parser = new DOMParser();
        var hero_exclude_list = ["140"];

        function get_get_property_function(doc)
        {
            return function (key)
            {
                return doc.firstChild.getElementsByTagName(key)[0].firstChild.nodeValue.trim();
            };
        }

        function get_get_hero_three_d_property_function(get_prop_func)
        {
            return function (key)
            {
                var prop = get_prop_func(key).trim().split(/\s*\+\s*/);
                var result = {};
                result.value = parseFloat(prop[0]);
                result.growth = parseFloat(prop[1]);
                return result;
            };
        }

        function get_get_other_data_function(get_prop_func)
        {
            var data = parser.parseFromString(decodeURIComponent(get_property("stats")), "text/xml").firstChild.getElementsByTagName("li");
            return function (index)
            {
                return data[index].lastChild.nodeValue.trim();
            };
        }

        function get_get_skill_data_function(get_prop_func)
        {
            var data = parser.parseFromString(decodeURIComponent(get_property("skills")), "text/xml").firstChild.getElementsByTagName("li");
            return function (index)
            {
                var result = {};
                result.icon = data[index].getElementsByTagName("img")[0].attributes.src.nodeValue.trim();
                var spans = data[index].getElementsByTagName("span");
                result.name = spans[0].firstChild.nodeValue.trim();
                result.hot_key = spans[1].firstElementChild.firstChild.nodeValue.trim();
                return result;
            };
        }

        for (var id in HD)
        {
            if (hero_exclude_list.indexOf(id) !== -1)
            {
                continue;
            }

            var hero = {};
            var xml_doc = parser.parseFromString(HD[id], "text/xml");
            var get_property = get_get_property_function(xml_doc);

            hero.key = parseFloat(get_property("key"));
            hero.icon = get_property("icon");
            hero.name = get_property("name");
            hero.image_name = get_property("imagename");
            hero.class = get_property("class");
            hero.hclass = parseFloat(get_property("hclass"));

            var get_hero_three_d_property = get_get_hero_three_d_property_function(get_property);

            hero.strength = get_hero_three_d_property("strength");
            hero.agility = get_hero_three_d_property("ability");
            hero.intelligence = get_hero_three_d_property("intelligence");

            var get_other_data = get_get_other_data_function(get_property);

            hero.affiliation = get_other_data(0);

            var damage = get_other_data(1).split(/\s*-\s*/);
            hero.damage = {};
            hero.damage.min = parseFloat(damage[0]);
            hero.damage.max = parseFloat(damage[1]);

            hero.armor = parseFloat(get_other_data(2));
            hero.move_speed = parseFloat(get_other_data(3));
            hero.attack_range = parseFloat(get_other_data(4));

            var attack_animation = get_other_data(5).split(/\s*\/\s*/);
            hero.attack_animation = {};
            hero.attack_animation.before = parseFloat(attack_animation[0]);
            hero.attack_animation.after = parseFloat(attack_animation[1]);

            var casting_animation = get_other_data(6).split(/\s*\/\s*/);
            hero.casting_animation = {};
            hero.casting_animation.before = parseFloat(casting_animation[0]);
            hero.casting_animation.after = parseFloat(casting_animation[1]);

            hero.base_attack_time = parseFloat(get_other_data(7));
            hero.missile_speed = parseFloat(get_other_data(8));

            var sight_range = get_other_data(9).split(/\s*\/\s*/);
            hero.sight_range = {};
            hero.sight_range.day = parseFloat(sight_range[0]);
            hero.sight_range.night = parseFloat(sight_range[1]);

            var get_skill_data = get_get_skill_data_function(get_property);
            hero.skills = [];

            for (var i = 0; i < skill_count; i++)
            {
                hero.skills[i] = get_skill_data(i);
            }

            hero_data[id] = hero;
        }
    }

    function build_hero_table()
    {
        var table = $("dota-heroes-raw-data-table");
        var table_cols =
        [
            "ID",
            "Key",
            "Icon",
            "Name",
            "Image Name",
            "Job",
            "Main Property",
            "Strength",
            "Strength Growth",
            "Agility",
            "Agility Growth",
            "Intelligence",
            "Intelligence Growth",
            "Affiliation",
            "Min Damage",
            "Max Damage",
            "Armor",
            "Move Speed",
            "Attack Range",
            "Attack Animation (Before)",
            "Attack Animation (After)",
            "Casting Animation (Before)",
            "Casting Animation (After)",
            "Base Attack Time",
            "Missile Speed",
            "Sight Range (Day)",
            "Sight Range (Night)",
            "Skill 1 Icon",
            "Skill 1 Name",
            "Skill 1 Hot Key",
            "Skill 2 Icon",
            "Skill 2 Name",
            "Skill 2 Hot Key",
            "Skill 3 Icon",
            "Skill 3 Name",
            "Skill 3 Hot Key",
            "Skill 4 Icon",
            "Skill 4 Name",
            "Skill 4 Hot Key"
        ];
        var i;
        var do_translate;

        if (translate)
        {
            do_translate = function (dict, str)
            {
                var val = dict[str];
                if (val !== undefined && val.length > 0)
                {
                    return val;
                }
            };
        }
        else
        {
            do_translate = function (dict, str)
            {
                return str;
            };
        }

        function add_text_content(element, text)
        {
            element.appendChild(d.createTextNode(text));
        }

        while (table.tHead.rows.length > 0)
        {
            table.tHead.deleteRow(-1);
        }
        var head_row = table.tHead.insertRow(-1);
        for (i in table_cols)
        {
            var cell = d.createElement("th");
            add_text_content(cell, table_cols[i]);
            head_row.appendChild(cell);
        }

        while (table.tBodies[0].rows.length > 0)
        {
            table.tBodies[0].deleteRow(-1);
        }
        for (i in hero_data)
        {
            var row = table.tBodies[0].insertRow(-1);
            add_text_content(row.insertCell(-1), i);

            var hero = hero_data[i];
            add_text_content(row.insertCell(-1), hero.key);

            var icon = d.createElement("img");
            icon.src = data_source_list[data_source_selected_index].media + "hero/" + i + "/" + hero.icon;
            row.insertCell(-1).appendChild(icon);

            add_text_content(row.insertCell(-1), do_translate(name_translate, hero.name));

            icon = d.createElement("img");
            icon.src = data_source_list[data_source_selected_index].media + "hero/" + i + "/" + hero.image_name;
            row.insertCell(-1).appendChild(icon);
            add_text_content(row.insertCell(-1), do_translate(job_translate, hero.class));
            add_text_content(row.insertCell(-1), hero.hclass);
            add_text_content(row.insertCell(-1), hero.strength.value);
            add_text_content(row.insertCell(-1), hero.strength.growth);
            add_text_content(row.insertCell(-1), hero.agility.value);
            add_text_content(row.insertCell(-1), hero.agility.growth);
            add_text_content(row.insertCell(-1), hero.intelligence.value);
            add_text_content(row.insertCell(-1), hero.intelligence.growth);
            add_text_content(row.insertCell(-1), hero.affiliation);
            add_text_content(row.insertCell(-1), hero.damage.min);
            add_text_content(row.insertCell(-1), hero.damage.max);
            add_text_content(row.insertCell(-1), hero.armor);
            add_text_content(row.insertCell(-1), hero.move_speed);
            add_text_content(row.insertCell(-1), hero.attack_range);
            add_text_content(row.insertCell(-1), hero.attack_animation.before);
            add_text_content(row.insertCell(-1), hero.attack_animation.after);
            add_text_content(row.insertCell(-1), hero.casting_animation.before);
            add_text_content(row.insertCell(-1), hero.casting_animation.after);
            add_text_content(row.insertCell(-1), hero.base_attack_time);
            add_text_content(row.insertCell(-1), isNaN(hero.missile_speed) ? "Instant" : hero.missile_speed);
            add_text_content(row.insertCell(-1), hero.sight_range.day);
            add_text_content(row.insertCell(-1), hero.sight_range.night);
            for (var j = 0; j < hero.skills.length; j++)
            {
                icon = d.createElement("img");
                icon.src = hero.skills[j].icon;
                row.insertCell(-1).appendChild(icon);
                add_text_content(row.insertCell(-1), do_translate(skill_translate, hero.skills[j].name));
                add_text_content(row.insertCell(-1), hero.skills[j].hot_key);
            }
        }
    }
})(document);
