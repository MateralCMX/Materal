import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'

export default defineConfig({
    base: '/GatewayManagement',
    plugins: [vue()],
    build: {
        chunkSizeWarningLimit: 3500,
        outDir: '../Materal.Gateway.Application/GatewayManagement'
    }
})
