import styled from 'styled-components';

export const Container = styled.div`
  padding: 0 15px;
`;

export const TableManipulation = styled.div`
  margin: 20px 0;

  display: flex;
  align-items: center;
  justify-content: space-between;

  input {
    max-width: 300px;
    width: 100%;
    height: 40px;
  }

  @media (max-width: 769px) {
    flex-direction: column;
    gap: 20px;

    input {
      max-width: 100%;
    }
  }
`;
