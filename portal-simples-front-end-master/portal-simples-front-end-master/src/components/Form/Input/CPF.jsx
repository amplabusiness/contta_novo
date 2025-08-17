import MaskedInput from 'antd-mask-input';

export const CPFInput = props => {
  return <MaskedInput mask="111.111.111-11" {...props} />;
};
