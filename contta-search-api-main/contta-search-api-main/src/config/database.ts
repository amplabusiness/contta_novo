import { connect, connection } from 'mongoose';

import { successLog } from '@/utils/chalk';

const { MONGODB_URI = '' } = process.env;

connect(MONGODB_URI);

connection.on(
  'error',
  console.error.bind(console, 'Could not connect to MongoDB'),
);

connection.once('open', () => {
  successLog('Successfully connected to MongoDB');
});
