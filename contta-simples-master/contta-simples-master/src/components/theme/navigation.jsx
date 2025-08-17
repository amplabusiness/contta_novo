import React, { Component } from 'react';
import $ from 'jquery';

import MenuItem from './menuItem';

import logo from '../theme/assets/img/logoAlt.png';
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
    let hrefLink = '#';
    return (
      <nav className="navbar-default navbar-static-side" role="navigation">
        <div className="sidebar-collapse">
          <ul className="nav metismenu" id="side-menu" ref="menu" style={{ zIndex: 4000 }}>
            <li className="nav-header">
              <div className="dropdown profile-element">
                {/**Onde vai a logo da empresa */}
                <img style={{ maxHeight: 170, maxWidth: 170 }} src={logo} alt="Perfil" />
                <span className="block m-t-xs font-bold">CONTTA SISTEMAS</span>
                <a data-toggle="dropdown" className="dropdown-toggle" href={hrefLink}>
                </a>
                <ul className="dropdown-menu animated fadeInRight m-t-xs">
                  <li>
                    <a className="dropdown-item" href="home">
                      Profile
                    </a>
                    {/* </li>
                  <li>
                    <a className="dropdown-item" href="contacts.html">
                      Contacts
                    </a>
                  </li>
                  <li>
                    <a className="dropdown-item" href="mailbox.html">
                      Mailbox
                    </a>
                  </li>
                  <li className="dropdown-divider" />
                  <li>
                    <a className="dropdown-item" href="login.html">
                      Logout
                    </a> */}
                  </li>
                </ul>
              </div>
            </li>
            {/* menu */}
            <MenuItem path="/" icon="home" label="Home" />
            <MenuItem path="/cadastroUser" label="Cadastro Usuario" />
          </ul>
        </div>
      </nav>
    );
  }
}

export default Navigation;
