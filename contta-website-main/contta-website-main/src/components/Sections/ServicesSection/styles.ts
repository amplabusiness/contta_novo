import styled from 'styled-components';

interface IContentContainerProps {
  type: 'cards' | 'descriptions';
}

interface IItemProps {
  active: boolean;
}

export const Container = styled.div`
  padding: 40px 0;
  width: 100%;
  background: #f5f5f5;

  @media screen and (min-width: 640px) {
    padding: 80px 0;
  }
`;

export const Wrapper = styled.div`
  margin: 0 auto;
  max-width: 300px;
  width: 100%;

  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: space-between;

  @media screen and (min-width: 640px) {
    max-width: 500px;
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

export const Heading = styled.div`
  text-align: center;

  > h2 {
    color: ${({ theme }) => theme.colors.headingColor};
    font-size: 2rem;
  }

  > p {
    margin-top: 10px;
    max-width: 600px;
    width: 100%;
    line-height: 30px;
  }
`;

export const ContentContainer = styled.div<IContentContainerProps>`
  margin-top: ${({ type }) => (type === 'cards' ? '60px' : '80px')};
  width: 100%;

  display: flex;
  flex-direction: column;
  align-items: ${({ type }) => (type === 'cards' ? 'center' : 'flex-start')};
  justify-content: ${({ type }) =>
    type === 'cards' ? 'space-around' : 'center'};

  @media screen and (min-width: 768px) {
    flex-direction: row;
  }
`;

export const Card = styled.div`
  margin-top: 20px;
  padding: 20px;
  width: 300px;
  min-height: 330px;
  border-radius: 6px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);
  transition: 0.3s;

  display: flex;
  flex-direction: column;
  align-items: center;

  &:hover {
    transform: scale(1.05);
  }

  > h2 {
    margin: 15px 0;
    color: ${({ theme }) => theme.colors.headingColor};
  }

  > p {
    margin-bottom: 8px;
    line-height: 24px;
    text-align: center;
  }

  @media screen and (min-width: 768px) {
    margin-top: 0;
  }
`;

export const IconContainer = styled.div`
  width: 100px;
  height: 100px;
  border-radius: 50%;
  background: rgba(50, 118, 177, 0.2);

  display: grid;
  place-items: center;
`;

export const ItemsContainer = styled.div`
  width: 300px;

  display: flex;
  flex-direction: row;
  align-items: center;
  justify-content: center;

  @media screen and (min-width: 768px) {
    flex-direction: column;
  }
`;

export const Item = styled.button<IItemProps>`
  margin: 0 5px;
  padding: 8px 12px;
  border: none;
  border-radius: 40px;
  background: ${({ active }) => (active ? '#3276b1' : '#ccc')};
  color: ${({ active }) => (active ? '#fff' : '#333333')};
  cursor: ${({ active }) => (active ? 'default' : 'pointer')};

  display: flex;
  align-items: center;

  > svg {
    margin-right: 5px;
  }

  @media screen and (min-width: 768px) {
    margin: 0 0 10px 0;
  }
`;

export const FeatureBox = styled.div`
  margin: 40px 0 0 0;
  padding: 30px;
  width: 100%;
  border-radius: 6px;
  background: #fff;
  box-shadow: 0 0 10px rgba(0, 0, 0, 0.15);

  > h2 {
    color: ${({ theme }) => theme.colors.headingColor};
  }

  > p {
    margin-top: 20px;
    line-height: 30px;

    > span {
      margin: 8px 0;

      display: block;
    }
  }

  @media screen and (min-width: 768px) {
    margin: 0 0 0 20px;
  }

  @media screen and (min-width: 1536px) {
    width: 800px;
  }
`;

export const FeatureButton = styled.button`
  margin: 20px auto 0 auto;
  padding: 12px 0;
  width: 120px;
  border: 1px solid ${({ theme }) => theme.colors.primaryColor};
  border-radius: 4px;
  background: ${({ theme }) => theme.colors.primaryColor};
  color: ${({ theme }) => theme.colors.ligthTextColor};
  cursor: pointer;
  transition: 0.3s;

  display: block;

  &:hover {
    background: ${({ theme }) => theme.colors.ligthTextColor};
    color: ${({ theme }) => theme.colors.primaryColor};
  }
`;
