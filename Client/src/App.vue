<template>
  <div id="app">
    <Poordumine msg="Pöördumise lisamine"/>
  </div>
</template>

<script>
import Poordumine from './components/Poordumine.vue'

export default {
  name: 'App',
  components: {
    Poordumine
  }
}
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
