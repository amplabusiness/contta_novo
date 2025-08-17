import { useState, useEffect } from 'react';
import { Card, Popconfirm, Table, Tooltip } from 'antd';
import { FaBook, FaBookOpen, FaEye, FaTimes } from 'react-icons/fa';
import { HiSearch } from 'react-icons/hi';
import { useSelector, useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';

import {
  empresasCadastradasColumns,
  spreadsheetHeaders,
} from '@/pages/app/Empresas/constants';
import useCompanies from '@/services/api/hooks/app/Empresas/useCompanies';
import useDeleteCompany from '@/services/api/hooks/app/Empresas/useDeleteCompany';
import { resetState } from '@/store/slices/activeCompany';

import ExportXLSX from '@/components/ExportXLSX';
import Shimmer from '@/components/Shimmer/Empresas';
import EmptyTable from '@/components/EmptyTable';

import {
  Container,
  CustomInput,
  CompanyName,
  DeleteCompanyButton,
} from './styles';

const CompaniesList = () => {
  const { id } = useSelector(state => state.activeCompanyState);
  const { companies } = useSelector(state => state.companiesState);
  const dispatch = useDispatch();

  const [filteredCompanies, setFilteredCompanies] = useState([]);

  const query = useCompanies({ executeOnSuccessCallback: false });
  const mutation = useDeleteCompany();

  useEffect(() => {
    setFilteredCompanies(companies);
  }, [companies]);

  const handleSearch = e => {
    const searchTerm = e.target.value;

    if (!searchTerm) {
      setFilteredCompanies(companies);
      return;
    }

    setFilteredCompanies(
      companies.filter(
        company =>
          company.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
          company.cnpj.includes(searchTerm),
      ),
    );
  };

  const handleCompanyDelete = companyId => {
    mutation.mutate(companyId, {
      onSuccess: () => {
        if (companyId === id) {
          dispatch(resetState());
        }
      },
    });
  };

  const mainColumns = [
    {
      title: 'Razão Social',
      dataIndex: 'name',
      key: 'name',
      width: 300,
      render: (text, record) => <CompanyName>{text}</CompanyName>,
    },
    ...empresasCadastradasColumns,
    {
      title: 'Ações',
      key: 'action',
      width: '10%',
      render: (text, record) => (
        <>
          <Tooltip title="Visualizar">
            <Link to={`empresas/dadosEmpresa/${record.id}`}>
              <FaEye size={18} color="#1394ba" />
            </Link>
          </Tooltip>
          {record.active ? (
            <Tooltip title="Status: Ativa">
              <FaBookOpen color="#25e014" />
            </Tooltip>
          ) : (
            <Tooltip title="Status: Desativada">
              <FaBook color="#eb9f07" />
            </Tooltip>
          )}
          <Popconfirm
            title={<span>Tem certeza que deseja deletar essa empresa?</span>}
            placement="left"
            onConfirm={() => handleCompanyDelete(record.id)}
          >
            <DeleteCompanyButton type="button">
              <FaTimes size={18} color="#ff0000" className="delete-company" />
            </DeleteCompanyButton>
          </Popconfirm>
        </>
      ),
    },
  ];

  const isLoading = query.isLoading || query.isFetching;

  if (isLoading) {
    return <Shimmer />;
  }

  return companies.length > 0 ? (
    <Container>
      <div className="table-manipulation">
        <CustomInput
          placeholder="Filtre por nome ou CNPJ"
          addonBefore={<HiSearch size={16} style={{ marginTop: 5 }} />}
          onChange={handleSearch}
        />
        <ExportXLSX
          data={[
            {
              headers: spreadsheetHeaders,
              items: companies,
              name: 'Empresas',
            },
          ]}
        />
      </div>
      <Card bodyStyle={{ padding: '10px 20px' }}>
        <Table
          columns={mainColumns}
          dataSource={filteredCompanies}
          pagination={{
            pageSize: 5,
            showSizeChanger: false,
          }}
          rowKey="id"
          size="small"
          scroll={{ x: 'max-content' }}
          style={{ minHeight: 250 }}
        />
      </Card>
    </Container>
  ) : (
    <EmptyTable title="Nenhuma empresa cadastrada" />
  );
};

export default CompaniesList;
