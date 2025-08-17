import { format, isValid, parseISO } from 'date-fns';
import ptBR from 'date-fns/locale/pt-BR';

export const removeNonNumericChars = value => {
  if (!value) {
    return '';
  }

  return value.replace(/\D/g, '');
};

export const cpfCnpjFormatter = value => {
  if (!value) {
    return '';
  }

  const rawValue = removeNonNumericChars(value);

  if (value.length === 11) {
    return rawValue.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, '$1.$2.$3-$4');
  }

  return rawValue.replace(
    /(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g,
    '$1.$2.$3/$4-$5',
  );
};

export const currencyFormatter = value => {
  const rawValue = value ?? 0;

  return Intl.NumberFormat('pt-br', {
    style: 'currency',
    currency: 'BRL',
  }).format(rawValue);
};

export const reformatCurrency = value => {
  const hasNumbersRegex = /\d/;

  if (!value || !hasNumbersRegex.test(value)) {
    return 0;
  }

  const parsedValue = parseFloat(
    String(value)
      .replace(/[R$ .]/g, '')
      .replace(',', '.'),
  );

  return parsedValue;
};

export const reformatPercentage = value => {
  if (!value) {
    return 0;
  }

  const parsedValue = parseFloat(value.replace(',', '.'));

  return parsedValue;
};

export const cnaeFormatter = value => {
  if (!value) {
    return '';
  }

  const parsedValue = value.replace(
    /(\d{2})(\d{2})(\d{1})(\d{2})/g,
    '$1.$2-$3-$4',
  );

  return parsedValue;
};

export const capitalizeWords = word => {
  const lowerCasedWord = word.toLowerCase();
  const splittedWord = lowerCasedWord.split(' ');
  const capitalizedWords = splittedWord.map(item => {
    if (item.length <= 2) {
      return item;
    }

    return item.charAt(0).toUpperCase() + item.slice(1);
  });

  return capitalizedWords.join(' ');
};

export const formatToBrazilianNumber = value => {
  return new Intl.NumberFormat('pt-BR', { maximumFractionDigits: 2 }).format(
    value,
  );
};

export const percentageFormatter = value => {
  if (!value) {
    return '0,00%';
  }

  const formattedValue = `${value.toFixed(2).replace('.', ',')}%`;

  return formattedValue;
};

export const cepFormatter = value => {
  if (!value) {
    return '';
  }

  const formattedValue = String(value).replace(/(\d{5})(\d{3})/g, '$1-$2');

  return formattedValue;
};

export const dateFormatter = (
  rawDate,
  formatAs = 'P',
  invalidDateString = '-',
) => {
  const parsedDate = parseISO(rawDate);
  const isValidDate = isValid(parsedDate);

  if (!isValidDate) {
    return invalidDateString;
  }

  const formattedDate = format(parsedDate, formatAs, { locale: ptBR });

  return formattedDate;
};
