<template>
  <div>
    <headerNav title="积分充值" />
    <van-radio-group v-model="payMethod" style="margin-top:10%;">
      <van-cell-group>
        <van-cell clickable @click="payMethod = '1'">
          <img height="30" width="120" :src="require('@/assets/images/weixin_bg.png')" />
          <van-radio name="1" style="float:right" />
        </van-cell>
        <van-cell clickable @click="payMethod = '2'">
          <img height="28" width="110" :src="require('@/assets/images/zhifubao_bg.png')" />
          <van-radio name="2" style="float:right" />
        </van-cell>
      </van-cell-group>
    </van-radio-group>
    <van-cell-group style="margin-top:5%">
      <van-field v-model="PayNum" label="金额" placeholder="请输入充值金额" type="number" />
    </van-cell-group>
    <van-cell-group style="margin-top:5%">
      <van-button
        size="large"
        type="primary"
        style="height:45px;line-height:45px;"
        @click="Pay()"
      >充值</van-button>
    </van-cell-group>
  </div>
</template>
<script>
export default {
  data() {
    return {
      payMethod: "1",
      PayNum: ""
    };
  },
  methods: {
    Pay() {
      if (parseInt(this.PayNum) <= 0) {
        return;
      }
      if (parseInt(this.PayNum) >= 1000000) {
        this.$toast("你的金额过大");
        return;
      }
    }
  }
};
</script>