import cors from 'cors';
import express from 'express';
import morgan from 'morgan';
import path from 'path';
import fs from 'fs';

import '@/config/env';
import '@/config/database';
import { isDbConnected } from '@/config/database';
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

const originChecker = (origin: string | undefined, callback: any) => {
  if (!origin) return callback(null, true);
  // If explicit list contains the origin, allow
  if (corsOrigins.length && corsOrigins.includes(origin)) return callback(null, true);
  // Fallback default rules (localhost and *.vercel.app)
  return defaultCorsChecker(origin, callback);
};

app.use(
  cors({
    origin: originChecker,
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
  const db = isDbConnected() ? 'connected' : 'disconnected';
  res.status(200).json({ status: 'ok', db });
});

// --- PRODUCTION ---

const distPath = path.join(__dirname, '..', 'dist');
const spaIndex = path.join(__dirname, '..', 'dist', 'lib', 'index.html');

if (process.env.NODE_ENV === 'production' && fs.existsSync(spaIndex)) {
  app.use(express.static(distPath));
  app.get('*', (_req, res) => res.sendFile(spaIndex));
} else {
  // Provide a friendly root response for API-only deployments
  app.get('/', (_req, res) => {
    res.status(200).json({ name: 'contta-search-api', status: 'ok', note: 'no frontend bundle' });
  });
}

export { app };
