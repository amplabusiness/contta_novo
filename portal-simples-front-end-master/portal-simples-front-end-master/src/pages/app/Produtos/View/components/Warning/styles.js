import styled from 'styled-components';
import { Button } from 'antd';

export const Container = styled.div`
  margin: 30px 10px 0 10px;

  display: flex;
  align-items: center;
  justify-content: center;
`;

export const Card = styled.div`
  margin: 10px;
  padding: 30px;
  max-width: 500px;
  width: 100%;
  border-radius: 6px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);

  p {
    font-size: 0.875rem;
  }
`;

export const CustomButton = styled(Button)`
  padding: 10px 0;
  width: 100%;
  max-width: 300px;
  border: 1px solid ${({ color }) => color};
  border-radius: 4px;
  background: ${({ color }) => color};
  color: #fff;
  transition: 0.3s;

  display: flex;
  align-items: center;
  justify-content: center;

  &:hover,
  &:focus {
    border-color: ${({ color }) => color};
    background: #fff;
    color: ${({ color }) => color};
  }

  > svg {
    margin-right: 5px;
  }

  > input {
    display: none;
  }
`;
