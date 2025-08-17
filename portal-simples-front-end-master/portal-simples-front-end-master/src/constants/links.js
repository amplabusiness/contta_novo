import { HiOfficeBuilding } from 'react-icons/hi';
import {
  IoApps,
  IoBriefcase,
  IoCalculator,
  IoCart,
  IoCloudUpload,
  IoHomeSharp,
  IoPerson,
  IoSettings,
} from 'react-icons/io5';
import { RiFileList2Fill } from 'react-icons/ri';

const links = [
  {
    id: '1',
    title: 'Home',
    icon: <IoHomeSharp size={18} />,
    link: '/home',
    disabledWhen: [],
  },
  {
    id: '2',
    title: 'Apuração',
    icon: <IoCalculator size={18} />,
    link: '/apuracao',
    disabledWhen: [],
  },
  {
    id: '3',
    title: 'Produtos',
    icon: <IoCart size={18} />,
    link: '/produtos',
    disabledWhen: ['servico', 'transporte'],
  },
  {
    id: '4',
    title: 'ICMS/ST',
    icon: <HiOfficeBuilding size={18} />,
    link: '/icmsST',
    disabledWhen: ['servico', 'transporte'],
  },
  {
    id: '5',
    title: 'PIS/Cofins',
    icon: <IoBriefcase size={18} />,
    link: '/pisCofins',
    disabledWhen: ['servico', 'transporte'],
  },
  {
    id: '6',
    title: 'Usuários',
    icon: <IoPerson size={18} />,
    link: '/usuarios',
    disabledWhen: [],
  },
  {
    id: '7',
    title: 'Nota Fiscal',
    icon: <RiFileList2Fill size={18} />,
    link: '/modeloNotaFiscal',
    disabledWhen: [],
  },
  {
    id: '8',
    title: 'Upload de XMLs',
    icon: <IoCloudUpload size={18} />,
    link: '/uploadXmls',
    disabledWhen: [],
  },
  {
    id: '9',
    title: 'Configuração',
    icon: <IoSettings size={18} />,
    link: '/configuracaoUsuario',
    disabledWhen: [],
  },
  {
    id: '10',
    title: 'Bloco E',
    icon: <IoApps size={18} />,
    link: '/blocoE',
    disabledWhen: [],
    onlyEnabledOnHighIncomeValue: true,
  },
];

export default links;
