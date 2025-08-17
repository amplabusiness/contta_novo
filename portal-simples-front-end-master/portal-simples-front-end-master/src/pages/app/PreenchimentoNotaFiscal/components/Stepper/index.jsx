import React, { useState } from 'react';
import PropTypes from 'prop-types';
import { Button, Row, Space, Steps } from 'antd';

import Form from '@/components/Form';

import { Container, Content } from './styles';

const Stepper = ({ form, steps, onSubmit, loading, children, ...rest }) => {
  const [currentStep, setCurrentStep] = useState(0);

  const childrenArray = React.Children.toArray(children);
  const currentChild = childrenArray[currentStep];
  const isLastStep = currentStep === childrenArray.length - 1;

  return (
    <Container>
      <Steps current={currentStep} size="small">
        {steps.map(item => (
          <Steps.Step key={item} title={item} />
        ))}
      </Steps>

      <Form
        {...rest}
        name="stepper-form"
        form={form}
        onFinish={async () => {
          if (isLastStep) {
            try {
              const values = form.getFieldsValue(true);

              await onSubmit(values);

              setCurrentStep(0);
            } catch (error) {
              //
            }
          } else {
            setCurrentStep(prevState => prevState + 1);
          }
        }}
      >
        <Content>{currentChild}</Content>

        <Row align="center" justify="end">
          <Space size="middle">
            {currentStep > 0 && (
              <Button
                htmlType="button"
                onClick={() => setCurrentStep(prevState => prevState - 1)}
              >
                Voltar
              </Button>
            )}

            <Button type="primary" htmlType="submit" loading={loading}>
              {isLastStep ? 'Enviar' : 'Próximo'}
            </Button>
          </Space>
        </Row>
      </Form>
    </Container>
  );
};

Stepper.propTypes = {
  form: PropTypes.object.isRequired,
  steps: PropTypes.array.isRequired,
  onSubmit: PropTypes.func.isRequired,
  loading: PropTypes.bool.isRequired,
  children: PropTypes.node.isRequired,
};

export default Stepper;
