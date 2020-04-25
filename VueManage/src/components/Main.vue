<template>
    <section class="app-main">
        <transition name="fade-transform" mode="out-in">
            <router-view/>
        </transition>
    </section>
</template>

<script>
  export default {
    name: "AppMain",
  }
</script>

<style scoped>
    .app-main {
        height: 100%;
        width: 100%;
        min-width: 1200px;
        position: relative;
        overflow: hidden;
    }
</style>


