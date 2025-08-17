import styled from 'styled-components';

export const Container = styled.div`
  margin: 5px 0;
  padding: 6px 0 6px 26px;
  height: 61px;
  border-top: 1px solid #e7eaec;
  position: relative;
  transition: 0.3s;

  display: flex;
  align-items: center;

  > span {
    font-size: 0.8125rem;
    font-weight: bold;
    position: absolute;
    left: 60px;
    animation: fade 2s;

    display: none;

    @keyframes fade {
      from {
        opacity: 0;
      }

      to {
        opacity: 1;
      }
    }
  }
`;

export const MobileContainer = styled.div`
  padding: 0 12px;
  width: 100%;
  height: 60px;
  border-top: 1px solid #e7eaec;
  cursor: pointer;
  transition: 0.3s;

  display: flex;
  align-items: center;

  svg {
    margin-right: 20px;
  }

  > span {
    font-size: 0.8125rem;
    font-weight: bold;
  }
`;
