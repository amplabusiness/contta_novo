import { useState } from 'react';
import { differenceInYears, isValid, parseISO } from 'date-fns';
import { useSelector, useDispatch } from 'react-redux';

import { setReferenceDate } from '@/store/slices/referenceDate';

import { DatePickerInput } from '@/components/Form/Input';

import { Container } from './styles';

const ReferenceDate = () => {
  const {
    data: { simplesNacional = {} },
  } = useSelector(state => state.activeCompanyState);
  const { date } = useSelector(state => state.referenceDateState);
  const { dateFounded = null } = simplesNacional;
  const dispatch = useDispatch();

  const [currentDate, setCurrentDate] = useState(new Date(date));

  const handleMonthChange = selectedDate => {
    setCurrentDate(selectedDate);

    const parsedDate = selectedDate.toISOString();
    dispatch(setReferenceDate(parsedDate));
  };

  const parsedFoundationDate = parseISO(dateFounded);
  const maxDate = new Date();
  const globalMinDate = new Date();
  globalMinDate.setFullYear(maxDate.getFullYear() - 5);

  // A data mínima de escolha deve possuir no máximo 5 anos de diferença
  const minDate =
    isValid(parsedFoundationDate) &&
    differenceInYears(globalMinDate, parsedFoundationDate) <= 0
      ? parsedFoundationDate
      : globalMinDate;

  return (
    <Container>
      <DatePickerInput
        value={currentDate}
        onChange={handleMonthChange}
        picker="month"
        format="MM/YYYY"
        disabledDate={current => current < minDate || current > maxDate}
      />
    </Container>
  );
};

export default ReferenceDate;
