import Vue from "vue";
import VueRouter from "vue-router";
const Home = () => import("@/views/Home");
const Login = () => import("@/views/Login");


Vue.use(VueRouter);

const routes = [
  {
    path: "/login",
    name: "login",
    component: Login
  },
  {
    path: "/",
    name: "home",
    component: Home
    // children: [
    //   {
    //     path: "/dashboard",
    //     name: "dashboard",
    //     component: Dashboard
    //   },
    //   {
    //     path: "/user",
    //     name: "user",
    //     component: User
    //   },
    //   {
    //     path: "/role",
    //     name: "role",
    //     component: Role
    //   }
    // ]
  },
  {
    path: "/",
    redirect: "/home"
  }
];

var router = new VueRouter({
  routes,
  mode: "history"
});

export default router;
