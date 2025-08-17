import React, { Component } from "react";

export default class Box extends Component {
  render() {
    const { col, title, description, amount, amountDescription, percent } = this.props;
    return (
      <div className={`col-md-${col}`}>
        <div className="ibox float-e-margins">
          <div className="ibox-title">
            <span className="label label-info pull-right">{title}</span>
            <h5>{description}</h5>
          </div>
          <div className="ibox-content">
            <h1 className="no-margins">{amount}</h1>
            <small>{amountDescription}</small>
            <div className="pull-right" >
              <small className="label-percent">{percent}%</small>
            </div>
          </div>
        </div>
      </div>
    );
  }
}
