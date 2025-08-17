import styled from 'styled-components';

export const Container = styled.div`
  width: 100%;
  height: 100vh;
  background: url('/images/meet.jpg') no-repeat center center;
  background-size: cover;
  position: relative;

  display: flex;
  justify-content: space-between;

  &:before {
    content: '';
    width: 100%;
    height: 100vh;
    background: linear-gradient(
      #050505 0%,
      #050505 8%,
      rgba(0, 0, 0, 0.8) 100%
    );
    position: absolute;
    top: 0;
    left: 0;
  }

  > * {
    z-index: 1;
  }
`;

export const Wrapper = styled.section`
  margin: 0 auto;
  max-width: 300px;
  width: 100%;
  position: relative;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  @media screen and (min-width: 640px) {
    max-width: 500px;

    align-items: flex-start;
  }

  @media screen and (min-width: 768px) {
    max-width: 620px;
  }

  @media screen and (min-width: 1024px) {
    max-width: 900px;
  }

  @media screen and (min-width: 1280px) {
    max-width: 1100px;
  }

  @media screen and (min-width: 1536px) {
    max-width: 1400px;
  }
`;

export const Heading = styled.div`
  width: 300px;
  color: ${({ theme }) => theme.colors.ligthTextColor};
  text-align: center;

  @media screen and (min-width: 640px) {
    width: 500px;
  }

  @media screen and (min-width: 768px) {
    width: 600px;
    text-align: left;
  }

  @media screen and (min-width: 1536px) {
    width: 800px;
  }

  > h1 {
    font-size: 2rem;

    @media screen and (min-width: 768px) {
      font-size: 3rem;
    }

    @media screen and (min-width: 1536px) {
      font-size: 4rem;
    }

    > span {
      color: ${({ theme }) => theme.colors.primaryColor};
    }
  }
`;

export const ActionsContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;

  @media screen and (min-width: 640px) {
    flex-direction: row;
  }
`;

export const ActionButton = styled.button`
  margin-top: 50px;
  padding: 20px;
  border: none;
  border-radius: 4px;
  background: ${({ theme }) => theme.colors.primaryColor};
  color: ${({ theme }) => theme.colors.ligthTextColor};
  font-size: 1rem;
  font-weight: bold;
  cursor: pointer;
  transition: 0.3s;

  &:hover {
    background: #fff;
    color: ${({ theme }) => theme.colors.primaryColor};
  }
`;

export const VideoAction = styled.button`
  margin: 50px 0 0 0;
  padding: 13px 0;
  width: 200px;
  border: none;
  border-radius: 40px;
  background: transparent;
  cursor: pointer;

  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;

  svg {
    border-radius: 50%;
    box-shadow: 0 0 0 rgba(50, 118, 177, 0.4);
    animation: pulse 2s infinite;

    @keyframes pulse {
      0% {
        box-shadow: 0 0 0 0 rgba(50, 118, 177, 0.4);
      }
      70% {
        -moz-box-shadow: 0 0 0 10px rgba(50, 118, 177, 0);
        box-shadow: 0 0 0 10px rgba(50, 118, 177, 0);
      }
      100% {
        -moz-box-shadow: 0 0 0 0 rgba(50, 118, 177, 0);
        box-shadow: 0 0 0 0 rgba(50, 118, 177, 0);
      }
    }
  }

  > p {
    margin-left: 10px;
    color: #fff;
    font-size: 1rem;
  }

  @media screen and (min-width: 640px) {
    margin: 50px 0 0 30px;
  }
`;

export const ScrollIndicator = styled.button`
  border: none;
  background: transparent;
  color: #fff;
  position: absolute;
  left: 50%;
  bottom: 20px;
  cursor: pointer;
  animation: bounce 1s infinite alternate;

  @keyframes bounce {
    from {
      transform: translateY(0px);
    }
    to {
      transform: translateY(8px);
    }
  }
`;
