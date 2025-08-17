import styled from 'styled-components';

export const Container = styled.div`
  margin: 30px 10px 0 10px;

  .title {
    margin-bottom: 10px;
    width: 200px;
    height: 37px;
    border-radius: 4px;
  }

  .subtitle {
    padding-left: 15px;
    width: 100%;
    height: 22px;
    border-radius: 4px;
  }

  .tabs {
    margin: 30px 0 16px 0;
    overflow-x: auto;

    display: flex;
    gap: 4px;
  }

  .tab {
    width: 200px;
    height: 40px;
    border-top-left-radius: 4px;
    border-top-right-radius: 4px;
  }

  .table {
    width: 100%;
    height: 202px;
    border-top-left-radius: 4px;
    border-top-right-radius: 4px;
  }
`;
