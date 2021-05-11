<template>
  <div>
    dashboard
  </div>
</template>

<script>
import http from "@/utils/http";


export default {
  props: {},
  data() {
    return {};
  },
  components: {},
  created() {
    this.getData();
  },
  methods: {
    getData() {
      // http.get("api/values",null,(res)=>{
      //   console.log(res);
      // },error=>{
      //   console.log(error);
      // })
    }
  }
};
</script>
<style lang="scss" scoped></style>
