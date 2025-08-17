import { useEffect, useState } from 'react';
import {
  Button,
  Card,
  Col,
  Divider,
  Form as AntForm,
  InputNumber,
  Modal,
  Popconfirm,
  Row,
  Space,
  Table,
  Tooltip,
} from 'antd';
import { FaFileInvoice } from 'react-icons/fa';
import { IoChevronUp, IoChevronDown, IoPencil } from 'react-icons/io5';
import { useSelector } from 'react-redux';

import useInvoicesQuantity from '@/services/api/hooks/app/Dashboard/useInvoicesQuantity';
import { dashboardInvoicesQuantityColumns } from '@/pages/app/Dashboard/constants';

import Form from '@/components/Form';
import { ShowQuantityButton } from './styles';

const { Item: FormItem } = AntForm;

const InvoicesQuantity = () => {
  const { clickedDownLoadButton } = useSelector(
    state => state.configurationsState,
  );

  const [isVisible, setIsVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [dataSource, setDataSource] = useState([]);

  const { data, isLoading } = useInvoicesQuantity({ enabled: isVisible });

  useEffect(() => {
    setDataSource(data);
  }, [data]);

  const openModal = () => {
    setIsVisible(true);
  };

  const closeModal = () => {
    setIsVisible(false);
  };

  const updateUnusableInvoices = values => {
    console.log(values);

    setIsEditing(false);
  };

  const columns = [
    ...dashboardInvoicesQuantityColumns,
    {
      title: 'Ações',
      dataIndex: 'actions',
      key: 'actions',
      render: (record, text) => (
        <Tooltip title="Editar notas inutilizadas">
          <Button
            type="primary"
            shape="circle"
            size="small"
            onClick={() => setIsEditing(prevState => !prevState)}
          >
            <IoPencil size={12} color="#fff" />
          </Button>
        </Tooltip>
      ),
    },
  ];

  if (!clickedDownLoadButton) {
    return null;
  }

  return (
    <>
      <Tooltip title="Quantidade de Notas Fiscais">
        <ShowQuantityButton onClick={openModal}>
          <FaFileInvoice size={18} color="#fff" />
        </ShowQuantityButton>
      </Tooltip>
      <Modal
        title="Quantidade de Notas Fiscais"
        visible={isVisible}
        onOk={closeModal}
        onCancel={closeModal}
        footer={false}
        width={900}
        destroyOnClose
        bodyStyle={{ padding: '44px 24px' }}
      >
        <Table
          columns={columns}
          dataSource={dataSource}
          size="small"
          rowKey="id"
          pagination={false}
          loading={isLoading}
          expandable={{
            expandedRowRender: record => {
              const { numSaidas } = record;

              return (
                <Card>
                  <strong
                    style={{
                      marginBottom: 20,
                      fontSize: '1rem',
                      display: 'block',
                    }}
                  >
                    Numerações das notas
                  </strong>
                  <Space size="large" wrap>
                    {numSaidas.map(item => (
                      <span key={item}>{item}</span>
                    ))}
                  </Space>
                </Card>
              );
            },
            // eslint-disable-next-line
            expandIcon: ({ expanded, onExpand, record }) => (
              <Button size="small" shape="circle">
                {expanded ? (
                  <IoChevronUp onClick={e => onExpand(record, e)} />
                ) : (
                  <IoChevronDown onClick={e => onExpand(record, e)} />
                )}
              </Button>
            ),
          }}
          scroll={{ x: 'max-content' }}
        />

        {isEditing && (
          <>
            <Divider>Edição</Divider>

            <Form
              name="unusable-invoices-form"
              initialValues={{
                unusableInvoicesCount: 0,
              }}
              onFinish={updateUnusableInvoices}
            >
              <Row gutter={[24, 24]}>
                <Col xs={24} md={8}>
                  <FormItem
                    name="unusableInvoicesCount"
                    label="Notas Inutilizadas"
                  >
                    <InputNumber style={{ width: '100%' }} />
                  </FormItem>
                </Col>

                <Col
                  xs={24}
                  md={16}
                  style={{
                    marginTop: 16,
                    display: 'flex',
                    gap: 24,
                    alignItems: 'center',
                  }}
                >
                  <FormItem noStyle>
                    <Button type="primary" htmlType="submit">
                      Confirmar
                    </Button>

                    <Popconfirm
                      title="Tem certeza em cancelar a edição?"
                      onConfirm={() => setIsEditing(false)}
                      okText="Sim"
                      cancelText="Não"
                    >
                      <Button>Cancelar Edição</Button>
                    </Popconfirm>
                  </FormItem>
                </Col>
              </Row>
            </Form>
          </>
        )}
      </Modal>
    </>
  );
};

export default InvoicesQuantity;
