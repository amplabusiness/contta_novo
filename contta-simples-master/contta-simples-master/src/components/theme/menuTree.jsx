import React from "react";

const hrefLink = "#";

export default props => (
  <li>
    <a href={hrefLink}>
      <i className={`fa fa-${props.icon}`} />
      <span className="nav-label">{props.label}</span>
      <span className="fa arrow" />
    </a>
    <ul className="nav nav-second-level collapse">{props.children}</ul>
  </li>
);
