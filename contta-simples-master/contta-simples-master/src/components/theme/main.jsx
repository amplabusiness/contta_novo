import React, { Component } from 'react';
import $ from 'jquery';

import Progress from './progress';
import Navigation from './navigation';
import TopHeader from './topHeader';
import Footer from './footer';
import { correctHeight, detectBody } from './helpers/helpers';
import { Router } from 'react-router-dom';
import history from '../../helpers/history';

// CSS and JS
import './assets/dependencies';

// Redux
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

// Routes
import Routes from '../../routes';

class Main extends Component {
  componentDidMount() {
    $(window).bind('load resize', function () {
      correctHeight();
      detectBody();
    });
  }

  render() {
    return (
      <div id="wrapper">
        <Router history={history}>
          <div>
            <Progress />
            <Navigation />
            <div id="page-wrapper" className="gray-bg">
              <TopHeader />
              <Routes />
              <Footer />
            </div>
          </div>
        </Router>
      </div>
    );
  }
}

export default Main;
