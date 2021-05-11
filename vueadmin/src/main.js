import Vue from "vue";
import App from "./App.vue";
import ElmentUI from "element-ui";
import router from "@/router";
import VueCookies from "vue-cookies";
import "element-ui/lib/theme-chalk/index.css";

Vue.use(VueCookies);

Vue.use(ElmentUI);

Vue.config.productionTip = false;

Vue.prototype.$components = {};

new Vue({
  render: (h) => h(App),
  router,
}).$mount("#app");
