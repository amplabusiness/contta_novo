import PropTypes from 'prop-types';

const Step = ({ children }) => {
  return <>{children}</>;
};

Step.propTypes = {
  children: PropTypes.node.isRequired,
};

export default Step;
