import { FiX } from 'react-icons/fi';

import { useUIContext } from '../../context/ui';

import { Container, CloseOverlay, Video } from './styles';

const VideoOverlay: React.FC = () => {
  const {
    uiState: { isVideoOpen },
    closeVideo,
  } = useUIContext();

  return isVideoOpen ? (
    <Container>
      <CloseOverlay onClick={() => closeVideo()}>
        <FiX size={44} color="#fff" />
      </CloseOverlay>
      <Video src="https://www.youtube.com/embed/LXb3EKWsInQ" />
    </Container>
  ) : null;
};

export default VideoOverlay;
