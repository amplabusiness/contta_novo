import ReactDOM from 'react-dom';

import * as serviceWorker from '@/serviceWorker';

import App from '@/App';

import 'antd/dist/antd.less';

ReactDOM.render(<App />, document.getElementById('root'));

serviceWorker.unregister();
