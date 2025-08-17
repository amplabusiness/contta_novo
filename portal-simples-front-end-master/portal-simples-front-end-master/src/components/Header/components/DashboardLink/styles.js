import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const CustomLink = styled(Link)`
  margin-top: 15px;
  color: ${({ disabled }) => (disabled ? '#ccc' : '#3276b1')};
  pointer-events: ${({ disabled }) => (disabled ? 'none' : 'auto')};

  display: inline-block;

  > svg {
    margin-right: 5px;
  }
`;
