#lang typed/racket

(([name . "Visual Studio Dark"]
  [general ([default ([background (30 30 30)]
                      [foreground (220 220 220)])]
            [selected ([background (51 153 255 102)])]
            [selected-inactive ([background . (86 86 86)])]
            [line-number ([background (30 30 30)]
                            [foreground (43 145 175)])]
            [visible-white-space . ([foreground (20 72 82)])]
            [keyword ([foreground (86 156 214)])]
            [comment ([foreground (87 166 74)])]
            [number ([foreground (181 206 168)])]
            [string ([foreground (214 157 133)])]
            [error ([foreground (216 80 80)])])])
