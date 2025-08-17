import styled from 'styled-components';

export const Container = styled.div`
  width: 140px;
  border: 1px solid #ccc;
  border-radius: 4px;
  background: #fff;
  overflow: hidden;

  display: flex;
  align-items: center;
  justify-content: center;

  @media (max-width: 769px) {
    width: 100px;
  }

  svg {
    font-size: 20px;

    @media (max-width: 769px) {
      font-size: 14px;
    }
  }

  button {
    padding: 5px 10px;
    border: none;
    border-right: 1px solid #ccc;
    outline: none;
    background: ${props => (props.isButtonDisabled ? '#ccc' : '#fff')};
    color: ${props => (props.isButtonDisabled ? '#a8a8a8' : '#3276b1')};
    cursor: pointer;

    flex: 1;
    display: flex;
    align-items: center;
    justify-content: center;

    &:disabled {
      cursor: default;
    }

    @media (max-width: 769px) {
      padding: 5px 0;
    }

    span {
      margin-left: 5px;
      font-weight: bold;

      @media (max-width: 769px) {
        font-size: 0.6785rem;
      }
    }
  }

  div {
    padding: 0 5px;
    background: #fff;

    @media (max-width: 769px) {
      padding: 0 2px;
    }
  }
`;
