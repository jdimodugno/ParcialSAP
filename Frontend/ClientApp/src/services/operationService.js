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
          .then(data => successHandler(data))
          .catch(err => errorHandler(err));
      case WITHDRAW:
        return createWithdrawal(payload)
          .then(data => successHandler(data))
          .catch(err => errorHandler(err));
      case TRANSFER:
        return createTransfer(payload)
          .then(data => successHandler(data))
          .catch(err => errorHandler(err));
      default:
        throw new Error('Invalid operation');
    }
  } catch (err) {
    errorHandler(err)
  }
}