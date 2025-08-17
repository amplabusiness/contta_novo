import PropTypes from 'prop-types';
import { Col, Divider, Form as AntForm, Input, Row } from 'antd';
import { FaIndustry } from 'react-icons/fa';
import { Link, useLocation } from 'react-router-dom';

import { dateFormatter } from '@/utils/formatters';

import Form from '@/components/Form';

import { Card } from '@/pages/app/DadosEmpresa/styles';
import { MemberButton } from './styles';

const { Item: FormItem } = AntForm;

const MembersBoard = ({ company }) => {
  const { pathname } = useLocation();

  const formattedLastUpdateDate = dateFormatter(
    company.simplesNacional.lastUpdate,
    "'Atualizado em' P 'às' HH:mm",
    'Nenhuma data de atualização encontrada',
  );

  return (
    <Card>
      <h2>Quadro de Sócios</h2>

      <span>{formattedLastUpdateDate}</span>

      {company.membership.map((member, index) => {
        const { name, cpfSocio, role = {} } = member;
        const { description } = role;

        const nameValue = name ?? '';
        const cpfSocioValue = cpfSocio ?? '';
        const descriptionValue = description ?? '';

        return (
          <Form
            key={member.name}
            name={`member-${index}-form`}
            initialValues={{
              [`nomeSocio${index}`]: nameValue,
              [`cpfSocio${index}`]: cpfSocioValue,
              [`cargoSocio${index}`]: descriptionValue,
            }}
            style={{ marginTop: 10 }}
          >
            <Row gutter={[24, 0]}>
              <Col xs={24} md={10}>
                <FormItem name={`nomeSocio${index}`} label="Nome">
                  <Input disabled />
                </FormItem>
              </Col>

              <Col xs={24} md={6}>
                <FormItem name={`cpfSocio${index}`} label="CPF">
                  <Input disabled />
                </FormItem>
              </Col>

              <Col xs={24} md={6}>
                <FormItem name={`cargoSocio${index}`} label="Cargo">
                  <Input disabled />
                </FormItem>
              </Col>

              <Col
                xs={24}
                md={2}
                style={{
                  marginTop: 18,
                  display: 'flex',
                  alignItems: 'center',
                }}
              >
                <FormItem noStyle>
                  <MemberButton htmlType="button">
                    <Link
                      to={{
                        pathname: `${pathname}/empresasSocio`,
                        state: {
                          memberName: nameValue,
                          memberCpf: cpfSocioValue,
                        },
                      }}
                    >
                      <FaIndustry size={18} />
                    </Link>
                  </MemberButton>
                </FormItem>
              </Col>
            </Row>
            <Divider />
          </Form>
        );
      })}
    </Card>
  );
};

MembersBoard.propTypes = {
  company: PropTypes.object.isRequired,
};

export default MembersBoard;
