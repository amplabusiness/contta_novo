import PropTypes from 'prop-types';
import { Form as AntForm } from 'antd';

const Form = ({ children, ...rest }) => {
  const formLayout = {
    wrapperCol: {
      span: 24,
    },
  };

  return (
    <AntForm
      {...formLayout}
      {...rest}
      layout="vertical"
      size="large"
      requiredMark={false}
    >
      {children}
    </AntForm>
  );
};

Form.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Form;
