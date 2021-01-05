// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x = x * x

#load "../test.fsx"
Test.test [
    f 1 = 1
    f 0 = 0
    f 2 = 4
    f 6 = 36
]