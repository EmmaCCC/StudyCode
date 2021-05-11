import axios from "axios";
import { Message } from "element-ui";
import VueCookies from "vue-cookies";
import router from "@/router";

axios.defaults.headers["X-Requested-With"] = "XMLHttpRequest";

const service = axios.create({
  baseURL: "http://localhost:21222/",
});

//添加请求拦截器
service.interceptors.request.use(
  (config) => {
    var token = VueCookies.get("token");
    config.headers["Authorization"] = "Bearer " + token;
    return config;
  },
  (error) => {
    console.log("request", error);
  }
);

service.interceptors.response.use(
  (response) => {
    //服务器返回都是200
    return response.data;
  },
  (error) => {
    console.log("response", error);
    return {
      data: {
        code: 600,
        msg: "服务器维护中",
        data: null,
      },
    };
  }
);

var http = {
  get: function (url, params, success, fail) {
    var config = {
      url: url,
      params: params,
      method: "get",
    };
    this.request(config, success, fail);
  },
  post: function (url, data, success, fail) {
    var config = {
      url: url,
      data: data,
      method: "post",
    };
    this.request(config, success, fail);
  },
  request(config, success, fail) {
    service.request(config).then((data) => {
      if (data.code == 401) {
        //未登录
        router.push("login");
      }
      if (data.code == 402) {
        //token 失效 使用refreshtoken 换取token继续请求
        var refreshToken = VueCookies.get("refreshToken");
        this.get(
          "api/auth/refreshtoken",
          { refreshToken },
          (res) => {
            console.log('get refreshtoken');
            //1.存储token
            VueCookies.set("token", res.data.token, res.data.expires);
            VueCookies.set("refreshToken", res.data.token, res.data.expires * 12 * 10);
            //继续调用当前请求
            this.request(config, success, fail);
          },
          (fail) => {
            //refreshToken失效，去往登录页面
            router.push("login");
          }
        );
      }

      if (data.code == 0) {
        success(data);
      } else {
        if (!fail) {
          Message({ message: data.msg, type: "error" });
        }
        fail && fail(data);
      }
    });
  },
};

export default http;
