<template>
  <div class="note-have-right">
    <div class="image">

    </div>
    <h2>Không có quyền truy cập</h2>
  </div>
</template>

<script>
export default {
    name:"NotHaveRole"
}
</script>

<style lang="scss" scoped>
.note-have-right{
    padding-top: 50px;
    display: flex;
    align-items: center;
    flex-direction: column;
    .image{
        background: url(../assets/images/nothaverole.png);
        width: 256px;
        height: 256px;
    }
}
</style>>