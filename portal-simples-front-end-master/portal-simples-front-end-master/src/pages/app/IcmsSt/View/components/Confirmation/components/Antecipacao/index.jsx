import { Form, notification, Tabs } from 'antd';
import { IoAddCircle, IoList } from 'react-icons/io5';
import { useSelector } from 'react-redux';

import useIcmsStTaxes from '@/services/api/hooks/app/IcmsSt/useIcmsStTaxes';
import useNewIcmsStTax from '@/services/api/hooks/app/IcmsSt/useNewIcmsStTax';

import ErrorMessage from '@/components/ErrorMessage';
import Shimmer from '@/components/Shimmer/IcmsSt';

import AntecipacaoForm from '@/pages/app/IcmsSt/View/components/Confirmation/components/Antecipacao/Form';
import Tables from '@/pages/app/IcmsSt/View/components/Confirmation/components/Antecipacao/Tables';

import { Container, TabTitle } from './styles';

const Antecipacao = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [form] = Form.useForm();

  const query = useIcmsStTaxes('antEncTributacao');
  const { isLoading, isError, data } = query;

  const mutation = useNewIcmsStTax('antEncTributacao');

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  const ncmsAlreadySelected = data
    ? data
        .filter(item => item.status.toLowerCase() === 'ativo')
        .map(item => item.ncm)
    : [];

  const handleSubmit = values => {
    const dataToSubmit = {
      ListImpostoAntecipacao: [
        {
          ...values,
          companyInformation: id,
        },
      ],
    };

    mutation.mutate(dataToSubmit, {
      onSuccess: () => {
        notification.success({
          message: 'Sucesso',
          description: 'Tabela cadastrada com sucesso!',
        });

        form.resetFields();
      },
      onError: () => {
        notification.error({
          message: 'Erro',
          description: 'Não foi possível cadastrar a tabela no momento.',
        });
      },
    });
  };

  return (
    <Container>
      <Tabs defaultActiveKey="1" style={{ overflow: 'visible' }}>
        <Tabs.TabPane
          key="1"
          tab={
            <TabTitle>
              <IoAddCircle /> Novo NCM
            </TabTitle>
          }
        >
          <AntecipacaoForm
            form={form}
            ncmsAlreadySelected={ncmsAlreadySelected}
            onSubmit={handleSubmit}
            isLoading={mutation.isLoading}
          />
        </Tabs.TabPane>
        <Tabs.TabPane
          key="2"
          tab={
            <TabTitle>
              <IoList /> NCMs Cadastrados
            </TabTitle>
          }
        >
          <Tables data={data} />
        </Tabs.TabPane>
      </Tabs>
    </Container>
  );
};

export default Antecipacao;
