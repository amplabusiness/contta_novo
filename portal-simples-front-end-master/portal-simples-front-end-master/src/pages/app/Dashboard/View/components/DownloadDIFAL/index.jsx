import { Tooltip } from 'antd';
import { IoDownload } from 'react-icons/io5';

import { CustomButton } from './styles';

const DownloadDIFAL = () => {
  return (
    <Tooltip title="Baixar relatório do DIFAL">
      <CustomButton type="text">
        <IoDownload size={32} color="#3276b1" />
      </CustomButton>
    </Tooltip>
  );
};

export default DownloadDIFAL;
