import styled from 'styled-components';

export const Container = styled.article`
  margin: 20px 0;
  padding: 30px 20px;
  width: 100%;
  border-radius: 10px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);
  position: relative;
`;

export const Icon = styled.div`
  width: 40px;
  height: 40px;
  border: 1px solid ${({ color }) => color};
  border-radius: 50%;

  display: grid;
  place-items: center;

  > svg {
    color: ${({ color }) => color};
  }
`;

export const ValuesRow = styled.div`
  display: flex;
  gap: 40px;
  flex-wrap: wrap;
`;

export const Value = styled.h2`
  margin: 15px 0 10px 0;
  color: ${({ color }) => color};
  font-weight: bold;
`;

export const Label = styled.p`
  margin: 0;
  color: #252525;
  font-size: 0.875rem;
  font-weight: lighter;
`;
