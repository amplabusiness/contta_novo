import styled from 'styled-components';

export const Container = styled.div`
  margin-top: 24px;

  .last-row {
    td {
      &:nth-child(2) {
        font-weight: 700;
        text-align: right;
      }
    }
  }

  .simples-row {
    background: rgba(255, 0, 51, 0.2);
  }
`;

export const Card = styled.div`
  margin: 20px 0;
  padding: 15px;
  width: 100%;
  border: 1px solid #dfdfdf;
  border-radius: 6px;
  background: #fff;
  box-shadow: 0 6px 6px -1px rgba(0, 0, 0, 0.1);

  > h2 {
    margin: 0;
    color: #858a91;
    font-size: 1.25rem;
    font-weight: 600;
    text-transform: uppercase;
  }

  .ant-divider.ant-divider-horizontal {
    margin: 10px 0;
    border-left: 1px solid #dfdfdf;
  }
`;
