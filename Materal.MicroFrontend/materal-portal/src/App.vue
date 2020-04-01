<template>
<div>
  <div class="menu">
    <a v-on:click="push('/')">Home</a>
    <a v-on:click="push('/myAngular')">MyAngular</a>
    <a v-on:click="push('/myVue')">MyVue</a>
  </div>
  <div id='container'></div>
</div>
</template>
<script>
export default {
  name: 'MainApp',
  methods:{
    push(url){
      window.localStorage.setItem('subAppUrl', url);
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
  left: 0;
  bottom: 10px;
}
</style>