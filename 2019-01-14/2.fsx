// 1. Нажать CTRL+A
// 2. Нажать Alt+Enter
// 3. Убедиться, что все тесты ок

// Почини функцию
let f x = []

#load "../test.fsx"
Test.test [
    f [1;2] = [10;20]
    f [10;22] = [100;220]
    f [3;4;7] = [30;40;70]
]