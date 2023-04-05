import React from 'react'
import ReactDOM from 'react-dom'

class Textbox extends React.Component {
    render() {
        return (
           <input
               type="text"
               value={this.props.value}
               onChange={this.props.handleChange}
            />
        )
   
    }
}

class Button extends React.Component {
    render() {
        return <button
            onClick={this.props.handleChange}>{this.props.text}
        </button>
    }
}

class Facts extends React.Component {
    state = {value: "type any fact here", text: "click for facts"}
    render() {
        return <div>
            <Textbox
                handleChange = {x => this.setState({value: x.target.value})}
                value = {this.state.value}>
            </Textbox>
            <Button
                text = {this.state.text}
                handleChange = {_ => this.setState({text: this.state.value})}>
            </Button>
        </div>
    }
}

const root = ReactDOM.createRoot(document.getElementById('root'))
root.render(<Facts />)