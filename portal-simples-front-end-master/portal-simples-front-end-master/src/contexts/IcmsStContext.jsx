import { createContext, useContext, useMemo, useReducer } from 'react';
import PropTypes from 'prop-types';

const SET_CURRENT_TAX = 'ICMS_ST/SET_CURRENT_TAX';
const SET_NCMS = 'ICMS_ST/SET_NCMS';
const SET_PRODUCTS = 'ICMS_ST/SET_PRODUCTS';
const SET_FILTERED_PRODUCTS = 'ICMS_ST/SET_FILTERED_PRODUCTS';
const SET_ACTIVE_NCM = 'ICMS_ST/SET_ACTIVE_NCM';
const SET_ACTIVE_PRODUCT = 'ICMS_ST/SET_ACTIVE_PRODUCT';
const RESET_STATE = 'ICMS_ST/RESET_STATE';

const initialState = {
  currentTax: '',
  ncms: [],
  products: [],
  filteredProducts: [],
  activeNcm: '',
  activeProduct: {},
};

const reducer = (state = initialState, action) => {
  switch (action.type) {
    case SET_CURRENT_TAX: {
      return { ...state, currentTax: action.payload };
    }
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

const IcmsStContext = createContext();

export const useIcmsStContext = () => {
  const context = useContext(IcmsStContext);

  if (!context) {
    throw new Error(
      'useIcmsStContext needs to be used inside IcmsStContextProvider.',
    );
  }

  return context;
};

const IcmsStContextProvider = ({ children }) => {
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

    const setCurrentTax = selectedTax => {
      dispatch({ type: SET_CURRENT_TAX, payload: selectedTax });
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
      setCurrentTax,
      filterProducts,
      changeProduct,
      changeAllProducts,
      confirmModification,
      confirmReversal,
      resetState,
    };
  }, [state]);

  return (
    <IcmsStContext.Provider value={value}>{children}</IcmsStContext.Provider>
  );
};

IcmsStContextProvider.propTypes = {
  children: PropTypes.node,
};

IcmsStContextProvider.defaultProps = {
  children: null,
};

export default IcmsStContextProvider;
