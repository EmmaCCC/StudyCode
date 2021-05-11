export default {
  created() {
    this.$components[this.$parent.name] = this;
  },
};
