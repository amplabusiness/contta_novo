import styled from 'styled-components';

export const Card = styled.div`
  margin: 0 auto;
  padding: 40px 20px;
  max-width: 400px;
  background: #fff;
  border: 1px solid #d5d5d5;
  border-radius: 6px;

  display: flex;
  flex-direction: column;
  align-items: center;

  button {
    width: 60%;
  }
`;

export const IconContainer = styled.div`
  width: 120px;
  height: 120px;
  border-radius: 50%;
  background: #ff0033;

  display: flex;
  align-items: center;
  justify-content: center;
`;

export const Title = styled.h2`
  margin: 20px 0;
  font-weight: normal;
`;

export const Text = styled.p`
  margin: 0 0 20px 0;
  font-size: 1rem;
  font-weight: lighter;
  text-align: center;
`;
