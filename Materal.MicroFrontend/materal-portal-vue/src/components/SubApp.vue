<template>
  <div id="subapp-container">
    <h4 v-if="loading" class="subapp-loading">Loading...</h4>
    <div v-html="appContent" />
  </div>
</template>

<script>
export default {
  name: "SubApp",
  props: {
    loading: Boolean,
    appContent: String
  }
};
</script>