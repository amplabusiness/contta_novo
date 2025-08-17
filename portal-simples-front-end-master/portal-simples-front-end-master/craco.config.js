const CracoAntDesignPlugin = require('craco-antd');
const { BundleAnalyzerPlugin } = require('webpack-bundle-analyzer');
const path = require('path');

module.exports = function ({ env }) {
  const isProductionBuild = process.env.NODE_ENV === 'production';
  const analyzerMode = process.env.REACT_APP_INTERACTIVE_ANALYZE
    ? 'server'
    : 'json';

  const plugins = [];

  if (isProductionBuild) {
    plugins.push(new BundleAnalyzerPlugin({ analyzerMode }));
  }

  return {
    webpack: {
      alias: {
        '@': path.resolve(__dirname, 'src/'),
      },
      configure: (webpackConfig, { env }) => {
        if (env === 'production') {
          const instanceOfMiniCssExtractPlugin = webpackConfig.plugins.find(
            plugin => plugin.constructor.name === 'MiniCssExtractPlugin',
          );

          if (instanceOfMiniCssExtractPlugin) {
            instanceOfMiniCssExtractPlugin.options.ignoreOrder = true;
          }
        }

        return webpackConfig;
      },
      plugins,
    },
    jest: {
      configure: {
        moduleNameMapper: {
          '^@(.*)$': '<rootDir>/src$1',
        },
      },
    },
    plugins: [
      {
        plugin: CracoAntDesignPlugin,
        options: {
          customizeTheme: {
            '@primary-color': '#337ab7',
            '@border-radius-base': '4px',
            '@border-color-base': '#c9d3dd',
            '@input-height-base': '40px',
            '@body-background': '#f3f3f4',
            '@component-background': '#fff',
            '@text-color': '#676a6c',
            '@heading-color': '#676a6c',
            '@modal-header-bg': '#3276b1',
            '@modal-heading-color': '#fff',
            '@modal-close-color': '#fff',
            '@table-row-hover-bg': 'rgba(50, 118, 177, 0.2)',
            '@table-header-bg': '#fff',
            '@table-header-color': '#333',
            '@btn-height-base': '40px',
            '@disabled-color': '#8e8e8e',
            '@font-family':
              '"Open Sans", Roboto, "PingFang SC", "Hiragino Sans GB", "Microsoft YaHei", "Helvetica Neue", Helvetica, Arial, sans-serif',
          },
        },
      },
    ],
  };
};
