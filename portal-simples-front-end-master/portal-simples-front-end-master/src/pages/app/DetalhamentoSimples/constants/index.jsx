import { currencyFormatter } from '@/utils/formatters';

export const dashboardImpostosEfetivosColumns = {
  'Anexo I': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo II': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'IPI',
      dataIndex: 'ipi',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo III': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo IV': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo V': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ISS',
      dataIndex: 'iss',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
  'Anexo I-III': [
    {
      title: 'IRPJ',
      dataIndex: 'irpj',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CSLL',
      dataIndex: 'csll',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'Cofins',
      dataIndex: 'cofins',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'PIS/PASEP',
      dataIndex: 'pis',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'CPP',
      dataIndex: 'cpp',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
    {
      title: 'ICMS',
      dataIndex: 'icms',
      render: (text, record) => (
        <span>{typeof text === 'number' ? currencyFormatter(text) : text}</span>
      ),
    },
  ],
};
