import { useState, useEffect } from 'react';
import { Col, Row } from 'antd';
import { useSelector } from 'react-redux';
import { useHistory, useParams } from 'react-router-dom';

import useUpdateCompany from '@/services/api/hooks/app/DadosEmpresa/useUpdateCompany';
import { removeNonNumericChars } from '@/utils/formatters';

import Address from '@/pages/app/DadosEmpresa/components/Address';
import EconomicActivities from '@/pages/app/DadosEmpresa/components/EconomicActivities';
import FederalRevenue from '@/pages/app/DadosEmpresa/components/FederalRevenue';
import MembersBoard from '@/pages/app/DadosEmpresa/components/MembersBoard';
import NationalSimple from '@/pages/app/DadosEmpresa/components/NationalSimple';
import ResponsibleDetails from '@/pages/app/DadosEmpresa/components/ResponsibleDetails';
import SimpleAnnexes from '@/pages/app/DadosEmpresa/components/SimpleAnnexes';
import StateRegistrations from '@/pages/app/DadosEmpresa/components/StateRegistrations';

import ShimmerLoading from '@/components/Shimmer/DadosEmpresa';

const DadosEmpresa = () => {
  const { companies } = useSelector(state => state.companiesState);

  const [company, setCompany] = useState(null);

  const { mutate, isLoading } = useUpdateCompany();

  const { id } = useParams();
  const { push } = useHistory();

  useEffect(() => {
    const foundCompany = companies.filter(item => item.id === id)[0];

    if (!foundCompany) {
      push('/empresas');
    }

    setCompany(foundCompany);
  }, [companies, id, push]);

  const handleSubmit = type => values => {
    let data = {
      id,
      simplesNacional: {
        lastUpdate: company.simplesNacional.lastUpdate,
        simplesOptant: company.simplesNacional.simplesOptant,
        simplesIncluded: company.simplesNacional.simplesIncluded,
        simplesExcluded: company.simplesNacional.simplesExcluded,
        simeiOptant: company.simplesNacional.simeiOptant,
        qualificationResp: company.simplesNacional.qualificationResp,
        simpleCode: company.simplesNacional.simpleCode,
        responsibleCpf: company.simplesNacional.responsibleCpf,
        responsibleName: company.simplesNacional.responsibleName,
        responsibleEmail: company.simplesNacional.responsibleEmail,
        responsibleTelefone: company.simplesNacional.responsibleTelefone,
      },
      regimeBox: company.regimeBox,
      regimeCompetence: company.regimeCompetence,
      tipo: type,
    };

    if (type === 1) {
      const newPhone =
        values.responsibleTelefone ||
        company.simplesNacional.responsibleTelefone;

      data = {
        simplesNacional: {
          ...data.simplesNacional,
          qualificationResp: values.qualificationResp,
          simpleCode: values.simpleCode,
          responsibleCpf: removeNonNumericChars(values.responsibleCpf),
          responsibleName: values.responsibleName,
          responsibleEmail: values.responsibleEmail,
          responsibleTelefone: newPhone,
        },
        ...data,
      };
    } else if (type === 2) {
      const isBoxRegime = values.regime === 'caixa';

      data = {
        ...data,
        regimeBox: isBoxRegime,
        regimeCompetence: !isBoxRegime,
      };
    }

    mutate(data);
  };

  if (!company) {
    return <ShimmerLoading />;
  }

  return (
    <Row gutter={[24, 20]}>
      <Col xs={24} xl={16}>
        <FederalRevenue company={company} />

        <Address company={company} />

        <ResponsibleDetails
          company={company}
          handleSubmit={handleSubmit}
          isLoading={isLoading}
        />

        {company.membership.length > 0 && <MembersBoard company={company} />}
      </Col>

      <Col xs={24} xl={8}>
        <NationalSimple
          company={company}
          handleSubmit={handleSubmit}
          isLoading={isLoading}
        />

        <StateRegistrations company={company} />

        <SimpleAnnexes company={company} />

        <EconomicActivities company={company} />
      </Col>
    </Row>
  );
};

export default DadosEmpresa;
