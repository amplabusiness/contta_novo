import styled from 'styled-components';
import { Button } from 'antd';

export const Container = styled.div`
  min-height: 300px;

  .ant-select {
    display: block;
  }
`;

export const Info = styled.div`
  margin-bottom: 20px;

  > h2 {
    font-size: 1.5rem;
    font-weight: bold;
  }

  > p {
    margin: 0;
    font-size: 0.875rem;
  }
`;

export const CancelButton = styled(Button)`
  margin-left: 10px;
  border-color: #ff0033;
  background: #ff0033;

  &:hover,
  &:focus {
    border-color: #ff0033;
    background: #ff0033;
  }
`;
