import React, { useCallback, useState } from 'react';
import { Link } from 'react-router-dom';
import {
  Card, CardBody, CardTitle, CardSubtitle, CardText, Button, Badge
} from 'reactstrap';
import moment from 'moment';
import OperationModal from '../OperationModal/OperationModal';
import { DEPOSIT, WITHDRAW, TRANSFER } from '../../common/operations';
import './AccountCard.css';

const AccountCard = ({
  label,
  alias,
  number,
  overdraft,
  dateCreated,
  balance,
  ...props
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
            <br />
            <span className="alias">
              <b>{alias}</b>
              <Link to={`/account/${number}`}>Ver Movimientos</Link>
            </span>
          </CardTitle>
          <CardSubtitle>
            Creada: { moment(dateCreated).format('DD/MM/YYYY h:mm:ss a') }
          </CardSubtitle>
          <CardText>
            Balance: <b>${balance}</b>
            <br />
            Descubierto: <b>${overdraft}</b>
          </CardText>
          <div className="actions">
            <Button onClick={() => toggleModal(DEPOSIT)}>Depositar</Button>
            <Button onClick={() => toggleModal(WITHDRAW)}>Extraer</Button>
            <Button onClick={() => toggleModal(TRANSFER)}>Transferir</Button>
          </div>
        </CardBody>
      </Card>
      <OperationModal
        alias={alias}
        accountNumber={number}
        modalOpen={modalOpen && !!operation}
        operation={operation}
        toggleModal={toggleModal}
        {...props}
      />
    </>
  );
}
 
export default AccountCard;