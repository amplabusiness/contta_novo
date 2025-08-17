import { Row, Col } from 'antd';

import SingleRegister from '@/pages/app/Empresas/components/SingleRegister';
import MultipleRegister from '@/pages/app/Empresas/components/MultipleRegister';
import CompaniesList from '@/pages/app/Empresas/components/CompaniesList';

import { Container, Title } from '@/styles/global';

const Empresas = () => {
  return (
    <Container>
      <Row gutter={[0, 20]}>
        <Col xs={24}>
          <Title>
            <h2>Cadastro</h2>
            <p>
              No campo logo abaixo, insira o CNPJ da empresa que deseja
              cadastrar. Caso deseje cadastrar várias empresas, utilize a opção
              Enviar Lista de CNPJs.
            </p>
          </Title>

          <SingleRegister />

          <MultipleRegister />
        </Col>

        <Col xs={24}>
          <Title>
            <h2>Empresas Cadastradas</h2>
            <p>
              Essas são as empresas já cadastradas em sua conta. Você pode
              editá-las, confirmar seu status (ativa ou desativa) ou excluí-las
              de sua conta.
            </p>
          </Title>

          <CompaniesList />
        </Col>
      </Row>
    </Container>
  );
};

export default Empresas;
