let theBrain = require ("./theBrain")

function counter() {return { count: 0, inc: function() { this.count = this.count + 1 } }} // <--- constructor

let w = counter()
let x = counter()
let y = counter()
let z = counter()

console.log(x.count)
x.inc()
console.log(x.count)
x.inc()
console.log(x.count)
x.inc()
x.inc()
console.log(x.count)

function superCounter() { // Inheritance. superCounter "inherits" from counter. counter is "ancestor" of superCounter.
    let u = counter()
    u.dec = function() { this.count = this.count - 1 }
    return u
}

let v = superCounter()
v.inc()
v.inc()
v.inc()
console.log(v.count)
v.dec()
v.dec()
v.dec()
v.dec()
v.dec()
console.log(v.count)

class Counter {
    count = 0
    inc() { this.count = this.count + 1 } // <--- method (not function)
}

let a = new Counter()
console.log(a.count)
a.inc()
a.inc()
a.inc()
a.inc()
console.log(a.count)
a.inc()
console.log(a.count)

class SuperCounter extends Counter {
    dec() { this.count = this.count - 1 }
}

class SuperDuperCounter extends SuperCounter {
    inct() { this.count = this.count + 0.1}
    dect() { this.count = this.count - 0.1}
    reset() {this.count = 0}
}

let b = new SuperDuperCounter()
b.inct()
b.inc()
b.inct()
b.inc()
b.inc()
b.inc()
console.log(b.count)
b.dect()
b.inc()
console.log(b.count)