<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <div id="app">
      <form @submit.prevent="submitPöördumine">
        <input type="text" v-model="kirjeldus" placeholder="Pöördumise kirjeldus">
        <input type="datetime-local" v-model="lahendamiseTähtaeg" placeholder="Lahendamise tähtaeg">
        <button type="submit">Esita</button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: 'HelloWorld',
  props: {
    msg: String
  },
  data() {
    return {
      kirjeldus: '',
      sisestamiseAeg: '',
      lahendamiseTähtaeg: ''
    }
  },
  methods: {
    submitPöördumine() {
      console.log('Pöördumine:', this.kirjeldus);
      console.log('Sisestamise aeg:', this.sisestamiseAeg);
      console.log('Lahendamise tähtaeg:', this.lahendamiseTähtaeg);
      alert('Pöördumine edukalt lisatud!');
    }
  }
}
</script>

<style scoped>
h1 {
  margin: 40px 0 0;
}
#app {
  text-align: center;
  margin-top: 20px;
}
input, button {
  margin: 5px;
}
</style>
