import styled from 'styled-components';
import { Button } from 'antd';

export const StyledButton = styled(Button)`
  margin: 2px 5px;
  height: 36px;
  border-color: #3276b1;
  background: #3276b1;
  color: #fff;
  box-shadow: 0 0 6px rgba(0, 0, 0, 0.3);

  display: inline-flex;
  align-items: center;

  svg {
    margin-right: 5px;
  }

  @media screen and (max-width: 576px) {
    font-size: 1rem;

    svg {
      margin: 0 0 5px 0;
    }

    > span {
      display: none;
    }
  }
`;
