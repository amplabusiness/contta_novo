import styled from 'styled-components';

export const Container = styled.div`
  margin-top: 10px;

  .last-row {
    background: #f0f0f0;
  }
`;

export const SectionTitle = styled.header`
  margin-top: 10px;

  a {
    border-bottom: 1px solid transparent;
    color: #3276b1;
    font-size: 1.5rem;
    font-weight: 300;
    transition: border-color 0.3s;

    &:hover {
      border-color: #3276b1;
    }
  }
`;
