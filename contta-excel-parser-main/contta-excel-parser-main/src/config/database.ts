import mongoose from 'mongoose';

// Deixar compatível com Mongoose 7 e silenciar warning de strictQuery
// @ts-ignore
mongoose.set('strictQuery', false);

const { MONGODB_URI = '' } = process.env as { MONGODB_URI?: string };

export const isDbConnected = () => mongoose.connection.readyState === 1;

if (!MONGODB_URI) {
  // Conexão é opcional neste serviço; não derruba a aplicação
  console.warn('MONGODB_URI não definida. Excel Parser seguirá sem MongoDB.');
} else {
  mongoose
    .connect(MONGODB_URI, {
      // @ts-ignore
      serverSelectionTimeoutMS: 8000,
      // @ts-ignore
      connectTimeoutMS: 8000,
    })
    .catch((err) => {
      console.error('Falha ao conectar no MongoDB:', (err as Error)?.message || err);
    });

  mongoose.connection.on('error', (err) => {
    console.error('Erro de conexão com MongoDB:', (err as Error)?.message || err);
  });

  mongoose.connection.once('open', () => {
    console.log('MongoDB conectado (Excel Parser)');
  });
}

export default mongoose;
