<template>
  <div>
      <Introduce/>
      <Content/>
  </div>
</template>

<script>
import Content from "./Content";
import Introduce from "./Introduce";
import {formatDateMatch} from "@/utils/utils.js"
export default {
  name: "Homepage",
  components:{
      Content,
      Introduce
  },
  created(){
    formatDateMatch();
  }
};
</script>
<style>
</style>