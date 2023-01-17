const { extend } = require("jquery")
let React = require("react") 
let ReactDOM = require("react-dom") 
const { toggleCell } = require("./theBrain")
const { nextStep } = require("./theBrain")

let root = document.createElement("div")
document.body.appendChild(root)

let xx = <i className="bg-info">Here</i>

function listContainsCoords (list, coords) {
    return list.findIndex ((a) => a.x == coords.x && a.y == coords.y) >= 0
}

class Game extends React.Component {
    state = { 
        liveCells: [ {x: 1, y: 3}, {x: 2, y: 4}, {x: 3, y: 4}, {x: 4, y: 4}, {x: 5, y: 3}, {x: 2, y: 1}, {x: 4, y: 1} ], 
        cellColor: "bg-danger"
    }

    render() {
        return <div className="bg-white"> 
            <Grid 
                liveCells={this.state.liveCells}
                cellColor={this.state.cellColor}
                onLiveCellsChanged={coords => this.setState({ liveCells: (toggleCell(coords, this.state.liveCells))})}></Grid>
            <NextStep liveCells={this.state.liveCells} onNextStep={() => this.setState ({ liveCells: nextStep(this.state.liveCells)})}></NextStep>
            <button className="btn btn-success" onClick={() => this.setState ({ liveCells: []})}>Clear</button>
            <ToggleCellColor 
                onCellColorChanged={() => {
                    switch(this.state.cellColor) {
                        case "bg-danger": this.setState({ cellColor: "bg-warning" }); break;
                        case "bg-warning": this.setState({ cellColor: "bg-success" }); break;
                        case "bg-success": this.setState({ cellColor: "bg-primary" }); break;
                        default: this.setState({ cellColor: "bg-danger" })
                    }
                }}
            >                
            </ToggleCellColor>
        </div>
    }
}

class ToggleCellColor extends React.Component {
    render() {
        return <button 
            className="btn btn-success" 
            onClick={this.props.onCellColorChanged}
        >
            Toggle Cell Color
        </button>
    }
}

// {x => this.props.f(x)} == {this.props.f}

class Square extends React.Component { 
    render() {
        let cellColor
        if (listContainsCoords(this.props.liveCells, this.props.coords))
            { cellColor = this.props.cellColor } 
            else { cellColor = "bg-white" }

        return <div 
            style={{ width: "50px", height: "50px",}} 
            className={cellColor + " border"} 
            onClick={() => this.props.onLiveCellsChanged(this.props.coords)}>
        </div>
    }
}

let array10 = Array(10)

class Row extends React.Component {
    render () {
        return <div className="d-flex">
            {array10.fill().map((_, i) => <Square 
                liveCells={this.props.liveCells}
                cellColor={this.props.cellColor}
                coords={{ x: i, y: this.props.row }}
                onLiveCellsChanged={this.props.onLiveCellsChanged}>
            </Square>)}
        </div>
    }
}

class Grid extends React.Component {
    render () {
        return <div>
            {array10.fill().map((_, i) => <Row 
                liveCells={this.props.liveCells}
                cellColor={this.props.cellColor}
                row={i}
                onLiveCellsChanged={this.props.onLiveCellsChanged}>
            </Row>)}
        </div>
    }
}

class NextStep extends React.Component {
    render () {
        return <button 
            className="btn btn-success" 
            onClick={this.props.onNextStep}>
            Next step
        </button>
    }
}

ReactDOM.render(<Game />, root)