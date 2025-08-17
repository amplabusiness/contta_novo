import { createContext, useContext, useMemo, useState } from 'react';
import PropTypes from 'prop-types';

const ConfiguracaoUsuarioContext = createContext();

export const useConfiguracaoUsuarioContext = () => {
  const context = useContext(ConfiguracaoUsuarioContext);

  if (!context) {
    throw new Error(
      'useConfiguracaoUsuarioContext needs to be used inside ConfiguracaoUsuarioContextProvider.',
    );
  }

  return context;
};

const ConfiguracaoUsuarioContextProvider = ({ children }) => {
  const [state, setState] = useState({
    books: {
      boxBook: {},
      entryBook: {},
      outBook: {},
      simple: {},
    },
    cfops: [],
  });

  const value = useMemo(() => {
    const setInitialBooksData = data => {
      const {
        fechamentoLivroCaixa,
        fechamentoLivroEntrada,
        fechamentoLivroSaida,
        fechamentoSimples,
      } = data;

      const initialBooksData = {
        boxBook: fechamentoLivroCaixa,
        entryBook: fechamentoLivroEntrada,
        outBook: fechamentoLivroSaida,
        simple: fechamentoSimples,
      };

      setState(prevState => ({ ...prevState, books: initialBooksData }));
    };

    const setInitialCfopsData = data => {
      setState(prevState => ({ ...prevState, cfops: data }));
    };

    return { state, setInitialBooksData, setInitialCfopsData };
  }, [state]);

  return (
    <ConfiguracaoUsuarioContext.Provider value={value}>
      {children}
    </ConfiguracaoUsuarioContext.Provider>
  );
};

ConfiguracaoUsuarioContextProvider.propTypes = {
  children: PropTypes.node,
};

ConfiguracaoUsuarioContextProvider.defaultProps = {
  children: null,
};

export default ConfiguracaoUsuarioContextProvider;
