// 1. Нажать CTRL+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты ок

// Это можно запустить и посмотреть, что получается:
let add5 x = x + 5
let list1 = [1;2;3]
let list2 = List.map add5 list1

// Почини функцию
let f x = []

#load "../test.fsx"
Test.test [
    f [1;2] = [6;7]
    f [10;22] = [15;27]
    f [3;4;7] = [8;9;12]
]