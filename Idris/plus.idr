total plusAssociativity : (m, n, o : Nat) -> plus (plus m n) o = plus m (plus n o)
plusAssociativity Z n o = Refl
plusAssociativity (S m) n o = cong (plusAssociativity m n o)

total plusCommutativity : (m, n : Nat) -> plus m n = plus n m
plusCommutativity Z Z = Refl
plusCommutativity Z (S n) = cong (plusCommutativity Z n)
plusCommutativity (S Z) Z = Refl
plusCommutativity (S Z) (S n) = cong (plusCommutativity (S Z) n)
plusCommutativity (S (S m)) n = trans (trans (cong (plusCommutativity (assert_smaller (S (S m)) (S m)) n))
                                             (sym (plusAssociativity (S Z) n (S m))))
                                      (trans (plusAssociativity (S Z) n (S m))
                                             (trans (cong {f = \x => plus x (S m)}
                                                          (plusCommutativity (assert_smaller (S (S m)) (S Z)) n))
                                                    (plusAssociativity n (S Z) (S m))))
