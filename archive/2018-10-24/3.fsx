// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию f
let f x = x

// Функцию g НЕ ТРОГАТЬ!
let g r t = if f r < f t then r else t

#load "../test.fsx"
Test.test [
    g 0 0 = 0
    g 1 0 = 1
    g 3 4 = 4
    g 2 2 = 2
    g 5 3 = 5
]