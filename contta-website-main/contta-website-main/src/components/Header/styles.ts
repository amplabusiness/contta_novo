import styled from 'styled-components';

interface IProps {
  scrolled: boolean;
}

/*
  Media Queries
  640px
  768px
  1024px
  1280px
  1536px
*/

export const Container = styled.header<IProps>`
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  background: ${({ scrolled }) => (scrolled ? '#fff' : 'transparent')};
  box-shadow: ${({ scrolled }) =>
    scrolled ? '0 0 10px rgba(0, 0 , 0, 0.25)' : 'transparent'};
  transition: 0.3s;
  z-index: 2;

  display: flex;
  align-items: center;
  justify-content: space-between;
`;

export const Wrapper = styled.div`
  margin: 0 auto;
  max-width: 300px;
  width: 100%;

  display: flex;
  align-items: center;
  justify-content: center;

  @media screen and (min-width: 640px) {
    max-width: 500px;

    justify-content: space-between;
  }

  @media screen and (min-width: 768px) {
    max-width: 620px;
  }

  @media screen and (min-width: 1024px) {
    max-width: 900px;
  }

  @media screen and (min-width: 1280px) {
    max-width: 1100px;
  }

  @media screen and (min-width: 1536px) {
    max-width: 1400px;
  }
`;

export const Nav = styled.nav`
  display: none;

  @media screen and (min-width: 640px) {
    display: flex;
  }
`;

export const List = styled.ul<IProps>`
  list-style: none;
  color: ${({ scrolled }) => (scrolled ? '#333' : '#fff')};

  display: flex;
  align-items: center;
`;

export const ListItem = styled.li`
  margin: 0 10px;

  > a {
    text-decoration: none;

    > button {
      border: none;
      background: transparent;
      color: inherit;
      font-size: 1rem;
      cursor: pointer;
      transition: 0.3s;

      &:hover {
        color: ${({ theme }) => theme.colors.primaryColor};
      }
    }
  }
`;
