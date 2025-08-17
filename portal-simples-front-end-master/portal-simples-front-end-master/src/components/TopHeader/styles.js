import styled from 'styled-components';

export const TopHeaderContainer = styled.div`
  padding: 0 15px;
  height: 61px;
  width: 100%;
  background: #f9fafb;
  position: sticky;
  top: 0;
  z-index: 10;

  display: flex;
  align-items: center;
  justify-content: space-between;

  @media (max-width: 769px) {
    justify-content: flex-start;

    > span {
      font-size: 0.6785rem;
      text-align: center;

      flex: 1;
    }

    .nav-items {
      display: none;
    }
  }

  .nav-items {
    width: 400px;

    display: flex;
    align-items: center;
    justify-content: space-between;

    > button {
      outline: none;
      border: none;
      background: transparent;
    }
  }
`;
