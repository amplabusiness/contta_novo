import { Request, Response } from 'express';
import XLSX from 'xlsx';

import { fitToColumn, fitToRow, formatData } from '@/utils/excel';
import { dateFormatter } from '@/utils/dateFns';

interface ObjectDTO {
  headers: {
    key: string;
    title: string;
  }[];
  items: any[];
  name: string;
}

class ParserController {
  static parseJson(request: Request, response: Response) {
    const data = request.body as ObjectDTO[];

    const workbook = XLSX.utils.book_new();

    data.forEach(({ headers, items, name }) => {
      const formattedData = formatData(headers, items);
      const worksheet = XLSX.utils.json_to_sheet(formattedData);
      worksheet['!cols'] = fitToColumn(formattedData);
      worksheet['!rows'] = fitToRow(formattedData);

      XLSX.utils.book_append_sheet(workbook, worksheet, name);
    });

    const filename = `planilha_${dateFormatter(
      new Date().toISOString(),
      'ddMMyyyyHHmm',
    )}.xlsx`;

    response.setHeader(
      'Content-Type',
      'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet',
    );
    response.setHeader(
      'Content-Disposition',
      `attachment; filename=${filename}`,
    );

    const xlsxFile = XLSX.write(workbook, { type: 'buffer', bookType: 'xlsx' });

    return response.send(Buffer.from(xlsxFile));
  }
}

export { ParserController };
