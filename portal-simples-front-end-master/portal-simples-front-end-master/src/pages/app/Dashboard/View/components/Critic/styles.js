import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const Container = styled.article`
  margin: 20px 0;
  width: 100%;
  border-radius: 10px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);
`;

export const Heading = styled.div`
  padding: 15px 30px;
  background: #fafafa;

  > h2 {
    margin: 0;
    color: #252525;
    font-weight: bold;
  }
`;

export const Body = styled.div`
  padding: 15px 30px;
`;

export const Line = styled.div`
  width: 100%;

  display: flex;
  flex-wrap: wrap;
  align-items: center;
  justify-content: space-between;
`;

export const Label = styled(Link)`
  margin: 0;
  color: #252525;
  font-size: 1rem;
  font-weight: lighter;
  transition: 0.3s;

  &:hover {
    color: #965005;
    transform: scale(1.06);
  }
`;

export const Value = styled.span`
  margin: 0;
  color: #965005;
  font-size: 1.5rem;
  font-weight: bold;
`;
