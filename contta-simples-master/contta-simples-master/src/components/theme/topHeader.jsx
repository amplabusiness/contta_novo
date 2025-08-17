import React, { Component } from 'react';
import { Button, notification } from 'antd';
import $ from 'jquery';

import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { smoothlyMenu } from './helpers/helpers';

class TopHeader extends Component {
  toggleNavigation(e) {
    e.preventDefault();
    $('body').toggleClass('mini-navbar');
    smoothlyMenu();
  }

  openNotification = () => {
    const key = `open${Date.now()}`;
    const btn = (
      <Button
        type="primary"
        onClick={() => {
          notification.close(key);
        }}>
        Sim, eu tenho certeza
      </Button>
    );
    notification.open({
      message: 'Saída',
      description: 'Tem certeza que deseja sair?.',
      btn,
      key,
    });
  };

  render() {
    let hrefLink = '#';
    return (
      <div className="row border-bottom">
        <nav className="navbar navbar-static-top" role="navigation" style={{ marginBottom: 0 }}>
          <div className="navbar-header">
            <a
              className="navbar-minimalize minimalize-styl-2 btn btn-primary"
              onClick={(e) => this.toggleNavigation(e)}
              href={hrefLink}>
              <i className="fa fa-bars" />{' '}
            </a>
            {/* <form role="search" className="navbar-form-custom" method="post" action="#">
              <div className="form-group">
                <input
                  type="text"
                  placeholder="Faça sua pesquisa..."
                  className="form-control"
                  name="top-search"
                  id="top-search"
                />
              </div>
            </form> */}
          </div>
          <ul className="nav navbar-top-links navbar-right">
            <li>
              <a onClick={() => this.openNotification()} href={hrefLink}>
                <i className="fa fa-sign-out" /> Sair
              </a>
            </li>
          </ul>
        </nav>
      </div>
    );
  }
}

export default TopHeader;
