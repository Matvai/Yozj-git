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

class Square extends React.Component { 
    render() {
        let cellColor
        if (listContainsCoords(this.props.liveCells, this.props.coords))
            { cellColor = "bg-danger" } 
            else { cellColor = "bg-white" }

        return <div 
            style={{ width: "50px", height: "50px",}} 
            className={cellColor + " border"}>
        </div>
    }
}

class Row extends React.Component {
    render () {
        return <div className="d-flex">
            {array10.fill().map((_, i) => <Square 
                liveCells={this.props.liveCells}
                coords={{ x: i, y: this.props.row }}>
            </Square>)}
        </div>
    }
}

class Grid extends React.Component {
    render () {
        return <div>
            {array10.fill().map((_, i) => <Row 
                liveCells={this.props.liveCells}
                row={i}>
            </Row>)}
        </div>
    }
}

class Facts extends React.Component {
    state = {value: "type any fact here", text: "click for facts", liveCells: [ {x: 1, y: 3}]}
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
            <Grid
                liveCells={this.state.liveCells}>
            </Grid>
        </div>
    }
}

const root = ReactDOM.createRoot(document.getElementById('root'))
root.render(<Facts />)