import { InputNumber } from 'antd';

export const PercentageInput = props => {
  return (
    <InputNumber
      min={0}
      max={100}
      step="0.01"
      decimalSeparator=","
      stringMode
      style={{ width: '100%' }}
      {...props}
    />
  );
};
