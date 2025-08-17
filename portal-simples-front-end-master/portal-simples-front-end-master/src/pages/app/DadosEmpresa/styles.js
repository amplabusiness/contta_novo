import styled from 'styled-components';

export const Card = styled.div`
  margin-top: 30px;
  padding: 20px 20px 40px 20px;
  border-radius: 10px;
  background: #fff;
  box-shadow: 0 4px 6px -1px rgba(0, 0, 0, 0.1);

  h2 {
    margin: 0 0 10px 0;
    color: #333;
    font-weight: normal;
  }

  input[type='text'],
  select,
  button {
    height: 40px;
  }

  input[type='text'] {
    color: rgb(75, 88, 88);
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
  }

  .ant-select {
    display: block;
  }

  .ant-checkbox-disabled {
    + span {
      color: #333;
    }
  }

  .ant-checkbox-group-item {
    margin: 20px 8px 0 0;
  }

  .ant-radio-disabled {
    + span {
      color: #333;
    }
  }
`;
