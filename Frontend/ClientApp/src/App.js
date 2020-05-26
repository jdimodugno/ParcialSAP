import React, { useContext, useEffect } from 'react';
import { Route } from 'react-router';
import { Spinner } from 'reactstrap';
import Layout from './layout/Main';
import Accounts from './components/Accounts/Accounts';
import Account from './components/Account/Account';
import { GlobalContext } from './context/GlobalContext';
import { fetchAccountTypes } from './utils/apiCalls';

import './custom.css'

const App = () => {
  const { loading, setAccountTypes } = useContext(GlobalContext);

  useEffect(() => {
    fetchAccountTypes()
      .then(accountTypes => {
        setAccountTypes(accountTypes);
      })
  }, []);
  
  return (
    <Layout>
      {
        loading ? (
          <Spinner type="grow" color="primary" />
        ) : (
          <>
            <Route exact path='/' component={Accounts} />
            <Route path='/account/:accountId' component={Account} />
          </>
        )
      }
    </Layout>
  )
};

App.displayName = 'App';

export default App;