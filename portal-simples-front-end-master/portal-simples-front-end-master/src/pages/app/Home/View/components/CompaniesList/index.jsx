import { useState } from 'react';
import { Checkbox, Input, Table } from 'antd';
import PropTypes from 'prop-types';
import { FaRegFilePdf } from 'react-icons/fa';
import { FiCheck } from 'react-icons/fi';
import { HiSearch } from 'react-icons/hi';
import { useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';

import { changeCompanySE } from '@/store/slices/activeCompany';
import {
  homeResumoEmpresasColumns,
  spreadsheetHeaders,
} from '@/pages/app/Home/constants';

import ExportXLSX from '@/components/ExportXLSX';

import { CompanyName, IconButton } from './styles';

const CompaniesList = ({ companies }) => {
  const dispatch = useDispatch();

  const [filteredCompanies, setFilteredCompanies] = useState(companies);

  const { push } = useHistory();

  const handleSearch = e => {
    const searchTerm = e.target.value;

    if (!searchTerm) {
      setFilteredCompanies(companies);
    }

    setFilteredCompanies(
      companies.filter(company =>
        company.razaoSocial.toLowerCase().includes(searchTerm.toLowerCase()),
      ),
    );
  };

  const handleCompanySelection = id => {
    dispatch(changeCompanySE(id));

    push('/dashboard');
  };

  const columns = [
    {
      title: 'Status',
      dataIndex: 'status',
      key: 'status',
      render: (text, record) => <Checkbox checked={text} />,
    },
    {
      title: 'Razão Social',
      dataIndex: 'razaoSocial',
      key: 'razaoSocial',
      width: 300,
      render: (text, record) => (
        <CompanyName
          type="button"
          onClick={() => handleCompanySelection(record.companyId)}
        >
          {text}
        </CompanyName>
      ),
    },
    ...homeResumoEmpresasColumns,
    {
      title: 'DIFAL',
      dataIndex: 'difal',
      key: 'difal',
      align: 'center',
      render: (text, record) => (
        <IconButton background="#B30B00" disabled={!text}>
          <FaRegFilePdf size={12} />
          Baixar
        </IconButton>
      ),
    },
    {
      title: 'Declaração',
      dataIndex: 'declaracao',
      key: 'declaracao',
      align: 'center',
      render: (text, record) => (
        <IconButton background="#B30B00" disabled={!text}>
          <FaRegFilePdf size={12} />
          Baixar
        </IconButton>
      ),
    },
    {
      title: 'DAS',
      dataIndex: 'das',
      key: 'das',
      align: 'center',
      render: (text, record) => (
        <IconButton background="#38B000" disabled={!text}>
          <FiCheck size={12} />
          Emitida
        </IconButton>
      ),
    },
    {
      title: 'Extrato',
      dataIndex: 'extrato',
      key: 'extrato',
      align: 'center',
      render: (text, record) => (
        <IconButton background="#B30B00" disabled={!text}>
          <FaRegFilePdf size={12} />
          Baixar
        </IconButton>
      ),
    },
  ];

  return (
    <>
      <div className="table-manipulation">
        <Input
          onChange={handleSearch}
          addonBefore={<HiSearch size={16} style={{ marginTop: 5 }} />}
          placeholder="Filtre por Nome"
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

      <Table
        columns={columns}
        dataSource={filteredCompanies}
        size="small"
        pagination={{
          pageSize: 10,
          showSizeChanger: false,
        }}
        scroll={{ x: 'max-content' }}
        rowKey="companyId"
        rowClassName="table-row"
      />
    </>
  );
};

CompaniesList.propTypes = {
  companies: PropTypes.array.isRequired,
};

export default CompaniesList;
