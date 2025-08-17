import { forwardRef, Fragment } from 'react';
import PropTypes from 'prop-types';
import { Button, Descriptions, notification } from 'antd';

import useNewCompany from '@/services/api/hooks/app/Empresas/useNewCompany';
import {
  cepFormatter,
  cpfCnpjFormatter,
  dateFormatter,
} from '@/utils/formatters';

import { Container, Footer } from './styles';

const CompanyDetails = forwardRef(({ company = null, close }, ref) => {
  const { isLoading, mutate } = useNewCompany();

  if (!company) {
    return null;
  }

  const {
    name,
    nameFantasy,
    cnpj,
    founded,
    email,
    phone,
    primaryActivity,
    regimeBox,
    regimeCompetence,
    address,
    membership,
  } = company;

  const formattedCnpj = cpfCnpjFormatter(cnpj);
  const formattedFoundationDate = dateFormatter(founded);

  const { zip, street, number, details, neighborhood, city, state } = address;
  const formattedZip = cepFormatter(zip);
  const formattedAddress = `${street ?? ''} ${number ?? ''} ${details ?? ''}, ${
    neighborhood ?? ''
  }, ${city ?? ''}, ${state ?? ''}`;

  const { code, description } = primaryActivity;
  const formattedPrimaryActivity = `${code ?? ''} - ${description ?? ''}`;

  const companyRegime = regimeBox
    ? 'Caixa'
    : regimeCompetence
    ? 'Competência'
    : 'Nenhum regime';

  const handleCompanyRegister = () => {
    mutate(cnpj, {
      onSuccess: () => {
        notification.success({
          message: 'Success',
          description: 'Empresa cadastrada com sucesso!',
        });

        close();
      },
      onError: () => {
        notification.error({
          message: 'Erro',
          description: 'Não foi possível cadastrar a empresa no momento.',
        });
      },
    });
  };

  return (
    <Container ref={ref}>
      <Descriptions
        title="Dados da Empresa"
        bordered
        layout="vertical"
        column={{ xxl: 4, xl: 3, lg: 3, md: 3, sm: 2, xs: 1 }}
      >
        <Descriptions.Item label="Razão Social">{name}</Descriptions.Item>
        <Descriptions.Item label="Nome Fantasia">
          {nameFantasy}
        </Descriptions.Item>
        <Descriptions.Item label="CNPJ">{formattedCnpj}</Descriptions.Item>
        <Descriptions.Item label="Data de Fundação">
          {formattedFoundationDate}
        </Descriptions.Item>
        <Descriptions.Item label="Atividade Primária">
          {formattedPrimaryActivity}
        </Descriptions.Item>
        <Descriptions.Item label="Regime">{companyRegime}</Descriptions.Item>
        <Descriptions.Item label="Email">{email}</Descriptions.Item>
        <Descriptions.Item label="Telefone">{phone}</Descriptions.Item>
        <Descriptions.Item label="CEP">{formattedZip}</Descriptions.Item>
        <Descriptions.Item label="Endereço" span={3}>
          {formattedAddress}
        </Descriptions.Item>
      </Descriptions>

      <Descriptions
        title="Sócios da Empresa"
        bordered
        layout="vertical"
        column={{ xxl: 3, xl: 3, lg: 3, md: 3, sm: 2, xs: 1 }}
      >
        {membership.map(member => {
          const { name: nameSocio, cpfSocio, role } = member;
          const { description: descriptionSocio } = role;

          return (
            <Fragment key={cpfSocio}>
              <Descriptions.Item label="Nome do Sócio">
                {nameSocio}
              </Descriptions.Item>
              <Descriptions.Item label="CPF do Sócio">
                {cpfSocio}
              </Descriptions.Item>
              <Descriptions.Item label="Função do Sócio">
                {descriptionSocio}
              </Descriptions.Item>
            </Fragment>
          );
        })}
      </Descriptions>

      <Footer>
        <p>
          Para visualizar todos os dados dessa empresa, é necessário efetuar o
          cadastro da mesma.
        </p>
        <Button
          type="primary"
          onClick={handleCompanyRegister}
          disabled={isLoading}
        >
          Cadastrar empresa
        </Button>
      </Footer>
    </Container>
  );
});

CompanyDetails.propTypes = {
  company: PropTypes.object,
  close: PropTypes.func.isRequired,
};

CompanyDetails.defaultProps = {
  company: null,
};

export default CompanyDetails;
