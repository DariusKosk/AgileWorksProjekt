<template>
  <div>
    <h2>Lisa pöördumine:</h2>
    <form @submit.prevent="esitaPoordumine">
      <div class="form-group">
        <label for="kirjeldus">Pöördumise kirjeldus:</label>
        <input type="text" id="kirjeldus" v-model="kirjeldus" placeholder="Pöördumise kirjeldus" required>
      </div>
      <div class="form-group">
        <label for="lahendamiseTähtaeg">Lahendamise tähtaeg:</label>
        <input type="datetime-local" id="lahendamiseTähtaeg" v-model="lahendamiseTähtaeg" required>
      </div>
      <button type="submit">Esita</button>
    </form>

    <div>
      <h2>Aktiivsed pöördumised:</h2>
      <table>
        <thead>
          <tr>
            <th>Pöördumise kirjeldus</th>
            <th>Tähtaeg</th>
            <th>Märgi lahendatuks</th> 
          </tr>
        </thead>
        <tbody>
          <tr v-for="poordumine in poordumised" :key="poordumine.id" :class="{ 'punane': tähtajaKontroll(poordumine.lahendamiseTähtaeg) }">
            <td>{{ poordumine.kirjeldus }}</td>
            <td>{{ formatDate(poordumine.lahendamiseTähtaeg) }}</td>
            <td>
              <button @click="lahendatud(poordumine.id)" class="lahendatud-btn">✅</button>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
import { v4 } from 'uuid';

export default {
  name: "PoordumineForm",
  data() {
    return {
      kirjeldus: '',
      lahendamiseTähtaeg: '',
      poordumised: [],
    };
  },
  methods: {
    async esitaPoordumine() {
      try {
        const id = v4();
        const response = await fetch('http://localhost:5050/api/poordumine', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json'
          },
          body: JSON.stringify({
            id: id,
            kirjeldus: this.kirjeldus,
            lahendamiseTähtaeg: this.lahendamiseTähtaeg
          })
        });
        if (response.ok) {
          this.saaPoordumised();
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
},
formatDate(dateString) {
  const date = new Date(dateString);
  return date.toLocaleDateString('et-EE', { weekday: 'long', year: 'numeric', month: 'long', day: 'numeric', hour: 'numeric', minute: 'numeric' });
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

table {
    width: 100%;
    border-collapse: collapse;
    margin-top: 20px;
  }

  th,
  td {
    padding: 8px;
    text-align: left;
    border-bottom: 1px solid #ddd;
  }

  th {
    background-color: #f2f2f2;
  }

  tr:nth-child(even) {
    background-color: #f2f2f2;
  }

  tr:hover {
    background-color: #ddd;
  }
.lahendatud-btn {
  background: none;
  border: none;
  text-align: center;
  text-decoration: none;
  display: inline-block;
  font-size: 30px;
  margin: 4px 2px;
  cursor: pointer;
  border-radius: 10px;
}
</style>
