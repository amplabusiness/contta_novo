import styled from 'styled-components';
import { Breadcrumb } from 'antd';

export const CustomBreadcrumb = styled(Breadcrumb)`
  .ant-breadcrumb-link {
    font-size: 0.875rem;

    > span {
      color: #333333;
    }
    > a {
      &:hover {
        color: #3276b1;
      }
    }
  }

  .ant-breadcrumb-separator {
    /* color: #676a6c; */
    font-size: 0.875rem;
  }
`;
