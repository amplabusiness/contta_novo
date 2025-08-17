import type { ReactNode } from 'react';
import { Container } from './styles';

type Props = { children?: ReactNode };

const Layout = ({ children }: Props) => {
  return <Container>{children}</Container>;
};

export default Layout;
