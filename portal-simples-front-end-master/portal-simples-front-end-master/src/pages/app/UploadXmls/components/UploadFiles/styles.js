import styled from 'styled-components';

export const Container = styled.article`
  width: 100%;
  height: 250px;
  border: ${({ isDragActive }) =>
    isDragActive ? '1px solid #333' : '1px dashed #3276b1'};
  border-radius: 6px;
  background: #fff;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  > p {
    margin: 10px 10px 0 10px;
    font-size: 1rem;
    font-weight: lighter;
    text-align: center;

    > span {
      font-weight: normal;
    }
  }

  @media screen and (max-width: 769px) {
    margin-bottom: 50px;
  }
`;
