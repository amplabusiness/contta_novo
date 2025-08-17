import * as Yup from "yup";

export const LoginSchema = Yup.object().shape({
  email: Yup.string()
    .email("O e-mail informado é inválido.")
    .required("Campo requerido."),
  password: Yup.string().required("Campo requerido.")
});