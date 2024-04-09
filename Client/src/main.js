import Vue from 'vue';
import App from './App.vue';
import axios from 'axios';

Vue.config.productionTip = false;

new Vue({
  render: h => h(App),
  data: {
    kirjeldus: '',
    sisestamiseAeg: '',
    lahendamiseTähtaeg: ''
  },
  methods: {
    async submitPöördumine() {
      try {
        const sisestamiseAeg = new Date().toISOString;
        const response = await axios.post('http://localhost:5050/api/poordumine', {
          kirjeldus: this.kirjeldus,
          sisestamiseAeg: sisestamiseAeg,
          lahendamiseTähtaeg: this.lahendamiseTähtaeg,
          lahendatud: false 
        });
        console.log(response.data);
        alert('Pöördumine edukalt lisatud!');
      } catch (error) {
        console.error(error);
        alert('Viga pöördumise lisamisel!');
      }
    }
  }
}).$mount('#app');
