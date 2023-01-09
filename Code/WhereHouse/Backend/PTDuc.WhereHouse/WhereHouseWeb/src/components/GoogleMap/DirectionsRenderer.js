import { MapElementFactory } from "vue2-google-maps";

export default MapElementFactory({
  name: "directionsRenderer",

  ctr() {
    return window.google.maps.DirectionsRenderer;
  },

  events: [],

  mappedProps: {},

  props: {
    origin: { type: Object },
    destination: { type: Object },
    travelMode: { type: String }
  },

  afterCreate(directionsRenderer) {
    let directionsService = new window.google.maps.DirectionsService();

    this.$watch(
      () => [this.origin, this.destination, this.travelMode],
      () => {
        let { origin, destination, travelMode } = this;
        if (!origin || !destination || !travelMode) return;

        directionsService.route(
          {
            origin: new window.google.maps.LatLng(origin),
            destination: new window.google.maps.LatLng(destination),
            travelMode: window.google.maps.TravelMode.DRIVING,
          },
          (response, status) => {
            if (status !== "OK") return;
            directionsRenderer.setDirections(response);
          }
        );
      }
    );
  }
});
