// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x y = x * y + y

#load "../test.fsx"
Test.test [
    f 1 2 = 4
    f 0 6 = 6
    f 2 3 = 9
    f 10 10 = 110
    f 1 1 = 2
]