import { message, Row } from 'antd';
import { useSelector } from 'react-redux';

import { useConfiguracaoUsuarioContext } from '@/contexts/ConfiguracaoUsuarioContext';
import usePatchUserConfiguration from '@/services/api/hooks/app/ConfiguracaoUsuario/usePatchUserConfiguration';
import useDownloadBook from '@/services/api/hooks/app/ConfiguracaoUsuario/useDownloadBook';

import AdjustmentConfiguration from '@/pages/app/ConfiguracaoUsuario/View/components/AdjustmentConfiguration';
import CfopManagement from '@/pages/app/ConfiguracaoUsuario/View/components/CfopManagement';
import EntryBook from '@/pages/app/ConfiguracaoUsuario/View/components/EntryBook';
import OutBook from '@/pages/app/ConfiguracaoUsuario/View/components/OutBook';
import SimpleBook from '@/pages/app/ConfiguracaoUsuario/View/components/SimpleBook';

import { Container } from '@/styles/global';

const ConfiguracaoUsuarioView = () => {
  const { id } = useSelector(state => state.activeCompanyState);

  const {
    state: { books },
  } = useConfiguracaoUsuarioContext();
  const { boxBook, entryBook, outBook, simple } = books;

  const patchConfigurationMutation = usePatchUserConfiguration();
  const downloadBookMutation = useDownloadBook();

  const onSubmit = configType => async values => {
    let data = {
      CompanyInformation: id,
      FechamentoSimples: {
        ...simple,
      },
      FechamentoLivroEntrada: {
        ...entryBook,
      },
      FechamentoLivroCaixa: {
        ...boxBook,
      },
      FechamentoLivroSaida: {
        ...outBook,
      },
    };

    switch (configType) {
      case 'simples': {
        data = {
          ...data,
          FechamentoSimples: {
            dataFechamento: new Date(values.simplesDate).toISOString(),
          },
        };
        break;
      }
      case 'boxBook': {
        data = {
          ...data,
          FechamentoLivroCaixa: {
            dataFechamento: new Date(values.boxBookDate).toISOString(),
            codUltimoEnviou: values.boxBookLastCode,
          },
        };
        break;
      }
      case 'entryBook': {
        data = {
          ...data,
          FechamentoLivroEntrada: {
            dataFechamento: new Date(values.entryBookDate).toISOString(),
            codUltimoEnviou: values.entryBookLastCode,
          },
        };
        break;
      }
      case 'outBook': {
        data = {
          ...data,
          FechamentoLivroSaida: {
            dataFechamento: new Date(values.outBookDate).toISOString(),
            codUltimoEnviou: values.outBookLastCode,
          },
        };
        break;
      }
      default: {
        break;
      }
    }

    await patchConfigurationMutation.mutateAsync(data);
  };

  const onBookDownload = async bookType => {
    const key = 'book_download_message_key';

    try {
      const messageEnd =
        bookType === 'Venda' ? 'Livro de Saídas' : 'Livro de Entradas';

      message.loading({
        content: `Preparando o ${messageEnd}. O processo pode levar de 10 a 15min.`,
        duration: 0,
        key,
      });

      await downloadBookMutation.mutateAsync(bookType);

      message.success({
        content: `Livro finalizado!`,
        duration: 2.5,
        key,
      });
    } catch (error) {
      message.error({
        content: `Erro durante a preparação do livro`,
        duration: 2.5,
        key,
      });
    }
  };

  return (
    <Container>
      <Row gutter={[40, 10]}>
        <SimpleBook
          onSubmit={onSubmit}
          isLoading={
            patchConfigurationMutation.isLoading ||
            downloadBookMutation.isLoading
          }
        />
        <EntryBook
          onSubmit={onSubmit}
          onBookDownload={onBookDownload}
          isLoading={
            patchConfigurationMutation.isLoading ||
            downloadBookMutation.isLoading
          }
        />
        <OutBook
          onSubmit={onSubmit}
          onBookDownload={onBookDownload}
          isLoading={
            patchConfigurationMutation.isLoading ||
            downloadBookMutation.isLoading
          }
        />
        <AdjustmentConfiguration />
        <CfopManagement />
      </Row>
    </Container>
  );
};

export default ConfiguracaoUsuarioView;
