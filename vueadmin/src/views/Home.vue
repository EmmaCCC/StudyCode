<template>
  <div class="home">
    <div class="container flex-column">
      <div class="header">
        <div class="logo flex center">
          <img src="@/assets/logo.png" alt />
          <span>企业ERP管理系统</span>
        </div>
        <div class="profile flex center">
          <el-badge :value="3" class="item">
            <span class="el-dropdown-link">消息</span>
          </el-badge>
          <el-dropdown @command="handleCommand">
            <span class="el-dropdown-link">
              亲爱的xxx，你好
              <i class="el-icon-arrow-down el-icon--right"></i>
            </span>
            <el-dropdown-menu slot="dropdown">
              <el-dropdown-item>个人信息</el-dropdown-item>
              <el-dropdown-item>修改密码</el-dropdown-item>
              <el-dropdown-item command="logout" divided
                >注销登录</el-dropdown-item
              >
            </el-dropdown-menu>
          </el-dropdown>
        </div>
      </div>
      <div class="container">
        <div class="aside">
          <el-menu
            class="el-menu-vertical-demo"
            background-color="#545c64"
            text-color="#fff"
            active-text-color="#ffd04b"
            @select="selectMenu"
          >
            <el-submenu
              v-for="menu in menus"
              :key="menu.name"
              :index="menu.name"
            >
              <template slot="title">
                <i :class="menu.icon"></i>
                <span>{{ menu.name }}</span>
              </template>
              <el-menu-item
                v-for="item in menu.items"
                :key="item.name"
                :index="item.component"
              >
                <i :class="item.icon"></i>
                <span slot="title">{{ item.name }}</span>
              </el-menu-item>
            </el-submenu>
          </el-menu>
        </div>
        <div class="main">
          <div class="breadnav">
            <el-breadcrumb separator="/">
              <el-breadcrumb-item>{{
                new Date().toLocaleDateString()
              }}</el-breadcrumb-item>
              <el-breadcrumb-item>{{
                weeks[new Date().getDay()]
              }}</el-breadcrumb-item>
            </el-breadcrumb>
          </div>
          <el-divider></el-divider>
          <div class="content">
            <el-tabs
              v-model="currentTabName"
              type="border-card"
              closable
              @tab-remove="removeTab"
              @tab-click="clickTab"
            >
              <el-tab-pane
                v-for="item in tabs"
                :key="item.name"
                :label="item.title"
                :name="item.name"
              >
                <component :is="item.content" :param="item.param" />
              </el-tab-pane>
            </el-tabs>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import globalBus from "@/global/bus";
const User = () => import("@/views/user/User");
const UserDetail = () => import("@/views/user/UserDetail");
const Role = () => import("@/views/role/Role");
const Dashboard = () => import("@/views/dashboard/Dashboard");
import Vue from "vue";

export default {
  components: {
    User,
    Dashboard,
    Role,
    UserDetail
  },
  data() {
    return {
      weeks: [
        "",
        "星期一",
        "星期二",
        "星期三",
        "星期四",
        "星期五",
        "星期六",
        "星期日",
      ],
      menus: [
        {
          name: "工作台",
          component: "",
          icon: "el-icon-monitor",
          items: [
            {
              name: "主面板",
              component: "Dashboard",
              icon: "el-icon-tickets",
            },
          ],
        },
        {
          name: "系统管理",
          component: "",
          icon: "el-icon-setting",
          items: [
            {
              name: "人员管理",
              component: "User",
              icon: "el-icon-user",
            },
            {
              name: "角色管理",
              component: "Role",
              icon: "el-icon-document-remove",
            },
          ],
        },
      ],
      currentTabName: "Dashboard",
      tabs: [
        {
          title: "主面板",
          name: "Dashboard",
        },
      ],
      refreshKey: 0,
    };
  },

  created() {
    Vue.prototype.$home = this;
  },
  methods: {
    handleCommand(command) {
      if (command === "logout") {
        this.$router.push("login");
      }
    },
    selectMenu(component, data) {
      let title = "";
      for (let i = 0; i < this.menus.length; i++) {
        const menu = this.menus[i];
        for (let j = 0; j < menu.items.length; j++) {
          const item = menu.items[j];
          if (item.component === component) {
            title = item.name;
            break;
          }
        }
      }

      var tab = this.tabs.findIndex((a) => a.name == component);
      if (tab < 0) {
        this.tabs.push({
          title: title,
          name: component,
          content: component,
        });
      }
      this.currentTabName = component;
    },
    clickTab(tab) {
      console.log(this.$components[tab.name]);
    },

    addTab(data) {
      let name = data.name;
      let tab = this.tabs.findIndex((a) => a.name == name);
      if (tab < 0) {
        this.tabs.push({
          title: data.title,
          name: data.name,
          content: data.content,
          param: data.param,
        });
      }
      this.currentTabName = name;
    },
    removeTab(targetName) {
      let tabs = this.tabs;
      let activeName = this.currentTabName;
      if (activeName === targetName) {
        tabs.forEach((tab, index) => {
          if (tab.name === targetName) {
            let nextTab = tabs[index + 1] || tabs[index - 1];
            if (nextTab) {
              activeName = nextTab.name;
            }
          }
        });
      }

      this.currentTabName = activeName;
      this.tabs = tabs.filter((tab) => tab.name !== targetName);

       this.$components[targetName] = null;
    },
  },
};
</script>
<style scoped>
.fade-enter-active,
.fade-leave-active {
  transition: all 0.1s ease;
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}
.home {
  height: 100%;
}

.container,
.aside,
.main {
  height: 100%;
}

.aside,
.header {
  background-color: rgb(84, 92, 100);
}

.header {
  height: 65px;
  display: flex;
  justify-content: space-between;
  border-bottom: 1px solid #393d49;
}

.header .logo {
  width: 260px;
  color: #dfdddd;
  font-size: 20px;
  font-weight: bold;
}

.header .logo img {
  width: 50px;
  height: 50px;
}

.header .profile {
  justify-content: flex-start;
}
.header .profile,
.header .profile .el-dropdown {
  font-size: 15px;
  color: #dfdddd;
  margin: 12px;
}

.aside {
  width: 260px;
}
.el-menu {
  border-right: none;
}
.main {
  background-color: #f2f2f2;
  flex: 1;
}
.main .el-breadcrumb {
  font-size: 15px;
}
.main .breadnav {
  padding: 15px;
  background-color: #fff;
  margin: 15px;
}

.main .content {
  background-color: #fff;
  margin-top: 15px;
  padding: 15px;
}
</style>
