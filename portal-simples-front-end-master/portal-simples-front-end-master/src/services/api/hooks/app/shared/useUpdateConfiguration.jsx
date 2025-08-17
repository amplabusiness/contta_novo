import { useMutation } from 'react-query';
import { useDispatch, useSelector } from 'react-redux';

import { putUpdateUserConfiguration } from '@/services/api/requests';
import { setConfig } from '@/store/slices/configurations';

const useUpdateConfiguration = configName => {
  const configs = useSelector(state => state.configurationsState);
  const dispatch = useDispatch();

  const updatedConfig = {
    ...configs,
    [configName]: true,
  };

  const mutation = useMutation(
    () => putUpdateUserConfiguration(updatedConfig),
    {
      onSuccess: () => {
        dispatch(setConfig(updatedConfig));
      },
    },
  );

  return mutation;
};

export default useUpdateConfiguration;
