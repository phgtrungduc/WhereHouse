<template>
  <div>
    <b>Start:</b>
    <select v-model="start">
      <option value="chicago, il">Chicago</option>
      <option value="st louis, mo">St Louis</option>
      <option value="joplin, mo">Joplin, MO</option>
    </select>
    <b>End:</b>
    <select v-model="end">
      <option value="chicago, il">Chicago</option>
      <option value="st louis, mo">St Louis</option>
      <option value="joplin, mo">Joplin, MO</option>
    </select>
    <GmapMap :zoom="7" :center="{ lat: 41.85, lng: -87.65 }">
      <DirectionsRenderer travelMode="DRIVING" :origin="origin" :destination="destionation"/>
    </GmapMap>
  </div>
</template>

<script>
import DirectionsRenderer from "@/components/GoogleMap/DirectionsRenderer";

export default {
  components: {
    DirectionsRenderer
  },

  data: () => ({
    start: "",
    end: ""
  }),

  computed: {
    origin() {
      if (!this.start) return null;
      return { query: this.start };
    },
    destionation() {
      if (!this.end) return null;
      return { query: this.end };
    }
  }
};
</script>

<style>
.vue-map-container {
  height: 300px;
}
</style>