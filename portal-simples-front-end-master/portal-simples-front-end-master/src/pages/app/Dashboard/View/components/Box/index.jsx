import PropTypes from 'prop-types';
import { Col } from 'antd';
import { FiDollarSign, FiPercent } from 'react-icons/fi';
import { IoCalculator } from 'react-icons/io5';

import { Container, Icon, ValuesRow, Value, Label } from './styles';

const ICONS = {
  money: <FiDollarSign size={24} />,
  calculator: <IoCalculator size={24} />,
  percent: <FiPercent size={24} />,
};

const Box = ({ size, icon, iconColor, values, extraContent }) => {
  return (
    <Col xs={24} sm={12} xl={size}>
      <Container>
        <Icon color={iconColor}>{ICONS[icon]}</Icon>
        <ValuesRow>
          {values.length > 0 &&
            values.map(item => (
              <div key={item.label}>
                <Value color={item.color || '#252525'}>{item.value}</Value>
                <Label>{item.label}</Label>
              </div>
            ))}
        </ValuesRow>
        {extraContent}
      </Container>
    </Col>
  );
};

Box.propTypes = {
  size: PropTypes.number.isRequired,
  icon: PropTypes.string.isRequired,
  iconColor: PropTypes.string.isRequired,
  values: PropTypes.array.isRequired,
  extraContent: PropTypes.node,
};

Box.defaultProps = {
  extraContent: null,
};

export default Box;
