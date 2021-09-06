// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x = x * (x+1)

#load "../test.fsx"
Test.test [
    f 1 = 2
    f 0 = 0
    f 2 = 6
    f 4 = 20
    f 10 = 110
]