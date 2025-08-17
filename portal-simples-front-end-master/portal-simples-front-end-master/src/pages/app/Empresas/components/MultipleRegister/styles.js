import styled from 'styled-components';

export const Container = styled.form`
  margin: 0 auto 20px auto;
  max-width: 300px;
`;

export const UploadLabel = styled.label`
  margin: 0;
  padding: 7.8px 20px;
  width: 100%;
  height: 40px;
  background: #3276b1;
  border: 1px solid #3276b1;
  border-radius: 4px;
  color: #fff;
  cursor: pointer;
  pointer-events: ${({ disabled }) => (disabled ? 'none' : 'default')};
  transition: 0.3s;

  display: flex;
  align-items: center;
  justify-content: center;

  &:hover {
    background: #fff;
    color: #3276b1;
  }

  @media screen and (max-width: 769px) {
    margin-bottom: 8px;
  }

  input {
    display: none;
  }

  svg {
    margin: 0 0 2px 8px;
  }
`;
