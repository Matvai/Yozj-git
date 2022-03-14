let number = 5
let anotherNumber = 3.1415
let string = "foo"
let array = [1,2,"foo","bar",42.5]
let object = { one: "1", two: "2", three: [1,2,3] }
let n = null
let u = undefined

console.log(typeof "number")
console.log(typeof number)
console.log(typeof (typeof true))

let s1 = "abcd"
let s2 = 'And he said "Hellouw" and I waved at him'

console.log(typeof f(4.2))
// console.log(f(bogus))
object["one"] = "gooogoo"
console.log(object["one"])

console.log(object)
object['hhh'] = 55
console.log(object)


function f(x) { 
    if (x < 5) 
        return "murch krokodil"
    else 
        return 42
}