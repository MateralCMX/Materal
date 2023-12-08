import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
  base: '/Management',
  plugins: [vue()],
  build: {
      chunkSizeWarningLimit: 3500,
      outDir: '../Materal.Gateway.WebAPI/Management'
  }
})
