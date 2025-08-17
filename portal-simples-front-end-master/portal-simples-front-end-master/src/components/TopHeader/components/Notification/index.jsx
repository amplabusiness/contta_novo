import { useState } from 'react';
import { Badge, Button, Popover } from 'antd';
import { FiBell } from 'react-icons/fi';

import Item from '@/components/TopHeader/components/Notification/components/Item';

const Notification = () => {
  const [visible, setVisible] = useState(false);

  const closePopover = () => {
    setVisible(false);
  };

  const handleVisibleChange = visibility => {
    setVisible(visibility);
  };

  const notifications = [];
  const areNotificationsEmpty = notifications
    ? notifications.length === 0
    : true;

  return (
    <Popover
      title={<p style={{ margin: 0, padding: '8px 0' }}>Notificações</p>}
      placement="bottomLeft"
      trigger="click"
      visible={visible}
      onVisibleChange={handleVisibleChange}
      content={
        !areNotificationsEmpty ? (
          notifications.map(notification => (
            <Item
              key={notification.id}
              notification={notification}
              closePopover={closePopover}
            />
          ))
        ) : (
          <p>Nenhuma notificação!</p>
        )
      }
    >
      <Button type="text" style={{ padding: 0 }}>
        <Badge
          overflowCount={9}
          count={notifications ? notifications.length : 0}
          size="small"
        >
          <FiBell size={20} color="#999c9e" />
        </Badge>
      </Button>
    </Popover>
  );
};

export default Notification;
