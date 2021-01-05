// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x = x * 2

#load "../test.fsx"
Test.test [
    f 5 = 10
    f 3 = 6
    f 1 = 2
    f 0 = 0
    f 8 = 16
]