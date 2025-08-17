import { useRef, useState } from 'react';
import PropTypes from 'prop-types';

import ProductForm from '@/pages/app/Produtos/View/components/ProductForm';
import ProductsList from '@/pages/app/Produtos/View/components/ProductsList';

import { Container } from '@/styles/global';

const ProdutosView = ({ products }) => {
  const [activeProduct, setActiveProduct] = useState(null);

  const productInputRef = useRef(null);

  return (
    <Container>
      <ProductsList
        products={products}
        activeProduct={activeProduct}
        setActiveProduct={setActiveProduct}
        ref={productInputRef}
      />
      <ProductForm
        activeProduct={activeProduct}
        setActiveProduct={setActiveProduct}
        ref={productInputRef}
      />
    </Container>
  );
};

ProdutosView.propTypes = {
  products: PropTypes.array.isRequired,
};

export default ProdutosView;
