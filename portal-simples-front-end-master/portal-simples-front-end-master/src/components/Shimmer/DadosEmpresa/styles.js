import styled from 'styled-components';

export const Container = styled.div`
  padding-bottom: 80px;

  .primary-box,
  .secondary-box {
    margin-top: 30px;
    border-radius: 6px;
  }

  .primary-box {
    &.first {
      height: 600px;
    }

    &.second {
      height: 433px;
    }

    &.third {
      height: 265px;
    }
  }

  .secondary-box {
    &.first {
      height: 265px;
    }

    &.second {
      height: 265px;
    }

    &.third {
      height: 178px;
    }
  }
`;
