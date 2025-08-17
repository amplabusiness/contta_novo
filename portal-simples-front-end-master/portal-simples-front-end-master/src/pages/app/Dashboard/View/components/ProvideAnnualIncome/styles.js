import styled from 'styled-components';
import { Button } from 'antd';

export const Container = styled.div`
  padding: 30px 20px;
  width: 100%;
  height: 100%;
  border-radius: 10px;
  background: rgba(0, 0, 0, 0.8);
  position: absolute;
  top: 0;
  left: 0;
  bottom: 0;
  right: 0;
  z-index: 1;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;

  > span {
    margin-bottom: 8px;
    color: #fff;
    text-align: center;
  }
`;

export const CustomButton = styled(Button)`
  border: none;
  background: #3276b1;
  color: #fff;
`;
