import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

// https://vitejs.dev/config/
export default defineConfig({
  base: '/Management',
  plugins: [vue()],
  build: {
    chunkSizeWarningLimit: 3500,
    outDir: '../RC.ServerCenter.Application/Management'
  }
})
