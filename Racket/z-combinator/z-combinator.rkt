#lang racket/base

(require rackunit)

(define fact1
  (λ (n)
    (if (zero? n)
        1
        (* (fact1 (sub1 n)) n))))

(define fact2
  (let ([fact-helper (λ (self)
                       (λ (n)
                         (if (zero? n)
                             1
                             (* ((self self) (sub1 n)) n))))])
    (λ (n)
      ((fact-helper fact-helper) n))))

(define fact3
  (let ([fact-helper (λ (self)
                       (λ (n)
                         (if (zero? n)
                             1
                             (* ((self self) (sub1 n)) n))))])
    (fact-helper fact-helper)))

(define fact4
  ((λ (self)
     (λ (n)
       (if (zero? n)
           1
           (* ((self self) (sub1 n)) n))))
   (λ (self)
     (λ (n)
       (if (zero? n)
           1
           (* ((self self) (sub1 n)) n))))))

(define fact5
  (let ([fact-helper (λ (self)
                       (λ (n)
                         (if (zero? n)
                             1
                             (* ((self self) (sub1 n)) n))))])
    ((λ (self)
       (fact-helper self))
     (λ (self)
       (fact-helper self)))))

(define fact6
  (let ([not-y (λ (f)
                 ((λ (self)
                    (f self))
                  (λ (self)
                    (f self))))])
    (not-y (λ (self)
             (λ (n)
               (if (zero? n)
                   1
                   (* ((self self) (sub1 n)) n)))))))

(define fact7
  (let ([not-y (λ (f)
                 ((λ (self)
                    (f (λ (x)
                         (self x))))
                  (λ (self)
                    (f (λ (x)
                         (self x))))))])
    (not-y (λ (self)
             (λ (n)
               (if (zero? n)
                   1
                   (* ((self self) (sub1 n)) n)))))))

(define fact8
  (let ([z (λ (f)
             ((λ (self)
                (f (λ (x)
                     ((self self) x))))
              (λ (self)
                (f (λ (x)
                     ((self self) x))))))])
    (z (λ (f)
         (λ (n)
           (if (zero? n)
               1
               (* (f (sub1 n)) n)))))))

;; Tests.

(for ([fact (list fact1 fact2 fact3 fact4 fact5 fact6 fact7 fact8)])
  (check-eq? (fact 0) 1)
  (check-eq? (fact 1) 1)
  (check-eq? (fact 2) 2)
  (check-eq? (fact 3) 6)
  (check-eq? (fact 4) 24)
  (check-eq? (fact 5) 120)
  (check-eq? (fact 6) 720))
