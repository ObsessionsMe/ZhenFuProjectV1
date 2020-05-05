<template>
  <div class="cap-image-ad__image-nav">
    <div
      v-for="(item,index)  in imagelist"
      :key="index"
      class="image-wrapper"
      style="width:25%;margin-right:0px;"
    >
      <a :href="item.link" class="cap-image-ad__link cap-image-ad__link--image-nav">
        <div class="cap-image-ad__image">
          <img v-lazy="item.src+'?w=320'" style="width: 100%;" />
        </div>
      </a>
    </div>
  </div>
</template>

<script>
export default {
  name: "imageText",
  props: {
    data: Object
  },
  data: function() {
    return {
      imagelist: [
        {
          link: "/#/list/4",
          src: "http://47.115.57.178/resource/images/cfyp.jpg"
        },
        {
          link: "/#/list/5",
          src: "http://47.115.57.178/resource/images/jydq.jpg"
        },
        {
          link: "/#/list/6",
          src: "http://47.115.57.178/resource/images/qcyp.jpg"
        },
        {
          link: "/#/list/7",
          src: "http://47.115.57.178/resource/images/sjsm.jpg"
        }
      ]
    };
  },
  created: function() {
    this.data.width = 100 / this.data.imagelist.length;
  }
};
</script>

<style>
</style>
