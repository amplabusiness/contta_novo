import styled from 'styled-components';

export const Status = styled.div`
  width: 90px;
  height: 40px;
  border-radius: 6px;
  background: ${({ isActive }) => (isActive ? '#38b000' : '#ff0033')};
  color: #fff;
  font-weight: bold;

  display: flex;
  align-items: center;
  justify-content: center;
`;
