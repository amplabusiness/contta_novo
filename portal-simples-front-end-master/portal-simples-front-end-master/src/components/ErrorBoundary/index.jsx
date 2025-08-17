import { Component } from 'react';
import PropTypes from 'prop-types';
import { Button } from 'antd';

import { Container } from './styles';

class ErrorBoundary extends Component {
  constructor(props) {
    super(props);

    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error) {
    const { message } = error;
    const isChunkLoadingError = message.toLowerCase().includes('chunk');

    if (isChunkLoadingError) {
      window.location.reload();

      return { hasError: false };
    }

    return { hasError: true };
  }

  render() {
    const { children } = this.props;
    const { hasError } = this.state;

    if (hasError) {
      return (
        <Container>
          <h1>Algo deu errado...</h1>
          <p>
            Por favor, verifique sua conexão com a internet. Caso ela esteja
            normal, espere um momento antes de recarregar a página.
          </p>
          <Button type="primary" onClick={() => window.location.reload()}>
            Recarregar Página
          </Button>
        </Container>
      );
    }

    return children;
  }
}

ErrorBoundary.propTypes = {
  children: PropTypes.node.isRequired,
};

export default ErrorBoundary;
