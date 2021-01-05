// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию f
let f a b x = 0

// Функцию g не трогать!
let g x = x * 2

// Функцию h тоже не трогать!
let h x = x + 5

#load "../test.fsx"
Test.test [
    f g h 10 = 5   //
    f g h 20 = 15
    f g h 0 = -5  //
    f g h 5 = 0
    f h g 0 = 5  //
    f h g 2 = 3
]
