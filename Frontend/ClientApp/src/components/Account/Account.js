import React, { useEffect, useState, useContext } from 'react';
import { Spinner, Table } from 'reactstrap';
import moment from 'moment';
import {
  fetchAccountById,
  fetchDeposits,
  fetchWithdrawals,
  fetchTransfersAsSource,
  fetchTransfersAsTarget,
} from '../../utils/apiCalls';
import { GlobalContext } from '../../context/GlobalContext'
import './Account.css';

const Account = ({
  match,
}) => {
  const [movements, setMovements] = useState(null);
  const [account, setAccount] = useState(null);
  const [deposits, setDeposits] = useState(null);
  const [withdrawals, setWithdrawals] = useState(null);
  const [transfersAsSource, setTransfersAsSource] = useState(null);
  const [transfersAsTarget, setTransfersAsTarget] = useState(null);
  const { accountTypes } = useContext(GlobalContext);

  useEffect(() => {
    const { params : { accountId } } = match;
    fetchAccountById(accountId)
      .then(data => setAccount(data));
    fetchDeposits(accountId)
      .then(data => setDeposits(data));
    fetchWithdrawals(accountId)
      .then(data => setWithdrawals(data));
    fetchTransfersAsSource(accountId)
      .then(data => setTransfersAsSource(data));
    fetchTransfersAsTarget(accountId)
      .then(data => setTransfersAsTarget(data));
  }, [])

  useEffect(() => {
    if (account && deposits && withdrawals && transfersAsSource && transfersAsTarget) {
      const _movements = [
        ...deposits.map(({ id, amount, dateCreated }) => ({
          type: 'Deposito',
          amount,
          id,
          dateCreated: new Date(dateCreated),
        })),
        ...withdrawals.map(({ id, amount, dateCreated }) => ({
          type: 'Extracción',
          amount,
          id,
          dateCreated: new Date(dateCreated),
        })),
        ...transfersAsSource.map(({ id, amount, dateCreated, targetAccountId }) => ({
          type: 'Transferencia Realizada',
          amount,
          id,
          dateCreated: new Date(dateCreated),
          accountId: targetAccountId,
          source: true,
        })),
        ...transfersAsTarget.map(({ id, amount, dateCreated, sourceAccountId }) => ({
          type: 'Transferencia Recibida',
          amount,
          id,
          dateCreated: new Date(dateCreated),
          accountId: sourceAccountId,
        }))
      ].sort((a, b) => a.dateCreated - b.dateCreated);
      setMovements(_movements);
    }
  }, [account, deposits, withdrawals,transfersAsTarget, transfersAsSource])

  return (
    <div>
      {
        !!account && (
          <h1>
            {accountTypes[account.type]}: <b>{account.number}</b>
          </h1>
        )
      }
      <Table hover>
        <thead>
          <tr>
            <th>Código Operación</th>
            <th>Tipo</th>
            <th>Monto</th>
            <th>Cuenta Origen</th>
            <th>Cuenta Destino</th>
            <th>Fecha</th>
          </tr>
        </thead>
        <tbody>
          { 
            !!movements && (
              !!movements.length ? movements.map(({ type, amount, id, dateCreated, accountId, source }) => (
                <tr key={id}>
                  <th scope="row">{ id }</th>
                  <td>{ type }</td>
                  <td>${ amount }</td>
                  <td>{ source ? '' : accountId }</td>
                  <td>{ source ? accountId : '' }</td>
                  <td>{ moment(dateCreated).format('DD/MM/YYYY, h:mm:ss a') }</td>
                </tr>
              )) : (
                <tr>
                  <td>No se registran movimientos</td>
                </tr>
              )
            )
          }
        </tbody>
      </Table>
      {
        !movements && (
          <Spinner type="grow" color="primary" /> 
        ) 
      }
    </div>
  );
}
 
export default Account;