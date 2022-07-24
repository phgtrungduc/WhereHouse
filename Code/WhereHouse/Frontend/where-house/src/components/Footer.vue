<template>
  <footer>
    <div class="container">
      <div class="row">
        <div class="col-12">
          <div class="text-center py-5">
            <img id="logo" src="../assets/images/wherehouselogo.svg" />
          </div>
        </div>
      </div>
    </div>
  </footer>
</template>

<script>
export default {
  name: "Footer",
};
</script>

<style scoped>
li {
  padding-left: 0;
  padding-bottom: 10px;
}

a {
  text-decoration: none;
}

.footer-link {
  color: #ddd;
}

#logo {
  width: 50px;
}

footer {
  font-family: "Roboto", sans-serif;
  background-color: #232f3e;
  font-size: 16px;
  height: 90px!important;
}
</style>
