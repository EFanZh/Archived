(define (greatest-common-divisor a b)
  (if (= b 0)
      a
      (greatest-common-divisor b (remainder a b))))

(greatest-common-divisor 72 108)
