import { isValid, format, parseISO } from 'date-fns';
import { ptBR } from 'date-fns/locale';

export const dateFormatter = (
  rawDate: string,
  formatAs: string = 'P',
  invalidDateString: string = '-',
) => {
  const parsedDate = parseISO(rawDate);
  const isValidDate = isValid(parsedDate);

  if (!isValidDate) {
    return invalidDateString;
  }

  const formattedDate = format(parsedDate, formatAs, { locale: ptBR });

  return formattedDate;
};
