import cors from 'cors';
import express from 'express';
import morgan from 'morgan';
import path from 'path';

import '@/config/env';

import { parserRouter } from '@/routes/parser.routes';
import { authMiddleware } from './middlewares/auth';

const app = express();

// --- MIDDLEWARES --- //

app.use(
  cors({
    origin: ['http://localhost:3000', process.env.PRODUCTION_URL],
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
  res.status(200).json({ status: 'ok' });
});

if (process.env.NODE_ENV === 'production') {
  app.use(express.static(distPath));

  app.get('*', (request, response) => {
    response.sendFile(path.join(__dirname, '..', 'dist', 'lib', 'index.html'));
  });
}

export { app };
