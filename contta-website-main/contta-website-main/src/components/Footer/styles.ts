import styled from 'styled-components';

export const Container = styled.div`
  padding: 20px 0;
  width: 100%;
  background: #000;
`;

export const Wrapper = styled.div`
  margin: 0 auto;
  max-width: 300px;
  width: 100%;
  text-align: center;

  display: flex;
  align-items: center;
  justify-content: center;

  @media screen and (min-width: 640px) {
    max-width: 500px;
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

export const Text = styled.p`
  color: ${({ theme }) => theme.colors.ligthTextColor};
  font-size: 0.875rem;
`;
