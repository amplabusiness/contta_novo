import styled from 'styled-components';
import { Button } from 'antd';

export const Container = styled.div`
  margin: 20px 10px 0 10px;
`;

export const GoBackButton = styled(Button)`
  margin-bottom: 15px;

  display: flex;
  align-items: center;

  > p {
    margin: 0 0 0 8px;
    color: #3276b1;
    font-size: 0.875rem;
    font-weight: normal;
  }
`;
