import React, { useContext, useState, useEffect, useCallback } from 'react';
import { Button } from 'reactstrap';
import { fetchAccounts, createAccount } from '../../utils/apiCalls';
import AccountCard from '../AccountCard/AccountCard';
import { GlobalContext } from '../../context/GlobalContext';

import './Accounts.css'

const Accounts = () => {
  const [data, setData] = useState(null);
  const { accountTypes, setAccounts, processing } = useContext(GlobalContext);

  const generateAccountPayload = (isChecking) => ({
    Type: isChecking ? 0 : 1,
    Alias: `${isChecking ? 'CC' : 'CA'}.CLIENT.${Math.random().toString(36).substring(7).toUpperCase()}`,
    Overdraft: isChecking ? Math.ceil(Math.random() * 100 * 5 * 10) : 0,
  })

  const fetchAccountsCallback = useCallback(
    () => {
      fetchAccounts()
        .then(accounts => {
          setData(accounts);
          setAccounts(accounts);
        });
    },
    [setAccounts, setData],
  );

  const createNewAccount = useCallback(
    (checking) => {
      const accountPayload = generateAccountPayload(checking);
      createAccount(accountPayload)
        .then(() => {
          fetchAccountsCallback();
        });
    },
    [fetchAccountsCallback],
  );

  useEffect(() => {
    fetchAccountsCallback();
  }, []);

  return (
    <div>
      <div className="account-actions">
        <h1> Acciones </h1>
        <Button
          onClick={() => createNewAccount(false)}
        >
          Crear CA
        </Button>
        <Button
          onClick={() => createNewAccount(true)}
        >
          Crear CC
        </Button>
      </div>
      <div className="cards">
        { 
          !!data && !processing && (
            data.map(account => (
              <AccountCard
                key={account.number}
                label={accountTypes[account.type]}
                refreshAccounts={fetchAccountsCallback}
                {...account}
              />
            ))
          )
        }
      </div>
    </div>
  );
}
 
export default Accounts;