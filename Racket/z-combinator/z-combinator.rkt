#lang racket/base

(require rackunit)

(define fib1 (λ (n)
               (if (zero? n)
                   1
                   (* (fib1 (sub1 n)) n))))

(define fib2 (let ([fib-helper (λ (self)
                                 (λ (n)
                                   (if (zero? n)
                                       1
                                       (* ((self self) (sub1 n)) n))))])
               (λ (n)
                 ((fib-helper fib-helper) n))))

(define fib3 (let ([fib-helper (λ (self)
                                 (λ (n)
                                   (if (zero? n)
                                       1
                                       (* ((self self) (sub1 n)) n))))])
               (fib-helper fib-helper)))

(define fib4 ((λ (self)
                (λ (n)
                  (if (zero? n)
                      1
                      (* ((self self) (sub1 n)) n))))
              (λ (self)
                (λ (n)
                  (if (zero? n)
                      1
                      (* ((self self) (sub1 n)) n))))))

(define fib5 (let ([fib-helper (λ (self)
                                 (λ (n)
                                   (if (zero? n)
                                       1
                                       (* ((self self) (sub1 n)) n))))])
               ((λ (self)
                  (fib-helper self))
                (λ (self)
                  (fib-helper self)))))

(define fib6 (let ([not-y (λ (f)
                            ((λ (self)
                               (f self))
                             (λ (self)
                               (f self))))])
               (not-y (λ (self)
                        (λ (n)
                          (if (zero? n)
                              1
                              (* ((self self) (sub1 n)) n)))))))

(define fib7 (let ([not-y (λ (f)
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

(define fib8 (let ([z (λ (f)
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

(for ([fib (list fib1 fib2 fib3 fib4 fib5 fib6 fib7 fib8)])
  (check-eq? (fib 0) 1)
  (check-eq? (fib 1) 1)
  (check-eq? (fib 2) 2)
  (check-eq? (fib 3) 6)
  (check-eq? (fib 4) 24)
  (check-eq? (fib 5) 120)
  (check-eq? (fib 6) 720))
