import { isValid, parseISO } from 'date-fns';

import { dateFormatter } from '@/utils/dateFns';

interface Header {
  key: string;
  title: string;
}

/*
 * Função que retorna o valor da propriedade informada de um objeto,
 * sendo que essa propriedade pode ser "nestada"
 */
const getNestedValue = (nestedObj: any, path: string | Array<string>) => {
  const pathArr = Array.isArray(path) ? path : [path];

  return pathArr.reduce(
    (obj, key) => (obj && obj[key] !== 'undefined' ? obj[key] : undefined),
    nestedObj,
  );
};

/*
 * Função que realiza uma formatação geral nos dados que estarão
 * presentes no arquivo final (tratamento de datas e valores booleanos)
 */
export const formatData = (headers: Header[], rawData: Array<any>) => {
  return rawData.map(currItem => {
    const newObjectFormat = headers.reduce((accumulator, currHeader) => {
      const { key, title } = currHeader;

      // Pegando o valor do objeto por meio da sua key (key é uma string ou array)
      let rowValue = getNestedValue(currItem, key) ?? '';

      if (rowValue === true) {
        // Formatação para valores boolean true
        rowValue = 'Sim';
      } else if (rowValue === false) {
        // Formatação para valores boolean false
        rowValue = 'Não';
      } else if (String(rowValue).length > 4 && isValid(parseISO(rowValue))) {
        /* Formatação para valores que são datas
         * (a primeira condição evita que valores como 4341 sejam considerados datas válidas)
         */
        rowValue = dateFormatter(rowValue);
      }

      accumulator[title] = rowValue;

      return accumulator;
    }, {});

    return newObjectFormat;
  });
};

/*
 * Função que calcula a largura ideal de cada célula da planilha
 */
export const fitToColumn = (sheet: Array<any>) => {
  if (sheet.length === 0) {
    return [];
  }

  const columnsWidth = [];

  Object.keys(sheet[0]).forEach(property => {
    const headerSize = property ? property.toString().length : 0;
    const rowsSize = sheet.map(item =>
      item[property] ? item[property].toString().length : 0,
    );

    columnsWidth.push({
      wch: Math.max(headerSize, ...rowsSize),
    });
  });

  return columnsWidth;
};

/*
 * Função que calcula a altura ideal de cada célula da planilha
 */
export const fitToRow = (sheet: Array<any>) => {
  if (sheet.length === 0) {
    return [];
  }

  const rowsHeight = Array.from(Array(sheet.length + 1)).map(() => ({
    hpx: 18,
  }));

  return rowsHeight;
};
