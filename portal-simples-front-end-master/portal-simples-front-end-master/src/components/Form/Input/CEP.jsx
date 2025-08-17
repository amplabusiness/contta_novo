import MaskedInput from 'antd-mask-input';

export const CEPInput = props => {
  return <MaskedInput mask="11111-111" {...props} />;
};
