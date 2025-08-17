import styled from 'styled-components';

export const FooterContainer = styled.div`
  width: 100%;

  display: flex;
  align-items: center;
  justify-content: space-between;
`;

export const DeleteAllItems = styled.button`
  width: 30px;
  height: 30px;
  border: none;
  border-radius: 50%;
  background: #ff0033;
  cursor: pointer;

  display: flex;
  align-items: center;
  justify-content: center;

  &:focus {
    outline: 2px solid #000;
  }
`;
