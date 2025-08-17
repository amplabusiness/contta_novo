import styled from 'styled-components';
import { Button, Drawer } from 'antd';

export const OpenButton = styled(Button)`
  padding: 0;
  width: 40px;
  height: 36px;
  border: none;
  border-radius: 4px;
  background: #3276b1;
  color: #fff;

  display: grid;
  place-items: center;
`;

export const CustomDrawer = styled(Drawer)`
  .ant-drawer-body {
    padding: 0;
    background: #fff;
  }
`;

export const List = styled.ul`
  margin: 0;
  padding: 0;
  list-style: none;

  display: flex;
  flex-direction: column;
`;

export const ListItem = styled.li`
  margin: 5px 0;
  padding: 0 20px;
  width: 100%;
  height: 50px;
  position: relative;
  background: '#fff';
  color: ${({ active }) => (active ? '#3276b1' : '#b1b0b5')};
  cursor: pointer;
  transition: 0.3s;

  display: flex;
  align-items: center;

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

  svg {
    margin-right: 20px;
  }

  > span {
    font-size: 0.8125rem;
  }

  > p {
    margin: 0;
    color: #999c9e;
    font-size: 0.8125rem;
  }

  > strong {
    margin: 0;
    color: #676a6c;
    font-size: 0.8125rem;
  }
`;

export const LogoutButton = styled.button`
  padding: 0;
  width: 100%;
  border: none;
  background: none;

  display: flex;

  > p {
    margin: 0;
    color: #999c9e;
    font-size: 0.8125rem;
  }
`;
