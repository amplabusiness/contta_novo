import styled from 'styled-components';

const Skeleton = styled.div`
  background-image: linear-gradient(
    -90deg,
    #c7ccd1 0%,
    #f8f8f8 50%,
    #c7ccd1 100%
  );
  background-size: 400% 400%;
  animation: shimmer 1s ease-in-out infinite;

  @keyframes shimmer {
    0% {
      background-position: 0% 0%;
    }
    100% {
      background-position: -135% 0%;
    }
  }
`;

export default Skeleton;
