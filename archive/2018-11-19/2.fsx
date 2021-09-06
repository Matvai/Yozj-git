// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию f
let f a x = 0

// Функцию g не трогать!
let g x = x + 1

#load "../test.fsx"
Test.test [
    f g 10 = 22
    f g 20 = 42
    f g 0 = 2
    f g 5 = 12
    f g 6 = 14
]