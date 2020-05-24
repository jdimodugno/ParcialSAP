import React from 'react';
import { Container } from 'reactstrap';
import NavMenu from '../components/NavMenu/NavMenu';

const Main = ({ children }) => (
  <div>
    <NavMenu />
    <Container>
      { children }
    </Container>
  </div>
);

Main.displayName = 'MainLayout';
 
export default Main;
