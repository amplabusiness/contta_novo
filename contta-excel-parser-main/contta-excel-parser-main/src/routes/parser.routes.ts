import { Router } from 'express';

import { ParserController } from '@/controller/ParserController';

const parserRouter = Router();

parserRouter.post('/parse-json', ParserController.parseJson);

export { parserRouter };
