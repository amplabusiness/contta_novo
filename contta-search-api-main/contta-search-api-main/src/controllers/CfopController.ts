import { Request, Response } from 'express';

import { Cfop } from '@/models/Cfop';

class CfopController {
  static async list(request: Request, response: Response) {
    try {
      const { q } = request.query;

      if (!q || typeof q !== 'string') {
        return response.status(400).json({ error: 'Invalid param value' });
      }

      const onlyNumbersRegex = /^\d+$/;
      const isSearchByCode = onlyNumbersRegex.test(q);

      let cfops = [];

      if (isSearchByCode) {
        cfops = await Cfop.find({ code: { $regex: `^${q}` } })
          .select('code description')
          .exec();
      } else {
        cfops = await Cfop.find({ description: { $regex: q, $options: 'i' } })
          .select('code description')
          .exec();
      }

      const formattedCfops = cfops.map(cfop => ({
        key: cfop._id,
        label: cfop.description,
        value: cfop.code,
      }));

      return response.status(200).json(formattedCfops);
    } catch (error) {
      return response.status(500).json({ error: 'Internal server error' });
    }
  }
}

export { CfopController };
