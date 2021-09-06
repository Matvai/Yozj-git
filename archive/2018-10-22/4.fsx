// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x = List.length x

#load "../test.fsx"
Test.test [
    f [2] = 1
    f [6;7] = 2
    f [true; false; false] = 3
    f [] = 0
    f ["a";"b";"a";"a";"b";"b";"c"] = 7
]