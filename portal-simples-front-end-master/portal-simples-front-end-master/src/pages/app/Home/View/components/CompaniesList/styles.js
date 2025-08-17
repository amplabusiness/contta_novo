import styled from 'styled-components';
import { Button } from 'antd';

export const CompanyName = styled.button`
  padding: 0;
  max-width: 300px;
  border: none;
  background: transparent;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  cursor: pointer;

  display: inline-block;
`;

export const IconButton = styled(Button)`
  height: 30px;
  border-color: ${({ background }) => background};
  background: ${({ background }) => background};
  color: #fff;

  display: inline-flex;
  align-items: center;

  &:not(:disabled):hover {
    border-color: ${({ background }) => background};
    background: ${({ background }) => background};
    filter: brightness(0.8);
    color: #fff;
  }

  &:focus {
    border-color: ${({ background }) => background};
    background: ${({ background }) => background};
    color: #fff;
  }

  > svg {
    margin-right: 6px;
  }
`;
