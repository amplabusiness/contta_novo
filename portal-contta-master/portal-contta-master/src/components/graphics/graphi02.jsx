import React, { PureComponent } from 'react';
import {
  AreaChart, Area, XAxis, YAxis, CartesianGrid, Tooltip,
} from 'recharts';

const data = [
  {
    name: 'JANEIRO', uv: 1500, pv: 2300, amt: 2600,
  },
  {
    name: 'FEVEREIRO', uv: 1500, pv: 2300, amt: 2100,
  },
  {
    name: 'MARÇO', uv: 2490, pv: 4300, amt: 2100,
  },
  {
    name: 'ABRIL', uv: 2490, pv: 4300, amt: 2100,
  },
  {
    name: 'MAIO',uv: 2490, pv: 4300, amt: 2100,
  },
  {
    name: 'JUNHO', uv: 2490, pv: 1300, amt: 1100,
  },
  {
    name: 'JULHO', uv: 2490, pv: 4300, amt: 2100,
  },
  {
    name: 'AGOSTO', uv: 2490, pv: 4300, amt: 2100,
  },
  {
    name: 'SETEMBRO', uv: 2490, pv: 4300, amt: 2100,
  },
  {
    name: 'OUTUBRO', uv: 2490, pv: 4300, amt: 2100,
  },
  {
    name: 'NOBEMBRO', uv: 1490, pv: 2300, amt: 1100,
  },
  {
    name: 'DEZEMBRO', uv: 1500, pv: 1300, amt: 1100,
  },
];

export default class Graplhic02 extends PureComponent {
  static jsfiddleUrl = 'https://jsfiddle.net/alidingling/c1rLyqj1/';

  render() {
    return (
      <AreaChart
        width={1600}
        height={500}
        data={data}
        margin={{
          top: 10, right: 30, left: 0, bottom: 0,
        }}
      >
        <CartesianGrid strokeDasharray="3 3" />
        <XAxis dataKey="name" />
        <YAxis />
        <Tooltip />
        <Area type="monotone" dataKey="uv" stackId="1" stroke="#8884d8" fill="#8884d8" />
        <Area type="monotone" dataKey="pv" stackId="1" stroke="#82ca9d" fill="#82ca9d" />
        <Area type="monotone" dataKey="amt" stackId="1" stroke="#ffc658" fill="#ffc658" />
      </AreaChart>
    );
  }
}
