import styled from 'styled-components';
import { Button } from 'antd';

export const ViewButton = styled(Button)`
  border-color: #b30b00;
  background: #b30b00;
  color: #fff;

  &:hover {
    border-color: #b30b00;
    background: #b30b00;
    filter: brightness(0.8);
    color: #fff;
  }

  &:focus {
    border-color: #b30b00;
    background: #b30b00;
    color: #fff;
  }

  > svg {
    margin: 0 6px 2px 0;
  }
`;
