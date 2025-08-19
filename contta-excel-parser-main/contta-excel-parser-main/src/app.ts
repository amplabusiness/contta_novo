import cors from 'cors';
import express from 'express';
import morgan from 'morgan';
import path from 'path';

import '@/config/env';
import { isDbConnected } from '@/config/database';

import { parserRouter } from '@/routes/parser.routes';
import { authMiddleware } from './middlewares/auth';

const app = express();

// --- MIDDLEWARES --- //

// CORS configurável por variável de ambiente (CORS_ORIGINS=orig1,orig2)
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
  if (corsOrigins.length && corsOrigins.includes(origin)) return callback(null, true);
  return defaultCorsChecker(origin, callback);
};

app.use(
  cors({
    origin: originChecker,
    exposedHeaders: ['Content-Disposition'],
  }),
);

app.use(morgan('dev'));

app.use(express.json());
app.use(express.urlencoded({ extended: false }));

// --- ROUTES --- //

app.use('/api/v1/', authMiddleware, parserRouter);

// --- PRODUCTION ---

const distPath = path.join(__dirname, '..', 'dist');

// --- HEALTH --- //

app.get('/health', (_req, res) => {
  const db = isDbConnected() ? 'connected' : 'disconnected';
  res.status(200).json({ status: 'ok', db });
});

if (process.env.NODE_ENV === 'production') {
  app.use(express.static(distPath));

  app.get('*', (request, response) => {
    response.sendFile(path.join(__dirname, '..', 'dist', 'lib', 'index.html'));
  });
}

export { app };
