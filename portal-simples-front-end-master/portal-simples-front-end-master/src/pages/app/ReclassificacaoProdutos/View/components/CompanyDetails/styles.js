import styled from 'styled-components';

export const Container = styled.div`
  margin-bottom: 50px;
`;

export const Content = styled.div`
  > p {
    margin: 0 0 5px 0;
    color: #676a6c;
    font-size: 0.875rem;
    font-weight: bold;

    @media screen and (max-width: 769px) {
      text-align: center;
    }
  }

  > span {
    color: #676a6c;
    font-size: 0.875rem;
    font-weight: lighter;

    @media screen and (max-width: 769px) {
      text-align: center;

      display: block;
    }
  }
`;
