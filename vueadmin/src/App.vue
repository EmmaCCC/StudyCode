<template>
  <div id="app">
    <transition name="fade" mode="out-in">
			<router-view></router-view>
		</transition>
  </div>
</template>

<script>
export default {
  name: "app",
  components: {},
  data(){
    return {
      msg:"123",
    }
  },
  methods:{
    addTab(){
       console.log(this.$router.$options.$routes);
    }
  }
};
</script>

<style scoped>
@import "./assets/css/base.css";
.fade-enter-active,
.fade-leave-active {
  transition: all 0.01s ease;
}

</style>
