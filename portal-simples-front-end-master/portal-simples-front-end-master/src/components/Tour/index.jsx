import PropTypes from 'prop-types';
import { disableBodyScroll, enableBodyScroll } from 'body-scroll-lock';
import ReactTour from 'reactour';

import { LastStepSpan } from './styles';

const Tour = ({ isActive, steps, onEnd }) => {
  const onAfterOpen = target => disableBodyScroll(target);

  const onBeforeClose = target => enableBodyScroll(target);

  return (
    <ReactTour
      isOpen={isActive}
      steps={steps}
      onRequestClose={onEnd}
      onAfterOpen={onAfterOpen}
      onBeforeClose={onBeforeClose}
      lastStepNextButton={<LastStepSpan>Finalizar</LastStepSpan>}
      accentColor="#3276b1"
      disableInteraction
    />
  );
};

Tour.propTypes = {
  isActive: PropTypes.bool.isRequired,
  steps: PropTypes.array.isRequired,
  onEnd: PropTypes.func.isRequired,
};

export default Tour;
