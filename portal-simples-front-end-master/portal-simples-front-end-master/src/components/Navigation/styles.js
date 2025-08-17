import styled from 'styled-components';
import { Link } from 'react-router-dom';

export const Container = styled.div`
  display: unset;

  @media (max-width: 769px) {
    display: none;
  }
`;

export const Sidebar = styled.aside`
  width: 70px;
  min-height: 100vh;
  background: #fff;
  border-right: 1px solid #e7eaec;
  position: fixed;
  top: 0;
  left: 0;
  bottom: 0;
  transition: 0.3s;
  z-index: 999;
  overflow-y: auto;
  overflow-x: hidden;
  -ms-overflow-style: none;
  scrollbar-width: none;

  display: flex;
  flex-direction: column;

  &:hover {
    width: 220px;

    > a {
      > p {
        display: unset;
      }
    }

    li {
      .title {
        display: unset;
      }
    }

    .username {
      display: unset;
    }
  }

  &::-webkit-scrollbar {
    display: none;
  }

  > ul {
    margin: 10px 0 auto;
    padding: 0;
    list-style: none;
  }
`;

export const Logo = styled(Link)`
  margin: 5px;
  padding: 6px 15px;
  height: 45px;
  border-radius: 6px;
  background: #f3f3f4;
  transition: 0.3s;
  pointer-events: ${({ disabled }) => (disabled ? 'none' : 'auto')};

  display: flex;
  align-items: center;

  > img {
    height: 32px;
  }

  > p {
    margin: 0 0 0 12px;
    color: #b1b0b5;
    font-size: 0.875rem;
    font-weight: bold;
    text-transform: uppercase;
    animation: fade 2s;

    display: none;

    @keyframes fade {
      from {
        opacity: 0;
      }

      to {
        opacity: 1;
      }
    }
  }
`;

export const Item = styled.li`
  margin: 5px 0;
  padding: 6px 26px;
  height: 45px;
  background: #fff;
  position: relative;
  color: ${({ active }) => (active ? '#3276b1' : '#b1b0b5')};
  cursor: ${({ disabled }) => (disabled ? 'normal' : 'pointer')};
  transition: 0.3s;

  display: flex;
  align-items: center;

  ${({ disabled }) =>
    !disabled &&
    ` &:hover {
    color: #3276b1;
  }`};

  &:before {
    content: '';
    width: 4px;
    height: 80%;
    border-top-right-radius: 6px;
    border-bottom-right-radius: 6px;
    background: ${({ active }) => (active ? '#3276b1' : 'transparent')};
    position: absolute;
    left: 0;
    transition: 0.3s;
  }

  .title {
    margin-left: 20px;
    font-size: 0.8125rem;
    font-weight: 600;
    animation: fade 2s;

    display: none;

    @keyframes fade {
      from {
        opacity: 0;
      }

      to {
        opacity: 1;
      }
    }
  }
`;
