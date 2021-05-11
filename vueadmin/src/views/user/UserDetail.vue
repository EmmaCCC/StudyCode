<template>
  <div class="">user detail {{ param.id }}</div>
</template>

<script>
import mixin from "@/global/mixin";

export default {
  mixins:[mixin],
  props: {
    param: { type: Object },
  },
  data() {
    return {};
  },
  components: {},
  created() {},
  destroyed(){
    console.log('destory',this.param.id);
  },
  methods: {},
};
</script>
<style lang="scss" scoped>
</style>