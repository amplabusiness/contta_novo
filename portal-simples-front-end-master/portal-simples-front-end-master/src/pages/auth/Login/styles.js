import styled from 'styled-components';
import { Button } from 'antd';

import wallpaperContta from '@/assets/images/wallpaperContta.jpeg';

export const Container = styled.div`
  width: 100vw;
  min-height: 100vh;
  background: url(${wallpaperContta}) no-repeat;
  background-position: center center;
  background-size: cover;

  display: flex;
  align-items: center;

  @media screen and (max-width: 769px) {
    padding: 0px 40px;

    justify-content: center;
  }
`;

export const LoginBox = styled.div`
  margin-left: 100px;
  padding: 20px;
  max-width: 350px;
  width: 100%;
  border-radius: 6px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.2);
  animation: fadeInDown 1s;

  display: flex;
  flex-direction: column;
  align-items: center;

  img {
    max-height: 150px;
  }

  @media screen and (max-width: 769px) {
    margin: 0px;
    max-width: 280px;

    img {
      max-height: 120px;
    }
  }

  @keyframes fadeInDown {
    from {
      transform: translateY(-100%);
    }

    to {
      transform: translateY(0);
    }
  }
`;

export const RegisterAccountButton = styled(Button)`
  width: 100%;
  height: 40px;
  border: 1px solid #ed8413;
  background: #ed8413;

  &:hover {
    border-color: rgba(237, 132, 19, 0.8);
    background: rgba(237, 132, 19, 0.8);
  }
`;

export const ForgoutPassword = styled.div`
  margin-top: 20px;
  width: 100%;

  display: flex;
  align-items: center;
  justify-content: center;
`;
