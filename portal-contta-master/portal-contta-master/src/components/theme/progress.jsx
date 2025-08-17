import { Component } from "react";
import Pace from "./assets/js/pace/pace";

export default class Progress extends Component {
  componentDidMount() {
    Pace.start();
  }
  render() {
    return null;
  }
}
