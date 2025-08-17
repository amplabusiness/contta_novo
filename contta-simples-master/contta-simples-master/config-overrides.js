const { override, fixBabelImports, addLessLoader } = require("customize-cra");

//https://ant.design/docs/react/use-with-create-react-app
module.exports = override(
  fixBabelImports("import", {
    libraryName: "antd",
    style: true
  }),
  addLessLoader({
    javascriptEnabled: true,
    modifyVars: {
      "@primary-color": "#18a689",
      "@font-size-base": "13px",
      "@border-radius-base": "4px",
      "@border-color-base": "#c9d3dd",
      "@input-height-base": "38px",
      "@body-background": "#2f4050",
      "@component-background": "#fff",
      "@text-color": "#2f4050",
      "@btn-height-base": "38px",
      "@font-family-no-number":
        '-apple-system, BlinkMacSystemFont, "Segoe UI", Roboto, "PingFang SC", "Hiragino Sans GB", "Microsoft YaHei", "Helvetica Neue", Helvetica, Arial, sans-serif',
      "@font-family":
        '"open sans, Helvetica Neue, Helvetica, Arial, sans-serif", @font-family-no-number',
      "@code-family": "Consolas, Menlo, Courier, monospace"
    }
  })
);
