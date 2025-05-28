import type { CapacitorConfig } from '@capacitor/cli';

const config: CapacitorConfig = {
  appId: 'ionic.pipelinemobile',
  appName: 'PipeLine',
  webDir: 'dist',
  "plugins": {
    "SplashScreen": {
      "launchShowDuration": 3000,
      "launchAutoHide": false, 
      "backgroundColor": "#ffffff",
      "showSpinner": true
    }
  }
};


export default config;
