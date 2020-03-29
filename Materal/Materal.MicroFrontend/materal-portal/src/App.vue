<template>
<div>
  <div class="menu">
    <a v-on:click="push('/')">Home</a>
    <a v-on:click="push('/vue')">Vue</a>
    <a v-on:click="push('/angular9')">Angular</a>
    <a v-on:click="push('/react15')">React15</a>
    <a v-on:click="push('/react16')">React16</a>
  </div>
  <div id='container'></div>
</div>
</template>
<script>
export default {
  name: 'MainApp',
  methods:{
    push(url){
      history.pushState(null, url, url) 
    }
  }
}
</script>
<style scoped>
a{
  margin-left: 10px;
  color: cornflowerblue;
  cursor: pointer;
}
.menu {
  position: absolute;
  background-color: white;
  z-index: 9999;
}
</style>