import styled from 'styled-components';
import { Button } from 'antd';

export const Content = styled.div`
  margin: 30px auto 20px auto;
  max-width: 300px;
`;

export const RegisterCompanyButton = styled(Button)`
  margin-top: 5px;
  padding: 7.8px 20px;
  max-width: 300px;
  width: 100%;
  background: #43aa8b;
  border-color: #43aa8b;
  color: #fff;

  &:hover {
    border-color: #43aa8b;
    color: #43aa8b;
    background: #fff;
  }

  &:focus {
    border-color: #43aa8b;
    color: #43aa8b;
    background: #fff;
  }

  @media (max-width: 769px) {
    margin-bottom: 8px;
    width: 100%;
  }
`;

export const ModalText = styled.p`
  font-size: 0.875rem;
  line-height: 26px;
`;
