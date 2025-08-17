import PropTypes from 'prop-types';
import { Typography } from 'antd';

import { iconStyles } from '@/components/TopHeader/components/Notification/constants';

import { Container, IconBox } from './styles';

const Item = ({ notification, closePopover }) => {
  const handleNotification = () => {
    closePopover();

    const updatedNotification = {
      ...notification,
      active: false,
    };

    console.log(updatedNotification);
  };

  const iconStyle = iconStyles[notification.result.toLowerCase()];

  return (
    <Container onClick={handleNotification}>
      <IconBox background={iconStyle.color}>{iconStyle.icon}</IconBox>
      <Typography.Paragraph ellipsis={{ rows: 2 }} style={{ margin: 0 }}>
        {notification.description}
      </Typography.Paragraph>
    </Container>
  );
};

Item.propTypes = {
  notification: PropTypes.object.isRequired,
  closePopover: PropTypes.func.isRequired,
};

export default Item;
