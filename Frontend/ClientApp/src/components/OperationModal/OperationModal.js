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
  DropdownItem
} from 'reactstrap';
import { GlobalContext } from '../../context/GlobalContext';
import { performOperation } from '../../services/operationService';
import { TRANSFER } from '../../common/operations';

const OperationModal = ({
  operation,
  accountNumber,
  modalOpen,
  toggleModal,
}) => {
  const [enabled, setEnabled] = useState(false);
  const [targetAccountId, setTargetAccountId] = useState(null);
  const [amount, setAmount] = useState(0);
  const { accounts } = useContext(GlobalContext);
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
        (x) => console.log('xxx', x),
      );
    },
    [operation, isTransfer, accountNumber, amount, targetAccountId],
  );

  return (
    <Modal
      isOpen={modalOpen}
      toggle={toggleModal}
    >
      <ModalHeader toggle={toggleModal}>Modal title</ModalHeader>
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
                setEnabled(amount === previousValue)
              }, 250);
            }}
          />
        </InputGroup>
        {
          !!accounts && isTransfer && (
            <UncontrolledDropdown>
              <DropdownToggle caret>
                Cuentas
              </DropdownToggle>
              <DropdownMenu>
                {
                  accounts
                    .filter(a => a.number !== accountNumber)
                    .map(({ label, number }) => (
                      <DropdownItem onClick={() => setTargetAccountId(number)}>{label}</DropdownItem>
                    ))
                }
              </DropdownMenu>
            </UncontrolledDropdown>
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
          disabled={!enabled}
        >
          Confirmar
        </Button>
      </ModalFooter>
    </Modal>
  );
}
 
export default OperationModal;