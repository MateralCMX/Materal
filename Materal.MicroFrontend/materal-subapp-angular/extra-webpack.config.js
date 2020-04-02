const singleSpaAngularWebpack = require('single-spa-angular/lib/webpack').default;
const webpackMerge = require('webpack-merge');

module.exports = (angularWebpackConfig, options) => {
  const singleSpaWebpackConfig = singleSpaAngularWebpack(angularWebpackConfig, options);
  // return singleSpaWebpackConfig
  const singleSpaConfig = {
    output: {
      library: 'myAngular',
      libraryTarget: 'umd',
    },
    externals: {
      'zone.js': 'Zone',
    }
  };
  const mergedConfig = webpackMerge.smart(singleSpaWebpackConfig, singleSpaConfig);
  return mergedConfig;
};