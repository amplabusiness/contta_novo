import styled from 'styled-components';
import wallpaperContta from '@/assets/images/wallpaperContta.jpeg';

export const Container = styled.div`
  padding: 40px 0;
  min-height: 100vh;
  background: url(${wallpaperContta}) no-repeat;
  background-position: center center;
  background-size: cover;

  display: flex;
  align-items: center;
  justify-content: center;
`;

export const Card = styled.div`
  margin: 0 auto;
  padding: 20px;
  max-width: 350px;
  width: 100%;
  border-radius: 6px;
  background: #ffff;
  box-shadow: 0 0 8px rgba(0, 0, 0, 0.2);
  animation: fadeInDown 1s;

  img {
    max-height: 150px;
  }

  @media (max-width: 769px) {
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

export const BackLink = styled.p`
  margin: 20px 0 0 0;
  font-size: 0.875rem;
  font-weight: lighter;
  text-align: center;

  > a {
    margin-left: 5px;
    color: #3276b1;
    font-weight: normal;
  }
`;
