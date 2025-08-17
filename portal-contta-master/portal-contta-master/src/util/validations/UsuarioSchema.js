import * as Yup from "yup";

export const UsuarioSchema = Yup.object().shape({
    name: Yup.string().required("Campo requerido."),
    email: Yup.string()
        .email("O e-mail informado é inválido.")
        .required("Campo requerido."),
    password: Yup.string().required("Campo requerido.")
});