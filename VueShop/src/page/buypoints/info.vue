<template>
  <van-radio-group v-model="radio">
    <!-- <van-cell-group>
      <van-cell clickable @click="radio = '1'">
        <template #right-icon>
          <img height="40" width="150"  :src="require('@/assets/images/weixin_bg.png')" />
        </template>
      </van-cell>
      <van-cell clickable @click="radio = '2'">
        <template #right-icon>
            <img height="40" width="150" :src="require('@/assets/images/zhifubao_bg.png')" />
        </template>
      </van-cell>
    </van-cell-group>-->
    <van-cell-group>
      <van-cell clickable @click="radio = '1'">
        <img height="40" width="150" :src="require('@/assets/images/weixin_bg.png')" />
        <van-radio name="1" style="float:right" />
      </van-cell>
      <van-cell clickable @click="radio = '2'">
        <img height="40" width="150" :src="require('@/assets/images/zhifubao_bg.png')" />
        <van-radio name="2" style="float:right" />
      </van-cell>
    </van-cell-group>
  </van-radio-group>
</template>
<script>
export default {
  data() {
    return {
      radio: "1"
    };
  },
  methods: {}
};
</script>