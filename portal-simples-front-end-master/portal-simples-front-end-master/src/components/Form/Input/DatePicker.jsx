import dateFnsGenerateConfig from 'rc-picker/lib/generate/dateFns';
import generatePicker from 'antd/es/date-picker/generatePicker';

import ptBR from 'antd/es/date-picker/locale/pt_BR';

const CustomDatePicker = generatePicker(dateFnsGenerateConfig);

export const DatePickerInput = props => {
  return (
    <CustomDatePicker {...props} locale={ptBR} style={{ width: '100%' }} />
  );
};
