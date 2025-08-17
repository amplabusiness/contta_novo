import styled from 'styled-components';

export const Container = styled.div`
  padding: 40px 0;
  width: 100%;
  background: #fff;

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

    flex-direction: row;
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

export const ImageContainer = styled.div`
  flex: 1;

  display: grid;
  place-items: center;
`;

export const Heading = styled.div`
  flex: 1;

  > h2 {
    margin-bottom: 15px;
    color: ${({ theme }) => theme.colors.headingColor};
    font-size: 2rem;
    text-align: center;

    @media screen and (min-width: 640px) {
      text-align: left;
    }
  }

  > p {
    color: #6a6870;
    line-height: 30px;
    text-align: center;

    @media screen and (min-width: 640px) {
      text-align: left;
    }
  }
`;
