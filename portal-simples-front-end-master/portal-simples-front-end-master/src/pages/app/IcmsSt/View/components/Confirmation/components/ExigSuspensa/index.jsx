import { Form, notification, Result, Tabs } from 'antd';
import { IoAddCircle, IoList } from 'react-icons/io5';
import { useSelector } from 'react-redux';

import useIcmsStTaxes from '@/services/api/hooks/app/IcmsSt/useIcmsStTaxes';
import useNewIcmsStTax from '@/services/api/hooks/app/IcmsSt/useNewIcmsStTax';

import Shimmer from '@/components/Shimmer/IcmsSt';
import ErrorMessage from '@/components/ErrorMessage';

import ExigSuspensaForm from '@/pages/app/IcmsSt/View/components/Confirmation/components/ExigSuspensa/Form';
import Tables from '@/pages/app/IcmsSt/View/components/Confirmation/components/ExigSuspensa/Tables';

import { Container, TabTitle } from './styles';

const ExigSuspensa = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const [form] = Form.useForm();

  const query = useIcmsStTaxes('exigSuspensa');
  const { isLoading, isError, data } = query;

  const mutation = useNewIcmsStTax('exigSuspensa');

  if (isLoading) {
    return <Shimmer />;
  }

  if (isError) {
    return <ErrorMessage />;
  }

  const taxesAlreadySelected = data
    ? data
        .filter(item => item.status.toLowerCase() === 'ativo')
        .map(item => item.nomeImposto)
    : [];
  const areAllTaxesSelected = taxesAlreadySelected.length === 6;

  const handleSubmit = values => {
    const dataToSubmit = {
      ...values,
      companyInformation: id,
      numPocesso: values.numProcesso,
      dataInicial: values.dataInicial.toISOString(),
      dataFinal: values.dataFinal.toISOString(),
    };
    delete dataToSubmit.numProcesso;

    mutation.mutate(dataToSubmit, {
      onSuccess: () => {
        notification.success({
          message: 'Sucesso',
          description: 'Tabela cadastrada com sucesso!',
        });

        /* Verificação local se os impostos já estão todos cadastrados
         * Foi necessário fazer isso para evitar o problema de Memory Leak
         * que aconteceria caso tentasse executar o reset do form
         */
        const isNextTaxLast = taxesAlreadySelected.length === 5;

        if (!isNextTaxLast) {
          form.resetFields();
        }
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
              <IoAddCircle /> Novo Imposto
            </TabTitle>
          }
        >
          {areAllTaxesSelected ? (
            <Result
              title="Aviso importante"
              subTitle={
                <p style={{ fontSize: '1rem' }}>
                  Verificamos que a empresa já cadastrou todos os impostos como
                  Exigibilidade Suspensa. Para informar um novo, aguarde o
                  vencimento do imposto registrado.
                </p>
              }
            />
          ) : (
            <ExigSuspensaForm
              form={form}
              taxesAlreadySelected={taxesAlreadySelected}
              onSubmit={handleSubmit}
              isLoading={mutation.isLoading}
            />
          )}
        </Tabs.TabPane>
        <Tabs.TabPane
          key="2"
          tab={
            <TabTitle>
              <IoList /> Impostos Cadastrados
            </TabTitle>
          }
        >
          <Tables data={data} />
        </Tabs.TabPane>
      </Tabs>
    </Container>
  );
};

export default ExigSuspensa;
