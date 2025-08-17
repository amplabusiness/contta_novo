import { useState } from 'react';
import { Tooltip } from 'antd';
import { FiDownload, FiCheck, FiX } from 'react-icons/fi';
import { useSelector } from 'react-redux';

import useUpdateConfiguration from '@/services/api/hooks/app/shared/useUpdateConfiguration';

import { Container } from './styles';

const DownloadEmalote = () => {
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );
  const { isAdmin } = useSelector(state => state.userState);

  const [isButtonClicked, setIsButtonClicked] = useState(
    isAdmin ? clickedDownLoadButton : true,
  );

  const { mutate } = useUpdateConfiguration('clickedDownLoadButton');

  const handleDownload = async () => {
    const link = document.createElement('a');
    link.setAttribute('href', './resources/e-Malote.exe');
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);

    mutate();

    setIsButtonClicked(true);
  };

  return (
    <Container id="download-emalote-button" isButtonDisabled={isButtonClicked}>
      <button type="button" onClick={handleDownload} disabled={isButtonClicked}>
        <FiDownload />
        <span>eMalote</span>
      </button>
      {isButtonClicked ? (
        <Tooltip title="eMalote instalado">
          <div style={{ display: 'grid', placeItems: 'center' }}>
            <FiCheck color="#38b000" />
          </div>
        </Tooltip>
      ) : (
        <Tooltip title="eMalote não instalado">
          <div style={{ display: 'grid', placeItems: 'center' }}>
            <FiX color="#ff0000" />
          </div>
        </Tooltip>
      )}
    </Container>
  );
};

export default DownloadEmalote;
