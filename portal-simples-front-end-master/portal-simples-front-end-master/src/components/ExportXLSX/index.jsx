import { Button } from 'antd';
import PropTypes from 'prop-types';

import useParseJsonToExcel from '@/services/api/hooks/app/shared/useParseJsonToExcel';

const ExportXLSX = ({ data }) => {
  const { mutate, isLoading } = useParseJsonToExcel();

  const hasItemsToExport =
    Object.values(data).filter(item => item.items.length > 0).length > 0;

  if (!data || !hasItemsToExport) {
    return null;
  }

  return (
    <Button type="primary" onClick={() => mutate(data)} loading={isLoading}>
      Exportar para XLSX
    </Button>
  );
};

ExportXLSX.propTypes = {
  data: PropTypes.oneOfType([PropTypes.array, PropTypes.string]).isRequired,
};

export default ExportXLSX;
