import mongoose from 'mongoose';

import { successLog } from '@/utils/chalk';

// Align with Mongoose 7 default and silence deprecation warning
mongoose.set('strictQuery', false);

const { MONGODB_URI = '' } = process.env;

// Small helper so other modules can check DB status
export const isDbConnected = () => mongoose.connection.readyState === 1; // 1 = connected

if (!MONGODB_URI) {
  console.error('MONGODB_URI is not set. Set it in Render (env group).');
} else {
  // Avoid crash loops on invalid DNS/URI; log and keep service up
  mongoose.connect(MONGODB_URI, {
    serverSelectionTimeoutMS: 8000,
    connectTimeoutMS: 8000,
  }).catch((err) => {
    console.error('Could not connect to MongoDB Error:', err?.message || err);
  });
}

mongoose.connection.on('error', (err) => {
  console.error('MongoDB connection error:', err?.message || err);
});

mongoose.connection.once('open', () => {
  successLog('Successfully connected to MongoDB');
});
