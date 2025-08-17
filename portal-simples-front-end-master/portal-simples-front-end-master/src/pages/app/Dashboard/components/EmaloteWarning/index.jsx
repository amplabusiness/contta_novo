import { Result } from 'antd';
import { useSelector } from 'react-redux';

const EmaloteWarning = () => {
  const { name } = useSelector(state => state.activeCompanyState);

  return (
    <Result
      status="info"
      title="e-Malote não instalado"
      subTitle={
        <p style={{ fontSize: '1rem' }}>
          Antes de visualizar os dados da empresa {name} é necessário realizar o
          download do e-Malote. O botão para download se localiza no canto
          superior direito.
        </p>
      }
    />
  );
};

export default EmaloteWarning;
