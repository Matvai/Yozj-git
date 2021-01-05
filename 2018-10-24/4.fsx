open System.Collections.Generic
// 1. Нажать Ctrl+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты "ok"

// Почини функцию
let f x y = List.length [1;2;3]

#load "../test.fsx"
Test.test [
    f [] [] = 0
    f [1;2] [1] = 1
    f [1;2;3] [3;4] = 2
    f [0] [5;6] = 1
    f [1;2;3] [4;5;6;7] = 3
]