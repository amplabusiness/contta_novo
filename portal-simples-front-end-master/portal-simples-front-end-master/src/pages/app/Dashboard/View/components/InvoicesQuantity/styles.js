import styled from 'styled-components';
import { Button } from 'antd';

export const ShowQuantityButton = styled.button`
  width: 40px;
  height: 40px;
  border: none;
  border-radius: 50%;
  background: #3276b1;
  position: absolute;
  bottom: 30px;
  right: 20px;
  cursor: pointer;

  display: grid;
  place-items: center;
`;

export const EditButton = styled(Button)`
  width: 24px;
  height: 24px;
`;
