import { Router } from 'express';

import { CfopController } from '@/controllers/CfopController';

const cfopRouter = Router();

cfopRouter.get('/cfop', CfopController.list);

export { cfopRouter };
