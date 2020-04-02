<template>
<div>
  <div class="menu">
    <a v-on:click="push('/')">Home</a>
    <a v-on:click="push('/react16')">React16</a>
    <a v-on:click="push('/react15')">React15</a>
    <a v-on:click="push('/vue')">Vue</a>
    <a v-on:click="push('/angular9')">angular9</a>
    <a v-on:click="push('/purehtml')">Purehtml</a>
  </div>
  <div id='subapp-container'></div>
</div>
</template>
<script>
export default {
  name: 'MainApp',
  methods:{
    push(url){
      window.localStorage.setItem('subAppUrl', url);
      history.pushState(null, "newSubApp", url) 
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
  font-size: 16px;
  position: absolute;
  background-color: white;
  z-index: 9999;
  left: 0;
  bottom: 10px;
}
</style>