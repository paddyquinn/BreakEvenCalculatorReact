import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  constructor(props) {
    super(props);
    this.state = { odds: '0', breakEvenPercentage: '' };
    this.handleChange = this.handleChange.bind(this);
    this.handleClick = this.handleClick.bind(this);
  }

  render() {
    return (
      <div>
        <label>
          Odds:
          <input type="text" value={this.state.odds} onChange={this.handleChange} />
        </label>
        <button onClick={this.handleClick}>Calculate!</button>
        <br></br>
        <label>
          Break-Even Percentage:
          <p>{this.state.breakEvenPercentage}</p>
        </label>
       </div>
    )
  }

  handleChange(event) {
    this.setState({ odds: event.target.value })
  }

  async handleClick() {
    const response = await fetch(`breakevencalculator?odds=${encodeURIComponent(this.state.odds)}`);
    if (response.status === 200) {
      const data = await response.json();
      const breakEvenPercentage = parseFloat(data.breakEvenPercentage)*100;
      this.setState({ odds: this.state.odds, breakEvenPercentage: `${breakEvenPercentage.toFixed(2)}%` });
    } else {
      this.setState({ odds: this.state.odds, breakEvenPercentage: 'Invalid odds!' });
    }
  }
}
