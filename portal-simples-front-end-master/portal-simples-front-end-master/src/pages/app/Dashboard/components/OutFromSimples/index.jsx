import { Result } from 'antd';

const OutFromSimples = () => {
  return (
    <Result
      status="error"
      title="Empresa fora do Simples Nacional"
      subTitle={
        <p style={{ fontSize: '1rem' }}>
          O faturamento anual da empresa estourou o limite do Simples Nacional.
          Portanto, ela já não pode ser considerada uma empresa do Simples.
        </p>
      }
    />
  );
};

export default OutFromSimples;
