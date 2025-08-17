import styled from 'styled-components';

export const Container = styled.button`
  padding: 20px 10px;
  width: clamp(120px, 320px, 320px);
  border: none;
  background: #fff;
  cursor: pointer;

  display: flex;
  align-items: center;

  &:hover {
    background: #f5f8fa;
  }
`;

export const IconBox = styled.div`
  margin-right: 10px;
  padding: 0 10px;
  height: 42px;
  background: ${({ background }) => background};

  display: grid;
  place-items: center;
`;
