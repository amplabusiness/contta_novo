import { Button, Tooltip } from 'antd';
import { IoMdRefresh } from 'react-icons/io';

const RefreshButton = () => {
  const refreshPage = () => {
    window.location.reload();
  };

  return (
    <Tooltip title="Atualizar página" placement="bottom">
      <Button type="text" onClick={refreshPage} style={{ padding: 0 }}>
        <IoMdRefresh size={28} color="#3276b1" />
      </Button>
    </Tooltip>
  );
};

export default RefreshButton;
