#lang racket

(require math/matrix)

(define (XYZ-to-xyY X Y Z)
  (let ([t (+ X Y Z)])
    (list (/ X t)
          (/ Y t)
          Y)))

(define (xyY-to-XYZ x y Y)
  (let ([t (/ Y y)])
    (list (* t x)
          Y
          (* t (- 1 x y)))))

(define (XYZ-to-srgb X Y Z)
  (let ([m1 (matrix [[3.2406 -1.5372 -0.4986]
                     [-0.9689 1.8758 0.0415]
                     [0.0557 -0.2040 1.0570]])]
        [m2 (col-matrix [X
                         Y
                         Z])])
    (list (matrix* m1 m2)
          (matrix-map (lambda (c)
                        (if (<= c 0.0031308)
                            (* 12.92 c)
                            (- (* 1.055 (expt c 5/12)) 0.055)))
                      (matrix* m1 m2)))))

(XYZ-to-srgb 0.001094949000 0.000031531040 0.005158320000)

(define (get-line p1 p2)
  (let* ([x1 (first p1)]
         [y1 (second p1)]
         [x2 (first p2)]
         [y2 (second p2)]
         [t (- x1 x2)])
    (list (/ (- y1 y2) t)
          (/ (- (* x1 y2) (* x2 y1)) t))))
(define (get-line-2 a p)
  (list a (- (second p) (* a (first p)))))


(let* ([pr '(64/100 33/100)]
       [pg '(3/10 6/10)]
       [pb '(15/100 6/100)]
       [line-rg (get-line pr pg)]
       [line-rb (get-line pr pb)]
       [line-gb (get-line pg pb)]
       [line-rgr (get-line-2 (/ 1 (first line-rg)) pr)]
       [line-rgg (get-line-2 (first line-rgr) pg)]
       [line-rbr (get-line-2 (/ 1 (first line-rb)) pr)]
       [line-rbb (get-line-2 (first line-rbr) pb)]
       [line-gbg (get-line-2 (/ 1 (first line-gb)) pg)]
       [line-gbb (get-line-2 (first line-gbg) pb)])
  
  (define (get-closest-point p)
    (define (above-line line)
      (> (second p)
         (+ (* (first line)
               (first p))
            (second line))))
    (define (get-pedal-point line)
      (let* ([a (first line)]
             [b (second line)]
             [x [first p]]
             [y (second p)]
             [t (+ 1 (* a a))])
        (list (/ (+ (- (* a b))
                    x
                    (* a y))
                 t)
              (/ (+ b
                    (* a x)
                    (* a a y))
                 t))))
    (if (above-line line-rg)
        (if (above-line line-rgr)
            (if (above-line line-rgg)
                pg
                (get-pedal-point line-rg))
            pr)
        (if (above-line line-rb)
            (if (above-line line-gb)
                (if (above-line line-gbg)
                    pg
                    (if (above-line line-gbb)
                        (get-pedal-point line-gb)
                        pr))
                p)
            (if (above-line line-rbr)
                pr
                (if (above-line line-rbb)
                    (get-pedal-point line-rb)
                    pb)))))
  (void))
