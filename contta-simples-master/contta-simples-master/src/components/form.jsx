import React from 'react';
import {
  Tooltip,
  Icon,
  DatePicker as AntdDatePicker,
  Select as AntdSelect,
  Input,
  InputNumber as AntdInputNumber,
  Tag,
  Switch,
} from 'antd';

import moment from 'moment';

const { Option } = AntdSelect;
const tooltipColor = 'rgb(201, 195, 195)';
const { TextArea } = Input;

/*
 * Input Text
 */
const InputText = ({
  name,
  value,
  label,
  col,
  tooltip,
  required,
  disabled,
  placeholder,
  handleChange,
  error,
}) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group ${!!error && 'is-invalid'}`}>
      <label className="control-label">
        {label}
        <span style={{ color: 'transparent' }}>.</span>
        {required && <span className="text-danger"> * </span>}
        {!!tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <Input
        name={name}
        onChange={handleChange}
        value={value}
        style={{ width: '100%' }}
        disabled={disabled}
        placeholder={placeholder}
      />
      <small className="help-block">{!!error && <span>{error}</span>}</small>
    </div>
  </div>
);

/*
 * Input Text
 */
const InputTextArea = ({
  name,
  value,
  label,
  col,
  tooltip,
  required,
  disabled,
  placeholder,
  handleChange,
  error,
}) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group ${!!error && 'is-invalid'}`}>
      <label className="control-label">
        {label}
        <span style={{ color: 'transparent' }}>.</span>
        {required && <span className="text-danger"> * </span>}
        {!!tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <TextArea
        name={name}
        onChange={handleChange}
        value={value}
        style={{ width: '100%' }}
        rows={6}
        disabled={disabled}
        placeholder={placeholder}
      />
      <small className="help-block">{!!error && <span>{error}</span>}</small>
    </div>
  </div>
);

/*
 * Tag Text
 */
const TagText = ({ value, label, col, tooltip }) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group`}>
      <label className="control-label">
        {label}
        <span style={{ color: 'transparent' }}>.</span>
        {!!tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <div className="row" style={{ marginLeft: 10 }}>
        {value ? <Tag color="green">Sim</Tag> : <Tag color="red">Não</Tag>}
      </div>
    </div>
  </div>
);

/*
 * Switch
 */
const SwitchText = ({ value, label, col, tooltip, handleChange }) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group`}>
      <label className="control-label">
        {label}
        <span style={{ color: 'transparent' }}>.</span>
        {!!tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <div className="row" style={{ marginLeft: 5 }}>
        <Switch checked={value} onChange={handleChange} />
      </div>
    </div>
  </div>
);

/*
 * Input Password
 */
const InputPassword = ({
  name,
  value,
  label,
  col,
  tooltip,
  required,
  disabled,
  placeholder,
  handleChange,
  error,
}) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group ${!!error && 'is-invalid'}`}>
      <label className="control-label">
        {label}
        <span style={{ color: 'transparent' }}>.</span>
        {required && <span className="text-danger"> * </span>}
        {!!tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <Input.Password
        name={name}
        onChange={handleChange}
        value={value}
        style={{ width: '100%' }}
        disabled={disabled}
        placeholder={placeholder}
      />
      <small className="help-block">{!!error && <span>{error}</span>}</small>
    </div>
  </div>
);

/*
 * Input Number
 */
const InputNumber = ({
  name,
  value,
  label,
  col,
  tooltip,
  required,
  handleChange,
  min,
  max,
  error,
}) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group ${!!error && 'is-invalid'}`}>
      <label className="control-label">
        {label}
        <span style={{ color: 'transparent' }}>.</span>
        {required && <span className="text-danger"> * </span>}
        {!!tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <AntdInputNumber
        name={name}
        onChange={handleChange}
        min={min}
        max={max}
        value={value}
        style={{ width: '100%' }}
      />
      <small className="help-block">{!!error && <span>{error}</span>}</small>
    </div>
  </div>
);

/*
 * Date Picker
 */
const DatePicker = ({ name, defaultValue, label, col, tooltip, required, handleChange, error }) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group ${!!error && 'is-invalid'}`}>
      <label className="control-label">
        {label}
        <span style={{ color: 'transparent' }}>.</span>
        {required && <span className="text-danger"> * </span>}
        {!!tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <AntdDatePicker
        name={name}
        onChange={handleChange}
        format="DD/MM/YYYY"
        defaultValue={defaultValue ? moment(defaultValue, 'DD/MM/YYYY') : null}
        style={{ width: '100%' }}
      />
      <small className="help-block">{!!error && <span>{error}</span>}</small>
    </div>
  </div>
);

/*
 * Select
 */
const Select = ({
  name,
  value,
  dataSource,
  label,
  col,
  tooltip,
  required,
  handleChange,
  error,
  disabled,
  loading,
}) => (
  <div className={`col-md-${col}`}>
    <div className={`form-group ${!!error && 'is-invalid'}`}>
      <label className="control-label">
        {label}
        {required && <span className="text-danger"> * </span>}
        {tooltip && (
          <Tooltip title={tooltip}>
            <Icon type="question-circle-o" style={{ color: tooltipColor }} />
          </Tooltip>
        )}
      </label>
      <AntdSelect
        name={name}
        onChange={handleChange}
        value={value}
        style={{ width: '100%' }}
        disabled={disabled}
        loading={loading}>
        {dataSource.map((p) => (
          <Option key={p.id} value={p.id}>
            {p.description}
          </Option>
        ))}
      </AntdSelect>
      <small className="help-block">{!!error && <span>{error}</span>}</small>
    </div>
  </div>
);

/*
 * Lista de Definição
 */
const DefinitionList = ({ children, col }) => (
  <div className={`col-md-${col}`}>
    <dl className="dl-horizontal">{children}</dl>
  </div>
);

const DefinitionListItem = ({ label, children }) => (
  <>
    <dt>{label}</dt>
    <dd>{children}</dd>
  </>
);

export {
  InputText,
  InputPassword,
  InputNumber,
  DatePicker,
  Select,
  DefinitionList,
  DefinitionListItem,
  TagText,
  SwitchText,
  InputTextArea,
};
