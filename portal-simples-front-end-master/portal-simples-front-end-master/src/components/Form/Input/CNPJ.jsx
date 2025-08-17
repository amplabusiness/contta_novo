import MaskedInput from 'antd-mask-input';

export const CNPJInput = props => {
  return <MaskedInput mask="11.111.111/1111-11" {...props} />;
};
