<template>
  <div class="content">
    <div class="row justify-content-around">
      <div class="col-6 pr-10 pl-10 justify-content-center">
        <div class="header-box row justify-content-between align-items-center">
          <div class="header-text col-5">Trận đấu sắp tới</div>
          <div class="col-2">
            <router-link to="/abc"><div class="header-icon"></div></router-link>
          </div>
        </div>
        <div class="match-box justify-content-center">
        </div>
      </div>
      <div class="col-6 pr-10 pl-10 justify-content-center">
        <div class="header-box row justify-content-between align-items-center">
          <div class="header-text col-5">Kết quả</div>
          <div class="col-2">
            <div class="header-icon"></div>
          </div>
        </div>
        <div class="match-box">
          <component
            v-for="(item, index) in listResultMatch"
            v-bind:key="index"
            v-bind:is="item"
            v-bind="propsForResultMatch[index]"
          >
          </component>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.header-icon {
  height: 50px;
  width: 50px;
  border-radius: 25px;
  border: 1px solid #d8dedd;
  cursor: pointer;
  background-color: white;
  background-image: url("../../assets/images/icon-arrow-right.svg");
  background-position: center;
}
.header-text {
  font-size: 1.5rem;
  font-weight: 300;
}
.match-box {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>

<script>
export default {
  name: "Content",
  components: {
  },
  data: function () {
    return {
      listResultMatch: [],
      propsForResultMatch: [],
      count: 0,
    };
  },
  mounted() {
    // axios
    //   .get(
    //     " https://app.sportdataapi.com/api/v1/soccer/matches?apikey=1f7a3250-35a9-11ec-9133-43eb5e5c001d&season_id=1980&status_code=3"
    //   )
    //   .then((response) => {
    //     console.log(response);
    //   })
    //   .catch((error) => console.log(error));
  },
};
</script>
