import React from 'react';

/*
 * Panel
 */
const Panel = ({ title, children, color }) => (
  <div className={`panel panel-${color}`}>
    <div className="panel-heading">
      <strong>{title}</strong>{' '}
    </div>
    <div className="panel-body">{children}</div>
  </div>
);

/*
 * Row
 */
const Row = ({ children, style }) => (
  <div className="row" style={style}>
    {children}
  </div>
);

/*
 * Column
 */
const Column = ({ col, children, style }) => (
  <div className={`col-md-${col}`} style={style}>
    {children}
  </div>
);

/*
 * Form
 */
const Form = ({ handleSubmit, children }) => <form onSubmit={handleSubmit}>{children}</form>;

/*
 * Break Line
 */
const Br = () => <br />;

/*
 * Center
 */
const Center = ({ children }) => <center>{children}</center>;

/*
 * Heading
 */
const H1 = ({ children, props }) => <h1 {...props}>{children}</h1>;
const H2 = ({ children, props }) => <h2 {...props}>{children}</h2>;
const H3 = ({ children, props }) => <h3 {...props}>{children}</h3>;

/*
 * Div
 */
const Div = ({ children, props }) => <div {...props}>{children}</div>;

export { Panel, Row, Column, Form, Br, Center, H1, H2, H3, Div };
