import { Router } from 'express';

import { AjusteApuracaoController } from '@/controllers/AjusteApuracaoController';

const ajusteApuracaoRouter = Router();

ajusteApuracaoRouter.get('/ajusteApuracao', AjusteApuracaoController.list);

export { ajusteApuracaoRouter };
