import PropTypes from 'prop-types';
import { Col } from 'antd';

import { Container, Heading, Body, Line, Label, Value } from './styles';

const Critic = ({ title, critics }) => {
  return (
    <Col xs={24} sm={12}>
      <Container>
        <Heading>
          <h2>{title}</h2>
        </Heading>
        <Body>
          <Line>
            <Label to="">ST</Label>
            <Value>{critics?.st ?? '-'}</Value>
          </Line>
          <Line>
            <Label to="">NCM INEXISTENTE</Label>
            <Value>{critics?.ncm ?? '-'}</Value>
          </Line>
          <Line>
            <Label to="">CFOP</Label>
            <Value>{critics?.cfop ?? '-'}</Value>
          </Line>
          <Line>
            <Label to="">CNAE</Label>
            <Value>{critics?.cnae ?? '-'}</Value>
          </Line>
          <Line>
            <Label to="">ESTOQUE</Label>
            <Value>{critics?.estoque ?? '-'}</Value>
          </Line>
        </Body>
      </Container>
    </Col>
  );
};

Critic.propTypes = {
  title: PropTypes.string.isRequired,
  critics: PropTypes.oneOfType([PropTypes.object, PropTypes.string]),
};

Critic.defaultProps = {
  critics: null,
};

export default Critic;
