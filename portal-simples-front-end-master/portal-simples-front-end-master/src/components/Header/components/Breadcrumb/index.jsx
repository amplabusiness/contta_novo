import { Link, useLocation } from 'react-router-dom';

import { getBreadcrumbTitle } from '@/components/Header/components/Breadcrumb/constants';

import { CustomBreadcrumb } from './styles';

const Breadcrumb = () => {
  const { pathname } = useLocation();

  const pathNames = pathname.split('/').filter(item => item);

  /*
   * Verifica se estou na tela de edição de empresa
   * Isso é feito por que essa tela recebe um id, ficando edicaoEmpresa/:id
   * Como eu dividi o pathname pela /, esse id ficaria como uma rota
   * Esse if basicamente junta a edicaoEmpresa com seu id novamente
   * E o coloca na segunda posição do array de pathNames
   */
  let serializedPathNames = [];
  if (
    pathNames.includes('dadosEmpresa') ||
    pathNames.includes('edicaoUsuario')
  ) {
    const newEmpresaEditPath = pathNames.slice(1, 3).join('/');
    const filteredPaths = pathNames.filter(
      (item, index) => index !== 1 && index !== 2,
    );
    serializedPathNames = [
      ...filteredPaths.slice(0, 1),
      newEmpresaEditPath,
      ...filteredPaths.slice(1),
    ];
  } else {
    serializedPathNames = pathNames;
  }

  return (
    <CustomBreadcrumb>
      {serializedPathNames.map((name, index) => {
        const routeTo = `/${serializedPathNames.slice(0, index + 1).join('/')}`;
        const isLast = index === serializedPathNames.length - 1;
        return isLast ? (
          <CustomBreadcrumb.Item key={name}>
            <span>{getBreadcrumbTitle(name)}</span>
          </CustomBreadcrumb.Item>
        ) : (
          <CustomBreadcrumb.Item key={name}>
            <Link to={routeTo}>{getBreadcrumbTitle(name)}</Link>
          </CustomBreadcrumb.Item>
        );
      })}
    </CustomBreadcrumb>
  );
};

export default Breadcrumb;
