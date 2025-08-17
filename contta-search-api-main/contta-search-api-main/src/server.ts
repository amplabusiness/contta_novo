import { app } from '@/app';
import { successLog } from '@/utils/chalk';

const port = process.env.PORT || 5000;
app.listen(port, () => {
  successLog(`Server listening on port ${port}`);
});
