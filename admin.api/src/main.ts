import { createApp } from 'vue'
import './assets/css/global.scss'
// import './style.css' //使用scss替代这个
import App from './App.vue'
import router from './router'

import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import * as ElementPlusIconsVue from '@element-plus/icons-vue'
// 让日历组件可以显示中文，需要到src下面声明一下vite-env.d.ts才行
import zhCn from 'element-plus/dist/locale/zh-cn.mjs'
import print from 'vue3-print-nb'

import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)


const app = createApp(App)
app.use(print)  //打印
//图标，不加入不显示
for (const [key, component] of Object.entries(ElementPlusIconsVue)) {
    app.component(key, component)
}

// 中文
app.use(ElementPlus, {
    locale: zhCn,
})

app.use(pinia)
app.use(router)
app.use(ElementPlus)
app.mount('#app')

