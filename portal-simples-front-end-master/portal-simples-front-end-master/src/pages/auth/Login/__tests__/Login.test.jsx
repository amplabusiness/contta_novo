import { Route } from 'react-router-dom';
import userEvent from '@testing-library/user-event';

import {
  render,
  screen,
  fireEvent,
  waitFor,
} from '@/tests/testing-library-utils';

import Login from '@/pages/auth/Login';

describe('Login tests', () => {
  beforeAll(() => {
    global.matchMedia =
      global.matchMedia ||
      function () {
        return {
          addListener: jest.fn(),
          removeListener: jest.fn(),
        };
      };
  });

  it('should show an error message when invalid email was provided', async () => {
    render(<Login />);

    const emailInput = screen.getByRole('textbox', { name: /e-mail/i });
    userEvent.clear(emailInput);
    userEvent.type(emailInput, 'abcdefgh');
    fireEvent.blur(emailInput);

    await waitFor(async () => {
      const errorMessage = await screen.findByText(
        'O e-mail informado é inválido',
      );

      expect(errorMessage).toBeInTheDocument();
    });
  });

  it('should show an error message when user blur password input without any value', async () => {
    render(<Login />);

    const passwordInput = screen.getByLabelText(/senha/i);
    userEvent.clear(passwordInput);
    fireEvent.blur(passwordInput);

    const errorMessage = await screen.findByText('Campo requerido');

    expect(errorMessage).toBeInTheDocument();
  });

  it('should show an error messages when user tries to submit without any values', async () => {
    render(<Login />);

    const submitButton = screen.getByRole('button', { name: /entrar/i });
    userEvent.click(submitButton);

    const errorMessages = await screen.findAllByText('Campo requerido');
    expect(errorMessages).toHaveLength(2);
  });

  it('should redirect user to registration page', async () => {
    render(
      <>
        <Login />
        <Route path="/cadastro">
          <h1>Cadastro</h1>
        </Route>
      </>,
    );

    const registerPageButton = screen.getByRole('button', {
      name: /cadastre-se/i,
    });
    userEvent.click(registerPageButton);

    const registerHeading = await screen.findByText('Cadastro');
    expect(registerHeading).toBeInTheDocument();
  });

  it('should redirect user to forgot password page', async () => {
    render(
      <>
        <Login />
        <Route path="/esqueciMinhaSenha">
          <h1>Esqueci minha senha</h1>
        </Route>
      </>,
    );

    const forgotPasswordLink = screen.getByRole('link', {
      name: 'Esqueceu sua senha?',
    });
    userEvent.click(forgotPasswordLink);

    const forgotPasswordHeading = await screen.findByText(
      'Esqueci minha senha',
    );
    expect(forgotPasswordHeading).toBeInTheDocument();
  });
});
