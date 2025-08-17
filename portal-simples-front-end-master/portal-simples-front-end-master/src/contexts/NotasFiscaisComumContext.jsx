import { createContext, useContext, useMemo, useReducer } from 'react';
import PropTypes from 'prop-types';

const SET_CURRENT_PAGE = 'NOTAS_FISCAIS_COMUM/SET_CURRENT_PAGE';
const SET_INVOICES_COUNT = 'NOTAS_FISCAIS_COMUM/SET_INVOICES_COUNT';
const SET_CURRENT_ITEMS = 'NOTAS_FISCAIS_COMUM/SET_CURRENT_ITEMS';
const SET_CURRENT_ANALYTICAL = 'NOTAS_FISCAIS_COMUM/SET_CURRENT_ANALYTICAL';

const initialState = {
  currentPage: 1,
  invoicesCount: 0,
  currentItems: [],
  currentAnalytical: [],
};

const reducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_CURRENT_PAGE: {
      return { ...state, currentPage: action.payload };
    }
    case SET_INVOICES_COUNT: {
      return { ...state, invoicesCount: action.payload };
    }
    case SET_CURRENT_ITEMS: {
      return { ...state, currentItems: action.payload };
    }
    case SET_CURRENT_ANALYTICAL: {
      return { ...state, currentAnalytical: action.payload };
    }
    default: {
      return state;
    }
  }
};

const NotasFiscaisComumContext = createContext();

export const useNotasFiscaisComumContext = () => {
  const context = useContext(NotasFiscaisComumContext);

  if (!context) {
    throw new Error(
      'useNotasFiscaisComumContext needs to be used inside NotasFiscaisComumContextProvider.',
    );
  }

  return context;
};

const NotasFiscaisComumContextProvider = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  const value = useMemo(() => {
    const setInitialState = data => {
      const itemsTotal = data?.totalDeItens;

      dispatch({ type: SET_INVOICES_COUNT, payload: itemsTotal });
    };

    const changePage = newPage => {
      const { currentItems, currentAnalytical } = state;

      if (currentItems.length > 0) {
        dispatch({ type: SET_CURRENT_ITEMS, payload: [] });
      }

      if (currentAnalytical.length > 0) {
        dispatch({ type: SET_CURRENT_ANALYTICAL, payload: [] });
      }

      dispatch({ type: SET_CURRENT_PAGE, payload: newPage });
    };

    const changeActiveInvoice = (currInvoices, invoiceId) => {
      const selectedInvoice = currInvoices.find(item => item.id === invoiceId);
      const { items, analytical } = selectedInvoice;

      dispatch({ type: SET_CURRENT_ITEMS, payload: items });
      dispatch({ type: SET_CURRENT_ANALYTICAL, payload: [analytical] });
    };

    return { state, setInitialState, changePage, changeActiveInvoice };
  }, [state]);

  return (
    <NotasFiscaisComumContext.Provider value={value}>
      {children}
    </NotasFiscaisComumContext.Provider>
  );
};

NotasFiscaisComumContextProvider.propTypes = {
  children: PropTypes.node,
};

NotasFiscaisComumContextProvider.defaultProps = {
  children: null,
};

export default NotasFiscaisComumContextProvider;
