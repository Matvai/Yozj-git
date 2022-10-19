const { extend } = require("jquery")
let React = require("react") 
let ReactDOM = require("react-dom") 

let root = document.createElement("div")
document.body.appendChild(root)

let xx = <i className="bg-info">Here</i>

class Counter extends React.Component {
    state = { count: 0 }

    render() {
        return <div>
            <b>The count is: {this.state.count}</b>
            {xx}
            <button className="btn btn-primary" onClick={() => this.setState({ count: this.state.count + 1 })}>Click me!</button>
            <button className="btn btn-primary" onClick={() => this.setState({ count: this.state.count - 1 })}>Click me too!</button>
            <button className="btn btn-primary" onClick={() => this.setState({ count: 0 })}>Reset!</button>
            <button className="btn btn-primary" onClick={() => this.setState({ count: 42 })}>42</button>
        </div>
    }
}

class Yozj extends React.Component {
    render() {
        return <div className="bg-danger"> 
            {Array(10).fill().map(() => <Counter></Counter>)}
            <Murch color="success"></Murch>
            <Murch color="info"></Murch>
        </div>
    }
}

class Murch extends React.Component {
    state = { hello: true }

    render() {
        let hello;
        if (this.state.hello) { hello = "Hello" } else { hello = "Goodbye" }

        return <div className={"bg-" + this.props.color}>
                {hello}
                <button className="btn btn-success" onClick={() => this.setState({ hello: !this.state.hello })}>Toggle</button>
            </div>
    }
}

ReactDOM.render(<Yozj />, root)