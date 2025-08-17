import styled from 'styled-components';

export const Total = styled.div`
  margin: 30px 0 0 auto;
  padding: 15px 10px;
  width: 180px;
  border-radius: 6px;
  background: #3276b1;
  color: #fff;
  box-shadow: 0 4px 4px rgba(0, 0, 0, 0.25);

  display: flex;
  align-items: center;
  justify-content: space-between;

  @media screen and (max-width: 769px) {
    margin: 30px auto;
  }

  > p {
    margin: 0;
    font-size: 1rem;
  }

  > span {
    font-size: 1rem;
    font-weight: bold;
  }
`;
