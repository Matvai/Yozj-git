// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x y = x + y + y

#load "../test.fsx"
Test.test [
    f 1 2 = 5
    f 0 6 = 12
    f 2 3 = 8
    f 10 10 = 30
    f 1 1 = 3
]