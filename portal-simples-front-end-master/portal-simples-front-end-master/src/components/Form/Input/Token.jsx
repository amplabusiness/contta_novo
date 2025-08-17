import MaskedInput from 'antd-mask-input';

export const TokenInput = props => {
  return <MaskedInput mask="11111111" {...props} />;
};
