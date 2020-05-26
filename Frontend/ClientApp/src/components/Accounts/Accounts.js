import React, { useContext, useState, useEffect, useCallback } from 'react';
import { fetchAccounts } from '../../utils/apiCalls';
import Account from '../Account/Account';
import { GlobalContext } from '../../context/GlobalContext';

const Accounts = () => {
  const [data, setData] = useState(null);
  const { accountTypes, setAccounts, processing } = useContext(GlobalContext);

  useEffect(() => {
    fetchAccounts()
      .then(accounts => {
        setData(accounts);
        setAccounts(accounts);
      });
  }, []);

  return (
    <div>
      { 
        !!data && !processing && (
          data.map(account => (
            <Account
              key={account.number}
              label={accountTypes[account.type]}
              {...account}
            />
          ))
        )
      }

    </div>
  );
}
 
export default Accounts;