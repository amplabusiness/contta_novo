import styled from 'styled-components';
import { Input } from 'antd';

export const Container = styled.div`
  table {
    tr {
      &:nth-child(2n) td {
        background: #fafafa;
      }
    }

    td {
      &:last-child {
        svg {
          margin: 0 4px;
        }
      }
    }
  }

  .table-manipulation {
    margin: 20px 0 25px;

    display: flex;
    gap: 12px;
    align-items: center;
    justify-content: center;
  }
`;

export const CustomInput = styled(Input)`
  width: 250px;
`;

export const CompanyName = styled.p`
  margin: 0;
  max-width: 300px;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;

  display: inline-block;
`;

export const DeleteCompanyButton = styled.button`
  padding: 0;
  border: none;
  background: transparent;
  cursor: pointer;
`;
