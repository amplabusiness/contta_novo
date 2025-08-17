import { Router } from 'express';

import { NcmController } from '@/controllers/NcmController';

const ncmRouter = Router();

ncmRouter.get('/ncm', NcmController.list);

export { ncmRouter };
