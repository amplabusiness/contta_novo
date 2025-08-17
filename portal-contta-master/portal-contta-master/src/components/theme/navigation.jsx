import React, { Component } from 'react';
import $ from 'jquery';

// import logo from '../../images/logo.png';

import MenuItem from './menuItem';
// import MenuTree from './menuTree';

import { smoothlyMenu } from './helpers/helpers';

class Navigation extends Component {
  componentWillUpdate(nextProps, nextState) {
    $('body').toggleClass('mini-navbar');
    smoothlyMenu();
  }

  componentDidMount() {
    const { menu } = this.refs;
    $(function () {
      $(menu).metisMenu({
        toggle: true,
      });
    });
  }

  render() {
    return (
      <nav className="navbar-default navbar-static-side" role="navigation">
        <div className="sidebar-collapse">
          <ul className="nav metismenu" id="side-menu" ref="menu">
            <li className="nav-header skin-1">
              <div className="dropdown profile-element">{/* <img src={logo} alt="logo" /> */}</div>
              <div className="logo-element">
                {/* <img style={{ width: '60%' }} src={logo} alt="logo" /> */}
              </div>
            </li>
            {/* menu */}
            <MenuItem path="/usuario" icon="users" label="Usuários" />
          </ul>
        </div>
      </nav>
    );
  }
}

export default Navigation;
