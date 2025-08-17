import { useCallback } from 'react';
import debounce from 'lodash.debounce';

const useDebounce = delay => {
  // eslint-disable-next-line
  const debouncedFunction = useCallback(
    debounce(fn => fn(), delay),
    [],
  );

  return [debouncedFunction];
};

export default useDebounce;
