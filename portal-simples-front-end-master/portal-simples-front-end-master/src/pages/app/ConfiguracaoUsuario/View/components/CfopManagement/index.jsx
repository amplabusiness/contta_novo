import { useState, useEffect } from 'react';
import {
  Button,
  Col,
  Divider,
  Input,
  Form as AntForm,
  Modal,
  Popconfirm,
  Row,
  Select,
  Table,
} from 'antd';
import { HiSearch, HiX } from 'react-icons/hi';

import useRegisteredCfops from '@/services/api/hooks/app/ConfiguracaoUsuario/useRegisteredCfops';
import useNewCfop from '@/services/api/hooks/app/ConfiguracaoUsuario/useNewCfop';
import useDeleteCfop from '@/services/api/hooks/app/ConfiguracaoUsuario/useDeleteCfop';
import { useConfiguracaoUsuarioContext } from '@/contexts/ConfiguracaoUsuarioContext';
import { registeredCfopsColumns } from '@/pages/app/ConfiguracaoUsuario/constants';
import { capitalizeWords } from '@/utils/formatters';

import Form from '@/components/Form';

import { Title } from '@/styles/global';
import { Content } from '@/pages/app/ConfiguracaoUsuario/View/styles';

const { Item: FormItem } = AntForm;

const CfopManagement = () => {
  const [visible, setVisible] = useState(false);
  const [totalData, setTotalData] = useState([]);
  const [results, setResults] = useState([]);

  const {
    state: { cfops },
  } = useConfiguracaoUsuarioContext();

  const [form] = AntForm.useForm();

  const query = useRegisteredCfops();
  const newCfopMutation = useNewCfop();
  const deleteCfopMutation = useDeleteCfop();

  useEffect(() => {
    if (query.data) {
      const sortedCfops = query.data.sort((a, b) => a.cfop - b.cfop);

      setTotalData(sortedCfops);
      setResults(sortedCfops);
    }
  }, [query.data]);

  const openModal = () => {
    setVisible(true);
  };

  const closeModal = () => {
    setVisible(false);
  };

  const handleSubmitNewCfop = async values => {
    const data = {
      cfop: Number(values.code),
      descricao: values.description,
    };

    await newCfopMutation.mutateAsync(data, {
      onSuccess: () => {
        form.resetFields();
      },
    });
  };

  const handleFilter = event => {
    const { value } = event.target;

    if (!value) {
      setResults(totalData);
      return;
    }

    const filteredData = totalData.filter(item => {
      const parsedCfop = String(item.cfop);

      return parsedCfop.toLowerCase().startsWith(value.toLowerCase());
    });
    setResults(filteredData);
  };

  const handleDeleteCfop = id => {
    deleteCfopMutation.mutate(id);
  };

  const mainColumns = [
    ...registeredCfopsColumns,
    {
      title: 'Ações',
      dataIndex: 'actions',
      key: 'actions',
      render: (text, record) => (
        <Popconfirm
          title="Tem certeza que deseja excluir esse CFOP?"
          placement="topLeft"
          onConfirm={() => handleDeleteCfop(record.id)}
          okText="Sim"
        >
          <Button type="text" style={{ padding: 0, height: '100%' }}>
            <HiX size={20} color="#3276b1" />
          </Button>
        </Popconfirm>
      ),
    },
  ];

  const codeOptions = cfops.map(item => ({
    key: String(item.codigo),
    label: String(item.codigo),
    value: String(item.codigo),
  }));

  return (
    <Col xs={24} md={12} style={{ marginTop: 20 }}>
      <Title>
        <h2>CFOPs na Base do Simples Nacional</h2>
        <p>
          Clique no botão abaixo para gerenciar os CFOPs presentes na base do
          Simples Nacional. Você conseguirá adicionar novos ou excluir os já
          registrados.
        </p>
      </Title>
      <Content>
        <Button type="primary" onClick={openModal} style={{ marginTop: 20 }}>
          Gerenciar
        </Button>

        <Modal
          visible={visible}
          title="Gerenciamento de CFOPs"
          onCancel={closeModal}
          footer={false}
          width="100%"
          destroyOnClose
        >
          <Divider orientation="left">Registrar CFOP</Divider>
          <Form
            name="add-cfop-form"
            form={form}
            initialValues={{
              code: '',
              description: '',
            }}
            onFinish={handleSubmitNewCfop}
          >
            <Row gutter={[24, 0]}>
              <Col xs={24} md={4}>
                <FormItem
                  name="code"
                  label="Código"
                  rules={[
                    {
                      required: true,
                      message: 'Campo obrigatório',
                    },
                  ]}
                >
                  <Select
                    onSelect={value => {
                      const { descricao } = cfops.find(
                        item => String(item.codigo) === value,
                      );
                      const capitalizedDescription = capitalizeWords(descricao);

                      form.setFieldsValue({
                        description: capitalizedDescription,
                      });
                    }}
                    showSearch
                    filterOption={(input, option) =>
                      option.children
                        .toLowerCase()
                        .indexOf(input.toLowerCase()) >= 0
                    }
                    listHeight={128}
                  >
                    {codeOptions.map(item => (
                      <Select.Option key={item.key} value={item.value}>
                        {item.label}
                      </Select.Option>
                    ))}
                  </Select>
                </FormItem>
              </Col>

              <Col xs={24} md={16}>
                <FormItem
                  name="description"
                  label="Descrição"
                  rules={[{ required: true, message: 'Campo obrigatório' }]}
                >
                  <Input disabled />
                </FormItem>
              </Col>

              <Col
                xs={24}
                md={4}
                style={{
                  marginTop: 16,
                  display: 'flex',
                  alignItems: 'center',
                }}
              >
                <Button
                  type="primary"
                  htmlType="submit"
                  disabled={newCfopMutation.isLoading}
                >
                  Adicionar
                </Button>
              </Col>
            </Row>
          </Form>

          <Divider orientation="left">CFOPs Registrados</Divider>

          <Row gutter={[24, 0]}>
            <Col xs={24} md={6}>
              <Input
                onChange={handleFilter}
                placeholder="Filtre pelo Código"
                addonBefore={<HiSearch size={16} style={{ marginTop: 5 }} />}
                style={{ marginTop: 14 }}
              />
            </Col>
          </Row>

          <Table
            columns={mainColumns}
            dataSource={results}
            pagination={{ defaultPageSize: 5, showSizeChanger: false }}
            size="small"
            rowKey="id"
            loading={query.isLoading}
            style={{ marginTop: 20 }}
          />
        </Modal>
      </Content>
    </Col>
  );
};

export default CfopManagement;
