import cors from 'cors';
import express from 'express';
import morgan from 'morgan';
import path from 'path';

import '@/config/env';
import '@/config/database';
import { ncmRouter } from './routes/ncm.routes';
import { cfopRouter } from './routes/cfop.routes';
import { ajusteApuracaoRouter } from './routes/ajusteApuracao.routes';
import { authMiddleware } from './middlewares/auth';
import { adminRouter } from './routes/admin.routes';

const app = express();

// --- MIDDLEWARES --- //

app.use(
  cors({
    origin: [
      'http://localhost:3000',
      'https://portal-simples-git-develop-conttaampla.vercel.app',
    ],
  }),
);
app.use(morgan('dev'));
app.use(express.urlencoded({ extended: false }));
app.use(express.json());

// --- ROUTES --- //

// Protege rotas de API com JWT (exceto saúde)
app.use('/api', authMiddleware, ncmRouter);
app.use('/api', authMiddleware, cfopRouter);
app.use('/api', authMiddleware, ajusteApuracaoRouter);
app.use('/api', authMiddleware, adminRouter);

// --- HEALTH --- //

app.get('/health', (_req, res) => {
  res.status(200).json({ status: 'ok' });
});

// --- PRODUCTION ---

const distPath = path.join(__dirname, '..', 'dist');

if (process.env.NODE_ENV === 'production') {
  app.use(express.static(distPath));

  app.get('*', (request, response) => {
    response.sendFile(path.join(__dirname, '..', 'dist', 'lib', 'index.html'));
  });
}

export { app };
