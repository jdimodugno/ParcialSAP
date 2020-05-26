import React, { useState, useCallback, useContext } from 'react';
import {
  Button,
  Input,
  InputGroup,
  InputGroupAddon,
  Modal,
  ModalHeader,
  ModalBody,
  ModalFooter,
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
  Alert,
} from 'reactstrap';
import { GlobalContext } from '../../context/GlobalContext';
import { performOperation } from '../../services/operationService';
import { TRANSFER } from '../../common/operations';
import './OperationModal.css';

const OperationModal = ({
  alias,
  operation,
  accountNumber,
  modalOpen,
  toggleModal,
}) => {
  const [enabled, setEnabled] = useState(false);
  const [targetAccountId, setTargetAccountId] = useState(null);
  const [targetAccountLabel, setTargetAccountLabel] = useState(null);
  const [amount, setAmount] = useState(0);
  const { accounts } = useContext(GlobalContext);
  const [error, setError] = useState(null);
  const [errorVisible, setErrorVisible] = useState(false);

  const onDismiss = () => {
    setErrorVisible(false);
    setError(null);
  };

  const isTransfer = operation === TRANSFER;

  const perform = useCallback(
    () => {
      const payload = {
        Amount: amount,
        TargetAccountId: isTransfer ? targetAccountId : accountNumber,
      }

      if (isTransfer) payload.SourceAccountId = accountNumber;

      performOperation(
        operation,
        payload,
        () => toggleModal(),
        (err) => {
          setError(err);
          setErrorVisible(true);
        },
      );
    },
    [operation, isTransfer, accountNumber, amount, targetAccountId, toggleModal],
  );

  return (
    <Modal
      isOpen={modalOpen}
      toggle={toggleModal}
    >
      <ModalHeader toggle={toggleModal}>{operation} - {alias}</ModalHeader>
      {
        !!error && (
          <Alert
            color="danger"
            isOpen={errorVisible}
            toggle={onDismiss}
          >
            {error}
          </Alert>
        )
      }
      <ModalBody>
        <InputGroup>
          <InputGroupAddon addonType="prepend">$</InputGroupAddon>
          <Input
            placeholder="Monto"
            min={0}
            type="number"
            step="1"
            value={amount}
            onChange={({ target }) => {
              let previousValue = amount;              
              setAmount(target.value);
              setTimeout(() => {
                setEnabled(amount !== 0 && amount === previousValue)
              }, 250);
            }}
          />
        </InputGroup>
        {
          !!accounts && isTransfer && (
            <>
              <UncontrolledDropdown className="target-account-selector">
                <DropdownToggle caret>
                  Cuentas
                </DropdownToggle>
                <DropdownMenu>
                  {
                    accounts
                      .filter(a => a.number !== accountNumber)
                      .map(({ label, number }) => (
                        <DropdownItem onClick={() => {
                          setTargetAccountId(number);
                          setTargetAccountLabel(label);
                        }}>{label}</DropdownItem>
                      ))
                  }
                </DropdownMenu>
              </UncontrolledDropdown>
              {
                !!targetAccountLabel && (
                  <span className="target-account-alias">
                    Destino: <b>{ targetAccountLabel }</b> 
                  </span>
                )
              }
            </>
          )
        }
      </ModalBody>
      <ModalFooter>
        <Button
          color="secondary"
          onClick={toggleModal}
        >
          Cancel
        </Button>
        <Button
          color="primary"
          onClick={perform}
          disabled={(amount == 0 && !enabled) || (isTransfer && !targetAccountId)}
        >
          Confirmar
        </Button>
      </ModalFooter>
    </Modal>
  );
}
 
export default OperationModal;