import styled from 'styled-components';
import { Button } from 'antd';

export const MemberButton = styled(Button)`
  padding: 0;
  width: 42px;
  height: 42px;
  outline: none;
  border: none;
  border-radius: 50%;
  background: rgba(50, 118, 177, 0.4);
  color: #3276b1;
  transition: 0.3s;

  display: flex;
  align-items: center;
  justify-content: center;

  &:hover {
    background: #3276b1;
    color: #fff;
  }

  &:disabled {
    background: #ccc;
    color: #242424;
    cursor: not-allowed;
  }
`;
