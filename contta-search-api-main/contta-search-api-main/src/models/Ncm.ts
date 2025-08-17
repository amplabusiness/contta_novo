import { Document, model, Schema } from 'mongoose';

interface INcm extends Document {
  code: string;
  description: string;
  startDate: string;
  endDate: string;
  actType: string;
  actNumber: string;
  actYear: string;
}

const schema = new Schema<INcm>({
  code: {
    type: String,
    required: true,
  },
  description: {
    type: String,
    required: true,
  },
  startDate: {
    type: String,
    required: true,
  },
  endDate: {
    type: String,
    required: true,
  },
  actType: {
    type: String,
    required: true,
  },
  actNumber: {
    type: String,
    required: true,
  },
  actYear: {
    type: String,
    required: true,
  },
});

const Ncm = model<INcm>('ncm', schema);

export { Ncm };
