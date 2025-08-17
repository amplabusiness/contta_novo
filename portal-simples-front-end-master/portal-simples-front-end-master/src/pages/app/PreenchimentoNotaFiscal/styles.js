import styled from 'styled-components';

export const Card = styled.article`
  margin-top: 20px;
  padding: 20px 20px 30px 20px;
  border-radius: 4px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);
  cursor: pointer;
  transition: 0.3s;

  display: flex;
  flex-direction: column;
  align-items: center;

  &:hover {
    transform: scale(1.05);
  }

  > h2 {
    margin: 10px 0 15px 0;
    font-weight: normal;
  }

  > p {
    margin: 0;
    font-weight: lighter;
    font-size: 0.875rem;
    text-align: center;
  }
`;

export const IconContainer = styled.figure`
  margin: 0;
  width: 100px;
  height: 100px;
  border-radius: 50%;
  background: rgba(50, 118, 177, 0.2);
  color: #3276b1;

  display: grid;
  place-items: center;
`;
