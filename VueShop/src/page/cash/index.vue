<template>
  <div>
    <headerNav :title="name" />
    <el-main>
      <van-field readonly clickable name="picker" v-model="entity.pay" label="兑现方式" placeholder="请选择" @click="showPayPicker = true" />
      <van-popup v-model="showPayPicker" position="bottom">
        <van-picker show-toolbar :columns="pays" @confirm="onPayConfirm" @cancel="showPayPicker = false" />
      </van-popup>
      <template v-show="entity.pay=='银行卡'">
        <van-field readonly clickable name="picker" v-model="entity.bank" label="所属银行" placeholder="请选择" @click="showBankPicker = true" />
        <van-field label="开户人姓名" placeholder="请输入开户人姓名" v-model="entity.bankUserName" />
        <van-popup v-model="showBankPicker" position="bottom">
          <van-picker show-toolbar :columns="banks" @confirm="onBankConfirm" @cancel="showBankPicker = false" />
        </van-popup>
      </template>
      <van-field :placeholder="'请输入'+entity.pay+'账号'" v-model="entity.account" label="账号" />
      <van-field label="可用积分" v-model="entity.integral" disabled />
      <van-field label="可兑现比例" class="red" :value="(entity.deductRate*100)+'%'" disabled />
      <van-cell title="兑现积分">
        <template #input>
          <van-stepper :min="min" :max="parseInt((entity.integral*entity.deductRate)/100)*100" v-model="entity.deduct" :step="100" />
        </template>
      </van-cell>
    </el-main>
    <el-footer>
      <div style="margin: 16px;">
        <van-button round block type="info" native-type="submit">
          提交
        </van-button>
      </div>
    </el-footer>
  </div>
</template>
<script>
import {
  Form,
  Field,
  Stepper,
  Picker,
  Button,
  Slider,
  CellGroup,
  Cell
} from "vant";

export default {
  data() {
    var pays = ["支付宝", "微信", "银行卡"];
    var banks = [
      "工商银行",
      "交通银行卡",
      "农业银行卡",
      "建设银行卡",
      "中国银行卡",
      "招商银行卡",
      "邮政储蓄银行卡"
    ];
    return {
      id: 0,
      name: "",
      showPayPicker: false,
      showBankPicker: false,
      pays: pays,
      banks: banks,
      min: 0,
      entity: {
        pay: "",
        bank: "",
        bankUserName: "",
        account: "",
        integral: 950,
        deductRate: 0.5,
        deduct: 100
      }
    };
  },
  created() {
    this.id = this.$route.query.id;
    this.name = this.$route.query.name;
  },
  methods: {
    onPayConfirm(data) {
      this.entity.pay = data;
      this.showPayPicker = false;
    },
    onBankConfirm(data) {
      this.entity.bank = data;
      this.showBankPicker = false;
    },
    onSubmit(values) {
      console.log("submit", values);
    }
  }
};
</script>
<style>
.red .van-van-field__control {
  color: red !important;
}
</style>
