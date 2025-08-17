import { Request, Response } from 'express';

import { AjusteApuracao } from '@/models/AjusteApuracao';

class AjusteApuracaoController {
  static async list(request: Request, response: Response) {
    try {
      const adjusts = await AjusteApuracao.find()
        .select('code description')
        .exec();

      return response.status(200).json(adjusts);
    } catch (error) {
      return response.status(500).json({ error: 'Internal server error' });
    }
  }
}

export { AjusteApuracaoController };
