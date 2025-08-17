import styled from 'styled-components';

export const Container = styled.div`
  margin-bottom: 20px;
  width: 100%;
`;

export const Main = styled.div`
  margin-bottom: 5px;
  padding-right: 12px;

  display: flex;
  align-items: center;
  justify-content: space-between;

  > p {
    margin: 0;
  }
`;

export const FileName = styled.p`
  margin: 0;
  max-width: 250px;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;

  display: inline-block;
`;

export const DeleteFile = styled.button`
  width: 24px;
  height: 24px;
  border: none;
  border-radius: 50%;
  background: #ff0033;
  cursor: pointer;

  display: flex;
  align-items: center;
  justify-content: center;

  &:focus {
    outline: 2px solid #000;
  }
`;
