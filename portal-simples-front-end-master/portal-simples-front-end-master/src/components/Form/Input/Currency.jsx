import { InputNumber } from 'antd';

export const CurrencyInput = props => {
  const currencyFormatter = value => {
    return new Intl.NumberFormat('pt-BR', {
      style: 'currency',
      currency: 'BRL',
    }).format(value);
  };

  const currencyParser = val => {
    try {
      if (typeof val === 'string' && !val.length) {
        val = '0.0';
      }

      const group = new Intl.NumberFormat('pt-BR')
        .format(1111)
        .replace(/1/g, '');
      const decimal = new Intl.NumberFormat('pt-BR')
        .format(1.1)
        .replace(/1/g, '');
      let reversedVal = val.replace(new RegExp(`\\${group}`, 'g'), '');
      reversedVal = reversedVal.replace(new RegExp(`\\${decimal}`, 'g'), '.');

      reversedVal = reversedVal.replace(/[^0-9.]/g, '');

      const digitsAfterDecimalCount = (reversedVal.split('.')[1] || []).length;
      const needsDigitsAppended = digitsAfterDecimalCount > 2;

      if (needsDigitsAppended) {
        reversedVal *= 10 ** (digitsAfterDecimalCount - 2);
      }

      return Number.isNaN(reversedVal) ? 0 : reversedVal;
    } catch (error) {
      console.error(error);
      return 0;
    }
  };

  return (
    <InputNumber
      {...props}
      formatter={currencyFormatter}
      parser={currencyParser}
      style={{ width: '100%' }}
    />
  );
};
