import React from 'react';
import { Route } from 'react-router';
import Layout from './layout/Main';
import Home from './components/Home/Home';
import Weather from './components/Weather/Weather';

import './custom.css'

const App = () => (
  <Layout>
    <Route exact path='/' component={Home} />
    <Route exact path='/weather' component={Weather} />
  </Layout>
);

App.displayName = 'App';

export default App;