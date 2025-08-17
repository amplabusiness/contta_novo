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

// CORS configurável (CORS_ORIGINS=orig1,orig2); default permite localhost e *.vercel.app
const corsOrigins = (process.env.CORS_ORIGINS || '')
  .split(',')
  .map((s) => s.trim())
  .filter(Boolean);

const defaultCorsChecker = (origin: string | undefined, callback: any) => {
  if (!origin) return callback(null, true);
  try {
    const url = new URL(origin);
    const host = url.host.toLowerCase();
    if (host === 'localhost:3000' || host.endsWith('.vercel.app')) return callback(null, true);
  } catch {}
  return callback(new Error('Not allowed by CORS'));
};

app.use(
  cors({
    origin: corsOrigins.length > 0 ? corsOrigins : defaultCorsChecker,
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
