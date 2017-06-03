#[derive(Clone)]
pub struct Raindrop {
    pub position: f64,
    pub speed: f64,
    pub characters: Vec<char>,
}

impl Raindrop {
    pub fn new(position: f64, speed: f64, characters: Vec<char>) -> Raindrop {
        return Raindrop {
            position: position,
            speed: speed,
            characters,
        };
    }

    pub fn get_size(&self) -> usize {
        return self.characters.len();
    }
}

