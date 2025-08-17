import { useState } from 'react';
import { Button, Form, Modal } from 'antd';
import { differenceInMonths, parseISO } from 'date-fns';
import { useSelector } from 'react-redux';

import useNewCompanyAnnualIncome from '@/services/api/hooks/app/Dashboard/useNewCompanyAnnualIncome';

import OldCompany from './OldCompany';
import NewCompany from './NewCompany';

import { Container, CustomButton } from './styles';

const ProvideAnnualIncome = () => {
  const {
    id,
    data: { simplesNacional = {}, faturamentoAnual = {} },
  } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );

  const [isModalOpen, setIsModalOpen] = useState(false);

  const [form] = Form.useForm();

  const { mutate, isLoading } = useNewCompanyAnnualIncome();

  const { faturamentoFechado = false } = faturamentoAnual;

  // Diferença entre a data de referência e a data de fundação da empresa
  const { dateFounded } = simplesNacional;
  const differenceBetweenDates = differenceInMonths(
    parseISO(date),
    parseISO(dateFounded),
  );
  const isNewCompany =
    differenceBetweenDates >= 0 && differenceBetweenDates < 12;
  const isOldCompany = differenceBetweenDates >= 12;
  const invalidDate = differenceBetweenDates < 0;

  const openModal = () => setIsModalOpen(true);

  const closeModal = () => setIsModalOpen(false);

  const handleSubmit = companyAge => values => {
    const withoutTotalizer =
      companyAge === 'old'
        ? Object.entries(values).slice(0, -1)
        : Object.entries(values);

    const rawData = withoutTotalizer.map(([key, value]) => {
      const rawDate = `01/${key}`;
      // Ex. de valor do rawDate: 01/June/2019
      const parsedDate = new Date(rawDate);
      const incomeDate = parsedDate.toISOString();
      const incomeYear = parsedDate.getFullYear();

      return {
        dataFaturamento: incomeDate,
        ano: incomeYear,
        valorFaturamento: value,
      };
    });

    const data = {
      companyInformation: id,
      faturamentoFechado: true,
      fechamentoEmp: companyAge === 'new',
      faturamentos: rawData,
    };

    mutate(data);
  };

  if (faturamentoFechado) {
    return null;
  }

  return (
    <Container>
      <span>
        O sistema verificou que o faturamento anual da empresa ainda não foi
        informado
      </span>
      <CustomButton onClick={openModal} disabled={!clickedDownLoadButton}>
        Informar Faturamento
      </CustomButton>
      <Modal
        visible={isModalOpen}
        onCancel={closeModal}
        title="Faturamentos mensais da empresa"
        footer={
          invalidDate
            ? false
            : [
                <Button
                  key="cancel"
                  type="default"
                  onClick={closeModal}
                  disabled={isLoading}
                >
                  Cancelar
                </Button>,
                <Button
                  key="confirm"
                  type="primary"
                  onClick={() => {
                    form.submit();
                  }}
                  loading={isLoading}
                >
                  Enviar
                </Button>,
              ]
        }
        destroyOnClose
        width={800}
        okText="Enviar"
        bodyStyle={{ padding: '24px 24px 44px 24px' }}
      >
        {isNewCompany ? (
          <NewCompany form={form} handleSubmit={handleSubmit} />
        ) : isOldCompany ? (
          <OldCompany form={form} handleSubmit={handleSubmit} />
        ) : (
          <p>A data de referência é anterior à data de fundação da empresa.</p>
        )}
      </Modal>
    </Container>
  );
};

export default ProvideAnnualIncome;
