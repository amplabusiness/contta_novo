import { useEffect, useState } from 'react';
import PropTypes from 'prop-types';
import { Card, Checkbox, Input, Table, Tooltip } from 'antd';
import { FiEdit } from 'react-icons/fi';
import { HiSearch } from 'react-icons/hi';
import { Link } from 'react-router-dom';

import {
  usuariosCadastradosColumns,
  spreadsheetHeaders,
} from '@/pages/app/UsuariosVinculados/constants';

import ExportXLSX from '@/components/ExportXLSX';

import { Container, TableManipulation } from './styles';

const UsersList = ({ users }) => {
  const [filteredUsers, setFilteredUsers] = useState(users);

  useEffect(() => {
    setFilteredUsers(users);
  }, [users]);

  const handleSearch = e => {
    const searchTerm = e.target.value;

    if (!searchTerm) {
      setFilteredUsers(users);
    }

    setFilteredUsers(
      users.filter(company =>
        company.name.toLowerCase().includes(searchTerm.toLowerCase()),
      ),
    );
  };

  const mainColumns = [
    ...usuariosCadastradosColumns,
    {
      title: 'Empresa',
      dataIndex: 'company',
      key: 'company',
      align: 'center',
      render: (text, record) => <Checkbox />,
    },
    {
      title: 'Ações',
      dataIndex: 'actions',
      key: 'actions',
      align: 'center',
      render: (text, record) => (
        <Tooltip title="Editar usuário">
          <Link to={`/usuarios/edicaoUsuario/${record.id}`}>
            <FiEdit size={18} />
          </Link>
        </Tooltip>
      ),
    },
  ];

  return (
    <Container>
      <TableManipulation>
        <Input
          onChange={handleSearch}
          addonBefore={<HiSearch size={16} style={{ marginTop: 5 }} />}
          placeholder="Filtre por Nome"
        />
        <ExportXLSX
          data={[
            {
              headers: spreadsheetHeaders,
              items: users,
              name: 'Usuários',
            },
          ]}
        />
      </TableManipulation>

      <Card bodyStyle={{ padding: '10px 20px' }}>
        <Table
          columns={mainColumns}
          dataSource={filteredUsers}
          pagination={{ pageSize: 5 }}
          size="small"
          scroll={{ x: 'max-content' }}
          rowKey="id"
        />
      </Card>
    </Container>
  );
};

UsersList.propTypes = {
  users: PropTypes.array.isRequired,
};

export default UsersList;
