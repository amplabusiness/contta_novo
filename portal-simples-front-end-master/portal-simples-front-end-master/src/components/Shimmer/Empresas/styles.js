import styled from 'styled-components';

export const Container = styled.div`
  width: 100%;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  .input {
    margin: 20px 0;
    max-width: 250px;
    width: 100%;
    height: 40px;
    border-radius: 4px;
  }

  .table {
    width: 100%;
    height: 305px;
    border-radius: 4px;
  }
`;
