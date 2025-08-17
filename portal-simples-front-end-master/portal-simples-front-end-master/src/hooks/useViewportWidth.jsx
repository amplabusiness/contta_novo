import { useState, useEffect } from 'react';

const useViewportWidth = () => {
  const [width, setWidth] = useState(window.innerWidth);
  const isMobile = width < 769;

  const handleWindowWidthResize = () => setWidth(window.innerWidth);

  useEffect(() => {
    window.addEventListener('resize', handleWindowWidthResize);

    return () => window.removeEventListener('resize', handleWindowWidthResize);
  }, []);

  return { width, isMobile };
};

export default useViewportWidth;
