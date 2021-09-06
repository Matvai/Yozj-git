// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию f
let d = x or y
let f d = d * 2

// Функцию g НЕ ТРОГАТЬ!
let g x y = (f x) + (f y)

#load "../test.fsx"
Test.test [
    g 0 0 = 0
    g 1 0 = 2
    g 3 4 = 14
    g 2 2 = 8
    g 5 3 = 16
]