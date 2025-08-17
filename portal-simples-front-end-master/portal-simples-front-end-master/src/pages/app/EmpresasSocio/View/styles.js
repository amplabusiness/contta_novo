import styled from 'styled-components';
import { Button, Table } from 'antd';

export const MemberTable = styled(Table)`
  border: 1px solid #3276b1;
  border-radius: 6px 6px 0 0;

  table {
    th {
      background-color: #3276b1;
      color: #fff;
      font-weight: bold;
    }

    i {
      margin-left: 10px;
      color: #3276b1;
      font-size: 1.125rem;
      cursor: pointer;
    }
  }
`;

export const GoBackButton = styled(Button)`
  margin-bottom: 15px;
  padding: 0;

  display: flex;
  align-items: center;

  > p {
    margin: 0 0 0 8px;
    color: #3276b1;
    font-size: 0.875rem;
    font-weight: normal;
  }
`;
