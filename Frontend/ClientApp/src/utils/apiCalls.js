import axios from 'axios';

export const fetchAccountTypes = () => {
  return axios.get(`${process.env.REACT_APP_API_URL}/accounts/types`)
    .then(res => res.data )
}

export const fetchAccounts = () => {
  return axios.get(`${process.env.REACT_APP_API_URL}/accounts`)
    .then(res => res.data )
}

export const fetchAccountById = (accountId) => {
  return axios.get(`${process.env.REACT_APP_API_URL}/accounts/${accountId}`)
    .then(res => res.data )
}

export const fetchDeposits = (accountId) => {
  return axios.get(`${process.env.REACT_APP_API_URL}/deposits/${accountId}`)
    .then(res => res.data )
}

export const fetchWithdrawals = (accountId) => {
  return axios.get(`${process.env.REACT_APP_API_URL}/withdrawals/${accountId}`)
    .then(res => res.data )
}

export const fetchTransfersAsSource = (accountId) => {
  return axios.get(`${process.env.REACT_APP_API_URL}/transfers/source/${accountId}`)
    .then(res => res.data )
}

export const fetchTransfersAsTarget = (accountId) => {
  return axios.get(`${process.env.REACT_APP_API_URL}/transfers/target/${accountId}`)
    .then(res => res.data )
}

export const createAccount = (payload) => {
  return axios.post(`${process.env.REACT_APP_API_URL}/accounts`, payload)
    .then(res => res.data )
    .catch(err => ({ err }))
}

export const createDeposit = (payload) => {
  return axios.post(`${process.env.REACT_APP_API_URL}/deposits`, payload)
    .then(res => res.data )
    .catch(err => ({ err }))
}

export const createWithdrawal = (payload) => {
  return axios.post(`${process.env.REACT_APP_API_URL}/withdrawals`, payload)
    .then(res => res.data )
    .catch(err => ({ err }))
}
export const createTransfer = (payload) => {
  return axios.post(`${process.env.REACT_APP_API_URL}/transfers`, payload)
    .then(res => res.data )
    .catch(err => ({ err }))
}