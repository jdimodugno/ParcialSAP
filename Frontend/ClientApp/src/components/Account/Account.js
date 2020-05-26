import React, { useCallback, useState } from 'react';
import {
  Card, CardBody, CardTitle, CardSubtitle, CardText, Button, Badge
} from 'reactstrap';
import OperationModal from '../OperationModal/OperationModal';
import { DEPOSIT, WITHDRAW, TRANSFER } from '../../common/operations';

const Account = ({
  type,
  label,
  number,
  overdraft,
  dateCreated,
  balance,
}) => {
  const [modalOpen, setModalOpen] = useState(false);
  const [operation, setOperation] = useState(null);

  const toggleModal = useCallback(
    (operationKey) => {
      if (!modalOpen) setOperation(operationKey);
      setModalOpen(!modalOpen);
      if (!modalOpen && !operationKey) setOperation(null);
    }, [modalOpen],
  );

  return ( 
    <>
      <Card>
        <CardBody>
          <CardTitle>
            <Badge>{label}</Badge>
            &nbsp; Número: {number}
          </CardTitle>
          <CardSubtitle>
            Creada: {dateCreated}
          </CardSubtitle>
          <CardText>
            Balance: {balance} || Descubierto: {overdraft}
          </CardText>
          <Button onClick={() => toggleModal(DEPOSIT)}>Depositar</Button>
          <Button onClick={() => toggleModal(WITHDRAW)}>Extraer</Button>
          <Button onClick={() => toggleModal(TRANSFER)}>Transferir</Button>
        </CardBody>
      </Card>
      <OperationModal
        accountNumber={number}
        modalOpen={modalOpen && !!operation}
        operation={operation}
        toggleModal={toggleModal}
      />
    </>
  );
}
 
export default Account;