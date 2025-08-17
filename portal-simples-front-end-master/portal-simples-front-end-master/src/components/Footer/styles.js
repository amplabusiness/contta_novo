import styled from 'styled-components';

export const FooterContainer = styled.div`
  padding: 10px 20px;
  border-top: 1px solid #e7eaec;
  background: none repeat scroll 0 0 white;
  position: absolute;
  bottom: 0;
  right: 0;
  left: 0;

  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;

  @media (max-width: 769px) {
    > div {
      margin: 0 auto;
    }
  }
`;
