import { createContext, useContext, useMemo, useReducer } from 'react';
import PropTypes from 'prop-types';

const SET_NCMS = 'PIS_COFINS/SET_NCMS';
const SET_PRODUCTS = 'PIS_COFINS/SET_PRODUCTS';
const SET_FILTERED_PRODUCTS = 'PIS_COFINS/SET_FILTERED_PRODUCTS';
const SET_ACTIVE_NCM = 'PIS_COFINS/SET_ACTIVE_NCM';
const SET_ACTIVE_PRODUCT = 'PIS_COFINS/SET_ACTIVE_PRODUCT';
const RESET_STATE = 'PIS_COFINS/RESET_STATE';

const initialState = {
  ncms: [],
  products: [],
  filteredProducts: [],
  activeNcm: '',
  activeProduct: {},
};

const reducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_NCMS: {
      return { ...state, ncms: action.payload };
    }
    case SET_PRODUCTS: {
      return { ...state, products: action.payload };
    }
    case SET_FILTERED_PRODUCTS: {
      return { ...state, filteredProducts: action.payload };
    }
    case SET_ACTIVE_NCM: {
      return { ...state, activeNcm: action.payload };
    }
    case SET_ACTIVE_PRODUCT: {
      return { ...state, activeProduct: action.payload };
    }
    case RESET_STATE: {
      return initialState;
    }
    default: {
      return state;
    }
  }
};

const PisCofinsContext = createContext();

export const usePisCofinsContext = () => {
  const context = useContext(PisCofinsContext);

  if (!context) {
    throw new Error(
      'usePisCofinsContext needs to be used inside PisCofinsContextProvider.',
    );
  }

  return context;
};

const PisCofinsContextProvider = ({ children }) => {
  const [state, dispatch] = useReducer(reducer, initialState);

  const value = useMemo(() => {
    const setInitialState = data => {
      const ncms = data.map(item => item.ncmProd);
      const uniqueNcms = [...new Set(ncms)]
        .sort()
        .map(item => ({ key: item, ncm: item }));

      dispatch({ type: SET_NCMS, payload: uniqueNcms });
      dispatch({ type: SET_PRODUCTS, payload: data });
    };

    const filterProducts = (currProducts, ncm) => {
      const filteredProducts = currProducts.filter(
        product => product.ncmProd === ncm,
      );

      dispatch({ type: SET_ACTIVE_NCM, payload: ncm });
      dispatch({ type: SET_FILTERED_PRODUCTS, payload: filteredProducts });
    };

    const changeProduct = (
      currFilteredProducts,
      productId,
      regime,
      checkboxValue,
    ) => {
      const foundProduct = currFilteredProducts.find(
        product => product.id === productId,
      );
      const foundProductIndex = currFilteredProducts.findIndex(
        product => product.id === productId,
      );

      const updatedProduct = {
        ...foundProduct,
        [regime]: checkboxValue,
        modificado: !foundProduct.modificado,
      };

      const updatedProducts = [
        ...currFilteredProducts.slice(0, foundProductIndex),
        updatedProduct,
        ...currFilteredProducts.slice(foundProductIndex + 1),
      ];

      dispatch({ type: SET_FILTERED_PRODUCTS, payload: updatedProducts });
    };

    const changeAllProducts = (currFilteredProducts, regime, checkboxValue) => {
      const updatedProducts = currFilteredProducts.map(product => ({
        ...product,
        [regime]: checkboxValue,
        modificado: !product.modificado,
      }));

      dispatch({ type: SET_FILTERED_PRODUCTS, payload: updatedProducts });
    };

    const confirmModification = async (currFilteredProducts, data) => {
      const { ListProdutos: changedProducts } = data;

      if (changedProducts.length === 1) {
        const [changedProduct] = changedProducts;

        const updatedFilteredProducts = currFilteredProducts.filter(
          product => product.id !== changedProduct.id,
        );

        dispatch({
          type: SET_FILTERED_PRODUCTS,
          payload: updatedFilteredProducts,
        });

        if (updatedFilteredProducts.length === 0) {
          dispatch({ type: SET_ACTIVE_NCM, payload: '' });
        }
      } else {
        dispatch({ type: SET_FILTERED_PRODUCTS, payload: [] });
        dispatch({ type: SET_ACTIVE_NCM, payload: '' });
      }
    };

    const confirmReversal = async (currFilteredProducts, data) => {
      const onlyRevertedProductsIds = data
        .filter(item => item.modificado === false)
        .map(item => item.id);
      const updatedFilteredProducts = currFilteredProducts.filter(
        item => !onlyRevertedProductsIds.includes(item.id),
      );

      dispatch({
        type: SET_FILTERED_PRODUCTS,
        payload: updatedFilteredProducts,
      });

      if (updatedFilteredProducts.length === 0) {
        dispatch({ type: SET_ACTIVE_NCM, payload: '' });
      }
    };

    const resetState = () => {
      dispatch({ type: RESET_STATE });
    };

    return {
      state,
      setInitialState,
      filterProducts,
      changeProduct,
      changeAllProducts,
      confirmModification,
      confirmReversal,
      resetState,
    };
  }, [state]);

  return (
    <PisCofinsContext.Provider value={value}>
      {children}
    </PisCofinsContext.Provider>
  );
};

PisCofinsContextProvider.propTypes = {
  children: PropTypes.node,
};

PisCofinsContextProvider.defaultProps = {
  children: null,
};

export default PisCofinsContextProvider;
