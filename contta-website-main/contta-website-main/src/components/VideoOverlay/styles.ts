import styled from 'styled-components';

export const Container = styled.div`
  width: 100%;
  height: 100vh;
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  background: rgba(0, 0, 0, 0.8);
  color: red;
  z-index: 10;

  display: flex;
  align-items: center;
  justify-content: center;
`;

export const CloseOverlay = styled.button`
  border: none;
  background: transparent;
  position: absolute;
  top: 20px;
  right: 20px;
  cursor: pointer;
`;

export const Video = styled.iframe`
  width: 1000px;
  height: 550px;
`;
