import styled from 'styled-components';
import { AutoComplete } from 'antd';

export const CustomAutoComplete = styled(AutoComplete)`
  margin: 0 10px;

  flex: 1;

  &.ant-select:not(.ant-select-customize-input) .ant-select-selector {
    border: 1px solid transparent;
    background: transparent;

    &:hover {
      border-color: #3276b1;
    }

    &:focus-within {
      background: #fff;
    }

    input {
      &::selection {
        background: #3276b1;
      }
    }

    @media screen and (max-width: 769px) {
      border: 1px solid #ccc;
      background: #fff;
    }
  }
`;
