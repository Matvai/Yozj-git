// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x y = if x > y then y else x

#load "../test.fsx"
Test.test [
    f 0 0 = 0
    f 5 6 = 5
    f 2 8 = 2
    f 9 1 = 1
    f 25 10 = 10
]