import styled, { createGlobalStyle } from 'styled-components';

export const Container = styled.div`
  margin: 30px 10px 0 10px;
`;

export const Title = styled.div`
  > h2 {
    margin: 0 0 10px 0;
    padding: 0 15px;
    position: relative;
    color: #67696c;

    &::before {
      content: '';
      position: absolute;
      left: 0;
      width: 4px;
      height: 100%;
      background: #3276b1;
    }
  }

  > p {
    margin: 0;
    padding: 0 15px;
    color: #676a6c;
    font-size: 0.875rem;
    font-weight: lighter;
  }
`;

export default createGlobalStyle`
  h1,
  h2,
  h3 {
    font-weight: 100;
  }

  h1,
  h2,
  h3 {
    margin-top: 20px;
    margin-bottom: 10px;
  }

  h1 {
    font-size: 1.875rem;
  }

  h2 {
    font-size: 1.5rem;
  }

  h3 {
    font-size: 1rem;
  }

  body {
    background: #f9fafb;
  }

  table {
    &:not(.ant-picker-content) {
      thead {
        height: 47px;

        tr {
          .ant-table-cell {
            font-weight: 600;
            border-bottom: 2px solid #e6e8eb;
          }
        }
      }

      tbody {
        tr {
          .ant-table-cell {
            height: 40px;
          }
        }
      }

      tr {
        &:nth-child(2n):not(.simples-row) td {
          background: #f3f6f9;
        }
      }
    }
  }

  #page-wrapper {
    min-height: 568px;
    position: relative !important;
  }

  @media (min-width: 769px) {
    #page-wrapper {
      position: inherit;
      margin: 0 0 0 70px;
      min-height: 100vh;
    }
  }

  @media (max-width: 769px) {
    #page-wrapper {
      position: inherit;
      margin: 0 0 0 0;
      min-height: 100vh;
    }
  }

  .validation-errors {
    margin-top: 4px;
    min-height: 20px;
    color: #ff0033;
  }

  .ant-form-large .ant-form-item-label {
    height: 40px;
  }

  .ant-btn {
    font-size: 0.875rem;

    display: inline-flex;
    align-items: center;
    justify-content: center;
  }

  .ant-select {
    width: 100%;
  }

  .page-header {
    padding: 16px 24px 24px 24px;
    border: 1px solid #e7eaec;
    border-radius: 10px;

    .ant-page-header-heading-title {
      color: #333333;
      font-weight: 500;
    }

    .ant-page-header-content {
      padding-top: 10px;
    }
  }
`;
