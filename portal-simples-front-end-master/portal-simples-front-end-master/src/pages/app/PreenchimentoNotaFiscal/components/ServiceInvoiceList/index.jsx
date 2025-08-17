import { useState, useEffect } from 'react';
import { Button, Col, Input, Popconfirm, Row, Table } from 'antd';
import { HiPencil, HiSearch, HiX } from 'react-icons/hi';
import { Link } from 'react-router-dom';

import useManualServiceInvoices from '@/services/api/hooks/app/PreenchimentoNotaFiscal/useManualServiceInvoices';
import useDeleteServiceInvoice from '@/services/api/hooks/app/PreenchimentoNotaFiscal/useDeleteServiceInvoice';
import { serviceInvoicesColumns } from '@/pages/app/PreenchimentoNotaFiscal/constants';

import EmptyTable from '@/components/EmptyTable';
import Shimmer from '@/components/Shimmer/PreenchimentoNotaFiscal/ListaNotasFiscais';

import { Title } from '@/styles/global';

const ServiceInvoiceList = () => {
  const [totalData, setTotalData] = useState([]);
  const [data, setData] = useState([]);

  const query = useManualServiceInvoices();
  const { data: queryData, isLoading } = query;
  const mutation = useDeleteServiceInvoice();

  useEffect(() => {
    if (queryData) {
      setTotalData(queryData.result);
      setData(queryData.result);
    }
  }, [queryData]);

  const handleFilter = event => {
    const { value } = event.target;

    if (!value) {
      setData(totalData);
      return;
    }

    const filteredData = totalData.filter(item => {
      const nameFilter = item.taker.name
        .toLowerCase()
        .startsWith(value.toLowerCase());
      const cpfCnpjFilter = item.taker.cnpj_Cpf.startsWith(value);

      return nameFilter || cpfCnpjFilter;
    });
    setData(filteredData);
  };

  const handleDeleteInvoice = id => {
    mutation.mutate(id);
  };

  const mainColumns = [
    ...serviceInvoicesColumns,
    {
      title: 'Ações',
      dataIndex: 'actions',
      key: 'actions',
      render: (text, record) => (
        <>
          <Button
            type="text"
            style={{ marginRight: 5, padding: 0, height: '100%' }}
          >
            <Link to={{ pathname: '/modeloNotaFiscal/servico', state: record }}>
              <HiPencil size={20} color="#3276b1" />
            </Link>
          </Button>
          <Popconfirm
            title="Tem certeza que deseja excluir essa nota fiscal?"
            placement="bottomLeft"
            onConfirm={() => handleDeleteInvoice(record.id)}
            okText="Sim"
          >
            <Button type="text" style={{ padding: 0, height: '100%' }}>
              <HiX size={20} color="#3276b1" />
            </Button>
          </Popconfirm>
        </>
      ),
    },
  ];

  if (isLoading) {
    return <Shimmer />;
  }

  return (
    <>
      <Title>
        <h2>Notas Fiscais de Serviço</h2>
        <p>
          Abaixo encontram-se todas as notas fiscais de serviço cadastradas por
          você. Caso queira excluir alguma, basta clicar no X.
        </p>
      </Title>
      {totalData.length > 0 ? (
        <>
          <Row gutter={[24, 0]}>
            <Col xs={24} md={8}>
              <Input
                onChange={handleFilter}
                placeholder="Filtre pelo CNPJ ou pelo Nome"
                addonBefore={<HiSearch size={16} style={{ marginTop: 5 }} />}
                style={{ marginTop: 14 }}
              />
            </Col>
          </Row>

          <Table
            columns={mainColumns}
            dataSource={data}
            size="small"
            rowKey="id"
            loading={query.isLoading}
            pagination={{ pageSize: 5 }}
            scroll={{ x: 'max-content' }}
            style={{ margin: '20px 0' }}
          />
        </>
      ) : (
        <EmptyTable title="Nenhuma nota manual de serviço encontrada" />
      )}
    </>
  );
};

export default ServiceInvoiceList;
