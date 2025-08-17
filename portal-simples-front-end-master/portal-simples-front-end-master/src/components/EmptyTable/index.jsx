import PropTypes from 'prop-types';
import { Result } from 'antd';
import { FaFolderOpen } from 'react-icons/fa';

import { Container, Icon } from './styles';

const EmptyTable = ({ title }) => {
  return (
    <Container>
      <Result
        icon={
          <Icon>
            <FaFolderOpen size={42} color="#3276b1" />
          </Icon>
        }
        title={title}
      />
    </Container>
  );
};

EmptyTable.propTypes = {
  title: PropTypes.string.isRequired,
};

export default EmptyTable;
