import { FiCheck, FiX } from 'react-icons/fi';
import { IoAlertOutline } from 'react-icons/io5';

export const iconStyles = {
  success: {
    color: '#4bb543',
    icon: <FiCheck size={21} color="#fff" />,
  },
  error: {
    color: '#ff0033',
    icon: <FiX size={21} color="#fff" />,
  },
  alert: {
    color: '#ea7a04',
    icon: <IoAlertOutline size={21} color="#fff" />,
  },
};
