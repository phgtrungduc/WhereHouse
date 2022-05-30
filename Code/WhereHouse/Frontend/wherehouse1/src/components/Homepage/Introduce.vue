<template>
  <div class="introduce">
    <div class="title-page">
      Trang Web cung cấp thông tin về các giải đấu trên các hệ thống giải hàng
      đầu thế giới
    </div>
  </div>
</template>
<style scoped>
.introduce {
  height: calc(100vh - 100px);
  display: flex;
  flex-direction: column;
  justify-content: center;
}
.title-page {
  font-size: 3.25rem!important;
  font-weight: 300;
  letter-spacing: -1px!important;
  width: 100%;
  line-height: 3.25rem;
  padding: 10px 0 10px 0;
}
</style>
<script>
export default {
  name: "Introduce",
};
</script>
