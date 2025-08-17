import MaskedInput from 'antd-mask-input';

export const MunicipalRegistrationInput = props => {
  return <MaskedInput mask="11111111" {...props} />;
};
