import {
  DEPOSIT,
  WITHDRAW,
  TRANSFER,
} from '../common/operations';
import {
  createDeposit,
  createWithdrawal,
  createTransfer
} from '../utils/apiCalls';

export const performOperation = (operationKey, payload, successHandler, errorHandler) => {
  try {
    switch (operationKey) {
      case DEPOSIT:
        return createDeposit(payload)
          .then(response => {
            if(response.error) throw new Error(response.error);
            successHandler()
          })
          .catch(err => errorHandler(err.message));
      case WITHDRAW:
        return createWithdrawal(payload)
          .then(response => {
            if(response.error) throw new Error(response.error);
            successHandler()
          })
          .catch(err => errorHandler(err.message));
      case TRANSFER:
        return createTransfer(payload)
          .then(response => {
            if(response.error) throw new Error(response.error);
            successHandler()
          })
          .catch(err => errorHandler(err.message));
      default:
        throw new Error('Invalid operation');
    }
  } catch (err) {
    errorHandler(err)
  }
}