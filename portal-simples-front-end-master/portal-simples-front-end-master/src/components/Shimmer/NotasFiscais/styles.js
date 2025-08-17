import styled from 'styled-components';

export const Container = styled.div`
  margin-top: 30px;

  .first-table {
    width: 100%;
    height: 322px;
    border-radius: 4px;
  }

  > span {
    margin-top: 20px;

    display: flex;
    align-items: center;

    .tab {
      margin-right: 5px;
      width: 110px;
      height: 40px;
      border-top-left-radius: 4px;
      border-top-right-radius: 4px;
    }
  }

  .second-table {
    margin-top: 40px;
    width: 100%;
    height: 297px;
    border-radius: 4px;
  }
`;
