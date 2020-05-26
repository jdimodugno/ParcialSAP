import React, { useState, createContext, useCallback } from 'react';

export const GlobalContext = createContext({
  loading: false,
  setLoading: () => {},
  processing: false,
  setProcessing: () => {},
  accountTypes: null,
  setAccountTypes: () => {},
  accounts: null,
  setAccounts: () => {},
});

export const GlobalProvider = ({ children }) => {
  const [loading, setLoading] = useState(true);
  const [processing, setProcessing] = useState(false);
  const [accountTypes, setAccountTypes] = useState(null);
  const [accounts, setAccounts] = useState(null);

  const notify = useCallback(
    (notifier, action) => {
      notifier(true);
      action();
      notifier(false);
    }, [],
  )

  const extendedSetAccountTypes = (data) => notify(
    setLoading, 
    () => {
      const accountTypesEnum = data.reduce((acc, current) => {
        const { value, description } = current;
        return { ...acc, [value]: description};
      }, {});
      setAccountTypes(accountTypesEnum);
    }
  )

  const extendedSetAccounts = (data) => notify(
    setProcessing, 
    () => {
      const minimalAccounts = data.map((account) => {
        const { number, type, alias } = account;
        return { number, label: `${ accountTypes[type] }: ${alias}` };
      });
      setAccounts(minimalAccounts);
    }
  )

  return (
    <GlobalContext.Provider value={{
      loading,
      setLoading,
      processing,
      setProcessing,
      accountTypes,
      setAccountTypes: extendedSetAccountTypes,
      accounts,
      setAccounts: extendedSetAccounts,
    }}>
      {children}
    </GlobalContext.Provider>
  )
};
