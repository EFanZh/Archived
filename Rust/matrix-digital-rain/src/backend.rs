use raindrop::*;
use rand::*;
use std::cmp::*;
use utilities::*;

struct Configuration
{
    lambda_mutation: f64,
    lambda_generate: f64,
    minimal_speed: f64,
    maximal_speed: f64,
    minimal_raindrop_size: usize,
    maximal_raindrop_size: usize,
    character_candidates: Vec<char>
}

impl Configuration
{
    fn new() -> Configuration
    {
        return Configuration {
            lambda_mutation: 0.2,
            lambda_generate: 1.0,
            minimal_speed: 16.0,
            maximal_speed: 32.0,
            minimal_raindrop_size: 12,
            maximal_raindrop_size: 36,
            character_candidates: "!\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~ ¡¢£¤¥¦§¨©ª«¬®¯°±²³´µ¶·¸¹º»¼½¾¿ÀÁÂÃÄÅÆÇÈÉÊËÌÍÎÏÐÑÒÓÔÕÖ×ØÙÚÛÜÝÞßàáâãäåæçèéêëìíîïðñòóôõö÷øùúûüýþÿĀāĂăĄąĆćĈĉĊċČčĎďĐđĒēĔĕĖėĘęĚěĜĝĞğĠġĢģĤĥĦħĨĩĪīĬĭĮįİıĲĳĴĵĶķĸĹĺĻļĽľĿŀŁłŃńŅņŇňŉŊŋŌōŎŏŐőŒœŔŕŖŗŘřŚśŜŝŞşŠšŢţŤťŦŧŨũŪūŬŭŮůŰűŲųŴŵŶŷŸŹźŻżŽžſƀƊƏƒƓƠơƯưǂǍǎǏǐǑǒǓǔǕǖǗǘǙǚǛǜǢǣǦǧǪǫǴǵǸǹǺǻǼǽǾǿȘșȚțɃɐɑɒɓɔɕɖɗɘəɚɛɜɞɟɠɡɢɣɤɥɦɧɨɪɫɬɭɮɯɰɱɲɳɴɵɶɸɹɺɻɽɾʀʁʂʃʄʇʈʉʊʋʌʍʎʏʐʑʒʔʕʘʙʜʝʞʟʡʢʤʦʧʰʲʳʷʸʹʻʼʾʿˁˆˇˈˉˊˋˌːˑ˘˙˚˛˝ˠˡˢˣ;΄΅Ά·ΈΉΊΌΎΏΐΑΒΓΔΕΖΗΘΙΚΛΜΝΞΟΠΡΣΤΥΦΧΨΩΪΫάέήίΰαβγδεζηθικλμνξοπρςστυφχψωϊϋόύώϐϑϕϗϙϛϝϡЀЁЂЃЄЅІЇЈЉЊЋЌЍЎЏАБВГДЕЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдежзийклмнопрстуфхцчшщъыьэюяѐёђѓєѕіїјљњћќѝўџѢѣѲѳѴѵҐґҒғҖҗҘҙҚқҠҡҢңҪҫҮүҰұҲҳҶҷҺһӀӁӂӏӐӑӔӕӖӗӘәӢӣӦӧӨөӮӯӲӳᵃᵇᵈᵉᵍᵏᵐᵒᵖᵗᵘᵛᶜᶠᶻḆḇḌḍḎḏḐḑḖḗḠḡḤḥḦḧḨḩḪḫḲḳḴḵḶḷḸḹḺḻḾḿṀṁṂṃṄṅṆṇṈṉṒṓṘṙṚṛṜṝṞṟṠṡṢṣṬṭṮṯṾṿẀẁẂẃẄẅẎẏẐẑẒẓẔẕẖẗẞẠạẢảẤấẦầẨẩẪẫẬậẮắẰằẲẳẴẵẶặẸẹẺẻẼẽẾếỀềỂểỄễỆệỈỉỊịỌọỎỏỐốỒồỔổỖỗỘộỚớỜờỞởỠỡỢợỤụỦủỨứỪừỬửỮữỰựỲỳỴỵỶỷỸỹ᾽ι῀῁῍῎῏῝῞῟῭΅`´῾‒–—―‗‘’‚‛“”„†‡•…‰′″‹›‼‽‾ⁿₔ₡₣₤₦₧₩₫€₮₱₲₴₵₸₹₺₽℅ℓ№™Ω℮⅓⅔⅛⅜⅝⅞←↑→↓↔↕↨∂∆∏∑−∕∙√∞∟∩∫≈≠≡≤≥⌂⌐⌠⌡─│┌┐└┘├┤┬┴┼═║╒╓╔╕╖╗╘╙╚╛╜╝╞╟╠╡╢╣╤╥╦╧╨╩╪╫╬□▫▬◊○◌◘◦☺☻☼♀♂♪♫ﬁﬂ".chars().collect(),
        };
    }
}

struct State
{
    configuration: Configuration,
    rows: usize,
    random_number_generator: ThreadRng
}

impl State
{
    fn new() -> State
    {
        return State {
            configuration: Configuration::new(),
            rows: 0,
            random_number_generator: thread_rng()
        };
    }

    fn probability_gate(&mut self, probability: f64) -> bool
    {
        return self.random_number_generator.next_f64() < probability;
    }

    fn get_random_character(&mut self) -> char
    {
        return *self.random_number_generator.choose(self.configuration.character_candidates.as_slice()).unwrap();
    }

    fn get_time_to_birth(&mut self) -> f64
    {
        return ((1.0 / (1.0 - self.random_number_generator.next_f64())) * self.configuration.lambda_generate).log2();
    }

    fn generate_speed_value(&mut self) -> f64
    {
        return self.random_number_generator
                   .gen_range(self.configuration.minimal_speed, self.configuration.maximal_speed);
    }

    fn generate_random_characters(&mut self, size: usize) -> Vec<char>
    {
        return (0..size).map(|_| self.get_random_character()).collect();
    }

    fn update_raindrop(&mut self, raindrop: &mut Raindrop, time_elapsed: f64, mutation_probability: f64) -> bool
    {
        let old_integer_position = raindrop.position as usize;

        raindrop.position += raindrop.speed * time_elapsed;

        if raindrop.position - (raindrop.get_size() as f64) < self.rows as f64
        {
            let new_integer_position = raindrop.position as usize;
            let integer_step = new_integer_position - old_integer_position;

            // Do character rotation.

            for i in (integer_step..raindrop.get_size()).rev()
            {
                if self.probability_gate(mutation_probability)
                {
                    raindrop.characters[i] = self.get_random_character();
                }
                else
                {
                    raindrop.characters[i] = raindrop.characters[i - integer_step];
                }
            }

            // Fill remaining positions.

            for i in 0..min(raindrop.get_size(), integer_step)
            {
                raindrop.characters[i] = self.get_random_character();
            }

            return true;
        }
        else
        {
            return false;
        }
    }

    fn update_column(&mut self, column: &mut Vec<Raindrop>, time_elapsed: f64, mutation_probability: f64)
    {
        let mut remove_list = Vec::new();

        for i in 0..column.len()
        {
            if !self.update_raindrop(&mut column[i], time_elapsed, mutation_probability)
            {
                remove_list.push(i);
            }
        }

        remove_by_indexes(column, &remove_list);

        let mut raindrop_birth_time = self.get_time_to_birth();

        while raindrop_birth_time <= time_elapsed
        {
            let speed = self.generate_speed_value();
            let position = speed * (time_elapsed - raindrop_birth_time);
            let size = self.random_number_generator.gen_range(self.configuration.minimal_raindrop_size,
                                                              self.configuration.maximal_raindrop_size);

            if position - (size as f64) < self.rows as (f64)
            {
                column.push(Raindrop::new(position, speed, self.generate_random_characters(size)));
            }

            raindrop_birth_time += self.get_time_to_birth();
        }
    }
}

pub struct Backend
{
    state: State,
    rain_columns: Vec<Vec<Raindrop>>
}

impl Backend
{
    pub fn new() -> Backend
    {
        return Backend {
            state: State::new(),
            rain_columns: Vec::new()
        };
    }

    pub fn get_view(&mut self, columns: usize, rows: usize, time_ellapsed: f64) -> &[Vec<Raindrop>]
    {
        self.rain_columns.resize(columns, Vec::new());
        self.state.rows = rows;

        let mutation_probability = 1.0 - 2.0f64.powf(-time_ellapsed / self.state.configuration.lambda_mutation);

        for rain_column in &mut self.rain_columns
        {
            self.state.update_column(rain_column, time_ellapsed, mutation_probability);
        }

        return self.rain_columns.as_slice();
    }
}

