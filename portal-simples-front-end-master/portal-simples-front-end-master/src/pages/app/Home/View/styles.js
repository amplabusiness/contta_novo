import styled from 'styled-components';
import { Button } from 'antd';

export const Heading = styled.div`
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

export const Content = styled.div`
  margin: 30px 0;
  width: 100%;

  display: flex;
  flex-direction: column;
  justify-content: center;

  .table-manipulation {
    margin-bottom: 20px;

    display: flex;
    align-items: center;
    justify-content: space-between;

    input {
      max-width: 300px;
      width: 100%;
      height: 40px;
    }

    @media (max-width: 769px) {
      flex-direction: column;
      gap: 20px;

      input {
        max-width: 100%;
      }
    }
  }
`;

export const CompanyName = styled.button`
  padding: 0;
  max-width: 300px;
  border: none;
  background: transparent;
  white-space: nowrap;
  text-overflow: ellipsis;
  overflow: hidden;
  cursor: pointer;

  display: inline-block;
`;

export const IconButton = styled(Button)`
  height: 30px;
  border-color: ${({ background }) => background};
  background: ${({ background }) => background};
  color: #fff;

  display: inline-flex;
  align-items: center;

  &:not(:disabled):hover {
    border-color: ${({ background }) => background};
    background: ${({ background }) => background};
    filter: brightness(0.8);
    color: #fff;
  }

  &:focus {
    border-color: ${({ background }) => background};
    background: ${({ background }) => background};
    color: #fff;
  }

  > svg {
    margin-right: 6px;
  }
`;
