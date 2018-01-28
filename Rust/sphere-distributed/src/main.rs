#![feature(nll)]

extern crate rand;

use rand::Rng;

#[derive(Clone, Copy, Debug, PartialEq)]
struct Point {
    x: f64,
    y: f64,
    z: f64,
}

impl Point {
    #[inline(always)]
    fn new(x: f64, y: f64, z: f64) -> Point {
        return Point { x, y, z };
    }

    #[inline(always)]
    fn dot(&self, rhs: &Point) -> f64 {
        return self.x * rhs.x + self.y * rhs.y + self.z * rhs.z;
    }

    #[inline(always)]
    fn norm(&self) -> f64 {
        return (self.x * self.x + self.y * self.y + self.z * self.z).sqrt();
    }

    #[inline(always)]
    fn to_normalized(&self) -> Point {
        let norm_inverted = 1.0 / self.norm();

        return Point::new(
            self.x * norm_inverted,
            self.y * norm_inverted,
            self.z * norm_inverted,
        );
    }
}

// Add.

impl std::ops::Add<Point> for Point {
    type Output = Point;

    #[inline(always)]
    fn add(self, rhs: Point) -> Self::Output {
        return Point::new(self.x + rhs.x, self.y + rhs.y, self.z + rhs.z);
    }
}

impl<'a> std::ops::Add<&'a Point> for Point {
    type Output = Point;

    #[inline(always)]
    fn add(self, rhs: &'a Point) -> Self::Output {
        return Point::new(self.x + rhs.x, self.y + rhs.y, self.z + rhs.z);
    }
}

impl<'a> std::ops::Add<Point> for &'a Point {
    type Output = Point;

    #[inline(always)]
    fn add(self, rhs: Point) -> Self::Output {
        return Point::new(self.x + rhs.x, self.y + rhs.y, self.z + rhs.z);
    }
}

impl<'a, 'b> std::ops::Add<&'b Point> for &'a Point {
    type Output = Point;

    #[inline(always)]
    fn add(self, rhs: &'b Point) -> Self::Output {
        return Point::new(self.x + rhs.x, self.y + rhs.y, self.z + rhs.z);
    }
}

// Sub.

impl std::ops::Sub<Point> for Point {
    type Output = Point;

    #[inline(always)]
    fn sub(self, rhs: Point) -> Self::Output {
        return Point::new(self.x - rhs.x, self.y - rhs.y, self.z - rhs.z);
    }
}

impl<'a> std::ops::Sub<&'a Point> for Point {
    type Output = Point;

    #[inline(always)]
    fn sub(self, rhs: &'a Point) -> Self::Output {
        return Point::new(self.x - rhs.x, self.y - rhs.y, self.z - rhs.z);
    }
}

impl<'a> std::ops::Sub<Point> for &'a Point {
    type Output = Point;

    #[inline(always)]
    fn sub(self, rhs: Point) -> Self::Output {
        return Point::new(self.x - rhs.x, self.y - rhs.y, self.z - rhs.z);
    }
}

impl<'a, 'b> std::ops::Sub<&'b Point> for &'a Point {
    type Output = Point;

    #[inline(always)]
    fn sub(self, rhs: &'b Point) -> Self::Output {
        return Point::new(self.x - rhs.x, self.y - rhs.y, self.z - rhs.z);
    }
}

// Mul.

impl std::ops::Mul<f64> for Point {
    type Output = Point;

    #[inline(always)]
    fn mul(self, rhs: f64) -> Self::Output {
        return Point::new(self.x * rhs, self.y * rhs, self.z * rhs);
    }
}

impl<'a> std::ops::Mul<&'a f64> for Point {
    type Output = Point;

    #[inline(always)]
    fn mul(self, rhs: &'a f64) -> Self::Output {
        return Point::new(self.x * rhs, self.y * rhs, self.z * rhs);
    }
}

impl<'a> std::ops::Mul<f64> for &'a Point {
    type Output = Point;

    #[inline(always)]
    fn mul(self, rhs: f64) -> Self::Output {
        return Point::new(self.x * rhs, self.y * rhs, self.z * rhs);
    }
}

impl<'a, 'b> std::ops::Mul<&'b f64> for &'a Point {
    type Output = Point;

    #[inline(always)]
    fn mul(self, rhs: &'b f64) -> Self::Output {
        return Point::new(self.x * rhs, self.y * rhs, self.z * rhs);
    }
}

//

#[inline(always)]
fn force_by(p0: &Point, p1: &Point) -> Point {
    let direction = p0 - p1;
    let t = (direction.x * direction.x + direction.y * direction.y + direction.z * direction.z)
        .powf(-1.5);

    return Point::new(direction.x * t, direction.y * t, direction.z * t);
}

fn iterate(points: &mut Vec<Point>, buffer: &mut Vec<Point>) -> f64 {
    let mut max_force = 0.0;

    for (i, (p, b)) in points.iter().zip(buffer.iter_mut()).enumerate() {
        let other_points = points[..i].iter().chain(points[i + 1..].iter());
        let force = other_points.fold(Point::new(0.0, 0.0, 0.0), |ref x, ref y| x + force_by(p, y));

        let useful_force = force - p * force.dot(p);

        *b = useful_force;

        max_force = useful_force.norm().max(max_force);
    }

    for (p, b) in points.iter_mut().zip(buffer.iter()) {
        let new_p = (*p + b * 0.0001).to_normalized();

        *p = new_p;
    }

    return max_force;
}

fn main() {
    let mut random = rand::thread_rng();
    const COUNT: i32 = 400;

    let mut points = (0..COUNT)
        .map(|_| {
            let r = &mut random;
            Point::new(r.next_f64(), r.next_f64(), r.next_f64())
        })
        .collect::<Vec<_>>();

    let mut buffer = (0..COUNT)
        .map(|_| Point::new(0.0, 0.0, 0.0))
        .collect::<Vec<_>>();

    let mut max_force = std::f64::INFINITY;

    for _ in 0..100000 {
        max_force = iterate(&mut points, &mut buffer).min(max_force);
    }

    for p in points {
        println!("{{{}, {}, {}}},", p.x, p.y, p.z);
    }

    println!{"Force: {}", max_force};
}
