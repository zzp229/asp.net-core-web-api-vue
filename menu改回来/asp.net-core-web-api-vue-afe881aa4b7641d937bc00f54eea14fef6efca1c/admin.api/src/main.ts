import { createApp } from 'vue'
import './assets/css/global.scss'
// import './style.css' //使用scss替代这个
import App from './App.vue'
import router from './router'

import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import { createPinia } from 'pinia'
import piniaPluginPersist from 'pinia-plugin-persist'

const app = createApp(App)
app.use(router)
app.use(ElementPlus)
app.use(createPinia().use(piniaPluginPersist))
app.mount('#app')
