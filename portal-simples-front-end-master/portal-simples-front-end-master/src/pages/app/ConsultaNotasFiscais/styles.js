import styled from 'styled-components';

export const Container = styled.div`
  margin: 30px 10px 0 10px;

  input,
  select {
    height: 40px;
  }

  .ant-picker {
    height: 40px;
    padding: 0 8px;
  }
`;

export const Filters = styled.div`
  margin: 40px 0 20px 0;
`;

export const FilterTitle = styled.div`
  margin-bottom: 10px;

  display: flex;
  align-items: center;

  > svg {
    margin-right: 8px;
  }

  > p {
    margin: 0;
    font-size: 0.875rem;
    font-weight: lighter;
  }
`;
