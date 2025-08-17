import { useState, useEffect } from 'react';
import { Tooltip } from 'antd';
import { MdInfo } from 'react-icons/md';
import { useSelector, useDispatch } from 'react-redux';
import { useHistory } from 'react-router-dom';

import useViewportWidth from '@/hooks/useViewportWidth';
import { changeCompanySE } from '@/store/slices/activeCompany';
import { cpfCnpjFormatter } from '@/utils/formatters';

import { CustomAutoComplete } from './styles';

const { Option } = CustomAutoComplete;

const ChangeCompany = () => {
  const { name } = useSelector(state => state.activeCompanyState);
  const { companies } = useSelector(state => state.companiesState);
  const dispatch = useDispatch();

  const [results, setResults] = useState([]);
  const [activeName, setActiveName] = useState('');

  const { push } = useHistory();

  const { isMobile } = useViewportWidth();

  useEffect(() => {
    setActiveName(name || '');
    setResults(companies);
  }, [name, companies]);

  const handleSearch = searchTerm => {
    setActiveName(searchTerm);

    if (!searchTerm) {
      setResults(companies);
      return;
    }

    setResults(
      companies.filter(
        item =>
          item.name.toLowerCase().includes(searchTerm.toLowerCase()) ||
          item.cnpj.includes(searchTerm),
      ),
    );
  };

  const handleBlur = () => {
    const companyNames = companies.map(item => item.name);

    if (!companyNames.includes(activeName)) {
      setActiveName(name || '');
    }
  };

  const handleFocus = event => {
    event.target.select();
  };

  const handleSelect = (value, option) => {
    const { key, value: selectedName } = option;

    if (selectedName !== name) {
      dispatch(changeCompanySE(key));
      setActiveName(selectedName);

      push('/dashboard');
    }
  };

  return (
    <>
      {!isMobile && (
        <Tooltip
          title="Clique ao lado para mudar a empresa ativa"
          placement="bottomRight"
        >
          <MdInfo size={28} color="#3276b1" />
        </Tooltip>
      )}
      <CustomAutoComplete
        placeholder="Nenhuma empresa selecionada"
        value={activeName}
        onChange={handleSearch}
        onSelect={handleSelect}
        onBlur={handleBlur}
        onFocus={handleFocus}
        defaultActiveFirstOption={false}
      >
        {results.length > 0 &&
          results.map(item => (
            <Option key={item.id} value={item.name}>
              {item.name} - {cpfCnpjFormatter(item.cnpj)}
            </Option>
          ))}
      </CustomAutoComplete>
    </>
  );
};

export default ChangeCompany;
