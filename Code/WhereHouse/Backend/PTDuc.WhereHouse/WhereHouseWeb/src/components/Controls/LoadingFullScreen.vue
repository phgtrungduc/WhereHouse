<template>
  <div class="wrap-loading">
    <div class="loader"></div>
  </div>
</template>

<script>
export default {
  name: "LoadingFullScreen",
};
</script>

<style lang="scss" scoped>
.wrap-loading {
    background-color: #ffffffa3;
    height: 100vh;
    width: 100vw;
    z-index: 100000;
    position: absolute;
    top: 0;
    left: 0;
    bottom: 0;
    right: 0;
    display: flex;
    align-items: center;
  .loader {
    margin: auto;
    border: 3px solid #f3f3f3;
    border-radius: 50%;
    border-top: 3px solid #919ba2;
    width: 30px;
    height: 30px;
    -webkit-animation: spin 2s linear infinite; /* Safari */
    animation: spin 2s linear infinite;
  }

  /* Safari */
  @-webkit-keyframes spin {
    0% {
      -webkit-transform: rotate(0deg);
    }
    100% {
      -webkit-transform: rotate(360deg);
    }
  }

  @keyframes spin {
    0% {
      transform: rotate(0deg);
    }
    100% {
      transform: rotate(360deg);
    }
  }
}
</style>