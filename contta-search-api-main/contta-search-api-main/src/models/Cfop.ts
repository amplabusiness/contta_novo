import { Document, model, Schema } from 'mongoose';

interface ICfop extends Document {
  code: string;
  description: string;
}

const schema = new Schema<ICfop>({
  code: {
    type: String,
    required: true,
  },
  description: {
    type: String,
    required: true,
  },
});

const Cfop = model<ICfop>('cfop', schema);

export { Cfop };
