import styled from 'styled-components';

export const Container = styled.div`
  margin-top: 30px;

  .ant-collapse-content > .ant-collapse-content-box {
    padding: 0 16px 20px;
  }
`;

export const TabTitle = styled.span`
  display: inline-flex;
  align-items: center;
  justify-content: center;

  > svg {
    margin-right: 5px;
  }
`;
