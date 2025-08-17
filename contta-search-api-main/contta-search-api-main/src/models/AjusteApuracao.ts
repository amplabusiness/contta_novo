import { Document, model, Schema } from 'mongoose';

interface IAjusteApuracao extends Document {
  code: string;
  description: string;
  type: string;
  startDate: string;
  endDate: string;
}

const schema = new Schema<IAjusteApuracao>({
  code: {
    type: String,
    required: true,
  },
  description: {
    type: String,
    required: true,
  },
  type: {
    type: String,
    default: '',
  },
  startDate: {
    type: String,
    default: '',
  },
  endDate: {
    type: String,
    default: '',
  },
});

const AjusteApuracao = model<IAjusteApuracao>('apuracao_ajuste', schema);

export { AjusteApuracao };
