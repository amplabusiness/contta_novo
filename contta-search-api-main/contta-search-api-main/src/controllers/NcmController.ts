import { Request, Response } from 'express';

import { Ncm } from '@/models/Ncm';

class NcmController {
  static async list(request: Request, response: Response) {
    try {
      const { q } = request.query;

      if (!q || typeof q !== 'string') {
        return response.status(400).json({ error: 'Invalid param value' });
      }

      const onlyNumbersRegex = /^\d+$/;
      const isSearchByCode = onlyNumbersRegex.test(q);

      let ncms = [];

      if (isSearchByCode) {
        ncms = await Ncm.find({ code: { $regex: `^${q}` } })
          .select('code description')
          .exec();
      } else {
        ncms = await Ncm.find({ description: { $regex: q, $options: 'i' } })
          .select('code description')
          .exec();
      }

      const formattedNcms = ncms.map(ncm => ({
        key: ncm._id,
        label: `${ncm.code} - ${ncm.description}`,
        value: ncm.code,
      }));

      return response.status(200).json(formattedNcms);
    } catch (error) {
      return response.status(500).json({ error: 'Internal server error' });
    }
  }
}

export { NcmController };
