import styled from 'styled-components';

export const WarningCard = styled.article`
  margin: 20px 0 60px 0;
  padding: 20px;
  width: 100%;
  border-radius: 4px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);

  > p {
    margin: 10px 0 0 4px;
    font-size: 0.875rem;
    font-weight: lighter;
  }
`;

export const WarningTitle = styled.div`
  color: #3276b1;

  display: flex;
  align-items: center;

  > h2 {
    margin: 0 0 0 12px;
    color: inherit;
    font-weight: normal;
  }
`;
