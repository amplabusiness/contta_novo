import PropTypes from 'prop-types';
import React from 'react';
import { Input } from 'antd';

const TYPES = {
  CPF: '999.999.999-999',
  CNPJ: '99.999.999/9999-99',
};

export const CNPJCPFInput = ({
  value = undefined,
  onChange = () => {},
  ...props
}) => {
  const getMask = val => {
    return val.length > 11 ? 'CNPJ' : 'CPF';
  };

  const applyMask = (val, mask) => {
    let result = '';

    let inc = 0;
    Array.from(val).forEach((letter, index) => {
      if (!mask[index + inc].match(/[0-9]/)) {
        result += mask[index + inc];
        inc += 1;
      }
      result += letter;
    });
    return result;
  };

  const clear = val => {
    return val && val.replace(/[^0-9]/g, '');
  };

  const MAX_LENGTH = clear(TYPES.CNPJ).length;

  const onLocalChange = ev => {
    const val = clear(ev.target.value);
    const mask = getMask(val);

    const nextLength = val.length;

    if (nextLength > MAX_LENGTH) return;

    value = applyMask(val, TYPES[mask]);

    ev.target.value = val;

    onChange(ev, mask);
  };

  let currentValue = clear(value);

  if (currentValue) {
    currentValue = applyMask(currentValue, TYPES[getMask(currentValue)]);
  }

  return (
    <Input
      {...props}
      type="tel"
      value={currentValue}
      onChange={onLocalChange}
    />
  );
};

CNPJCPFInput.propTypes = {
  value: PropTypes.string,
  onChange: PropTypes.func,
};

CNPJCPFInput.defaultProps = {
  value: undefined,
  onChange: () => {},
};
