<template>
  <div>
    <h2>Lisa pöördumine:</h2>
    <form>
      <input type="text" v-model="kirjeldus" placeholder="Pöördumise kirjeldus">
      <input type="datetime-local" v-model="lahendamiseTähtaeg" placeholder="Lahendamise tähtaeg">
      <button type="submit" @click="esitaPoordumine">Esita</button>
    </form>
    <div>
      <h2>Aktiivsed pöördumised:</h2>
      <ul>
        <li v-for="poordumine in poordumised" :key="poordumine.id" :class="{ 'punane': tähtajaKontroll(poordumine.lahendamiseTähtaeg) }">
        {{ poordumine.kirjeldus }} - Lahendamise tähtaeg: {{ poordumine.lahendamiseTähtaeg }}
        <button @click="lahendatud(poordumine.id)">✅</button>
      </li>
      </ul>
    </div>
  </div>
</template>

<script>
export default {
  name: "PoordumineForm",
  data() {
    return {
      kirjeldus: '',
      lahendamiseTähtaeg: '',
      poordumised: [],
      id:1
    };
  },
  methods: {
    async esitaPoordumine() {
      try {
        const response = await fetch('http://localhost:5050/api/poordumine', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            id: this.id,
            kirjeldus: this.kirjeldus,
            lahendamiseTähtaeg: this.lahendamiseTähtaeg
          })
        });
        if (response.ok) {
          this.saaPoordumised();
          this.id++;
          console.log('Pöördumine edukalt lisatud!');
        } else {
          throw new Error('Viga pöördumise lisamisel!');
        }
      } catch (error) {
        console.error(error);
        alert('Viga pöördumise lisamisel!');
      }
    },
    async saaPoordumised() {
      try {
        const response = await fetch('http://localhost:5050/api/poordumine');
        if (response.ok) {
          this.poordumised = await response.json();
        } else {
          throw new Error('Viga poordumiste laadimisel!');
        }
      } catch (error) {
        console.error(error);
        alert('Viga poordumiste laadimisel!');
      }
    },
    tähtajaKontroll(lahendamiseTähtaeg) {
      const tähtaeg = new Date(lahendamiseTähtaeg);
      const praeguneAeg = new Date();
      const vahe = Math.abs(tähtaeg - praeguneAeg) / 36e5; 
      return vahe < 1;
    },
    async lahendatud(pöördumineId) {
      try {
        const response = await fetch(`http://localhost:5050/api/poordumine/${pöördumineId}`, {
        method: 'DELETE'
        } 
      );
        if (response.ok) {
          this.saaPoordumised();
          console.log('Pöördumine on edukalt märgitud lahendatuks!');
         } else {
          throw new Error('Viga pöördumise lahendamisel!');
          }
        } catch (error) {
          console.error(error);
          alert('Viga pöördumise lahendamisel!');
  }
}
  },
  mounted() {
    this.saaPoordumised();
  }
};
</script>

<style>
.punane {
  color: red;
}
</style>
