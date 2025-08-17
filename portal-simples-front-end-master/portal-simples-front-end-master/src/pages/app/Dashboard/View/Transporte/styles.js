import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const Section = styled.section``;

export const SectionHeader = styled.div`
  * {
    margin: 0;
  }

  > h2 {
    font-size: 1.5rem;
    font-weight: bold;
  }

  p {
    margin-top: 5px;
    font-size: 0.875rem;
    font-weight: lighter;
  }
`;

export const StyledLink = styled(Link)`
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: #3276b1;
  position: absolute;
  top: 30px;
  right: 20px;

  display: grid;
  place-items: center;
`;

export const DisabledLink = styled.span`
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: #dfdfdf;
  position: absolute;
  top: 30px;
  right: 20px;

  display: grid;
  place-items: center;
`;
