import { useRef, useState } from 'react';
import PropTypes from 'prop-types';
import { Button, Table } from 'antd';
import { FiArrowLeft, FiEye } from 'react-icons/fi';
import { useHistory } from 'react-router-dom';

import useMemberCompany from '@/services/api/hooks/app/EmpresasSocios/useMemberCompany';
import { empresasDadosSociosColumns } from '@/pages/app/EmpresasSocio/constants';

import Loading from '@/components/Loading';

import CompanyDetails from '@/pages/app/EmpresasSocio/View/components/CompanyDetails';

import { Container, Title } from '@/styles/global';
import { GoBackButton } from './styles';

const EmpresasSocioView = ({ data }) => {
  const [companyData, setCompanyData] = useState(null);

  const companyViewRef = useRef(null);

  const { goBack } = useHistory();

  const { isLoading, mutate } = useMemberCompany();

  const handleCompanySelection = cnpj => {
    mutate(cnpj, {
      onSuccess: response => {
        setCompanyData(response);

        if (companyViewRef.current) {
          companyViewRef.current.scrollIntoView({ behavior: 'smooth' });
        }
      },
    });
  };

  const columns = [
    ...empresasDadosSociosColumns,
    {
      title: 'Visualizar Empresa',
      dataIndex: 'action',
      key: 'action',
      align: 'center',
      render: (text, record) => (
        <Button
          type="text"
          style={{ padding: 0, height: 33 }}
          onClick={() => handleCompanySelection(record.cnpj)}
        >
          <FiEye size={18} color="#3276b1" />
        </Button>
      ),
    },
  ];

  return (
    <>
      {isLoading && <Loading />}
      <Container>
        <GoBackButton type="text" onClick={() => goBack()}>
          <FiArrowLeft size={18} color="#3276b1" />
          <p>Voltar</p>
        </GoBackButton>

        <Title>
          <h2>Empresas Vinculadas ao Sócio</h2>
          <p>
            Abaixo encontram-se todas as empresas vinculadas ao sócio
            selecionado.
          </p>
        </Title>

        <Table
          dataSource={data}
          columns={columns}
          pagination={false}
          size="small"
          rowKey="cnpj"
          style={{ marginTop: 30 }}
        />

        <CompanyDetails
          company={companyData}
          close={() => setCompanyData(null)}
          ref={companyViewRef}
        />
      </Container>
    </>
  );
};

EmpresasSocioView.propTypes = {
  data: PropTypes.array.isRequired,
};

export default EmpresasSocioView;
