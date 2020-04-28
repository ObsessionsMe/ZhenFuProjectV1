<template>
  <div>
    <headerNav :title="name" />
    <el-main>
      <van-field readonly clickable name="picker" v-model="entity.payTypeName" label="兑现方式" placeholder="请选择" @click="showPayPicker = true" />
      <van-popup v-model="showPayPicker" position="bottom">
        <van-picker value-key="name" show-toolbar :columns="pays" @confirm="onPayConfirm" @cancel="showPayPicker = false" />
      </van-popup>
      <template v-if="entity.payTypeName=='银行卡'">
        <van-field readonly clickable name="picker" v-model="entity.bankTypeName" label="所属银行" placeholder="请选择" @click="showBankPicker = true" />
        <van-field label="开户人姓名" placeholder="请输入开户人姓名" v-model="entity.bankUserName" />
        <van-popup v-model="showBankPicker" position="bottom">
          <van-picker value-key="name" show-toolbar :columns="banks" @confirm="onBankConfirm" @cancel="showBankPicker = false" />
        </van-popup>
      </template>
      <van-field :placeholder="'请输入'+entity.payTypeName+'账号'" v-model="entity.account" label="账号" />
      <van-field label="可用积分" v-model="entity.integral" disabled />
      <van-field label="可兑现比例" class="red" :value="(entity.deductRate*100)+'%'" disabled />
      <van-cell title="兑现积分">
        <template #input>
          <van-stepper :min="min" :max="parseInt((entity.integral*entity.deductRate)/100)*100" v-model="entity.deduct" :step="100" />
        </template>
      </van-cell>
    </el-main>
    <el-footer>
      <el-button style="width:100%;" type="success" @click="onSubmit" round>提交</el-button>
    </el-footer>
  </div>
</template>
<script>
import { submitCash } from "@/api/cash.js";

import {
  Form,
  Field,
  Stepper,
  Picker,
  Button,
  Slider,
  CellGroup,
  Cell,
  Toast
} from "vant";

export default {
  data() {
    // var pays = ["支付宝", "微信", "银行卡"];
    var pays = [
      { id: 10001, name: "支付宝" },
      { id: 10002, name: "微信" },
      { id: 10003, name: "银行卡" }
    ];
    var banks = [
      { id: 10001, name: "工商银行" },
      { id: 10002, name: "交通银行卡" },
      { id: 10003, name: "农业银行卡" },
      { id: 10004, name: "建设银行卡" },
      { id: 10005, name: "中国银行卡" },
      { id: 10006, name: "招商银行卡" },
      { id: 10007, name: "邮政储蓄银行卡" }
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
        Type: 0,
        payType: 0,
        payTypeName: "",
        bankType:0,
        bankTypeName: "",
        bankUserName: "",
        account: "",
        integral: 950,
        deductRate: 0.5,
        deduct: 100
      }
    };
  },
  created() {
    this.entity.Type=parseInt(this.$route.query.type);
    this.name = this.$route.query.name;
  },
  methods: {
    onPayConfirm(data) {
      this.entity.payType = data.id;
      this.entity.payTypeName = data.name;
      this.showPayPicker = false;
    },
    onBankConfirm(data) {
      this.entity.bankType = data.id;
      this.entity.bankTypeName = data.name;
      this.showBankPicker = false;
    },
    onSubmit() {
      console.log("submit", this.entity);
      submitCash(this.entity).then(res => {
        console.log(res)
        if(res.state=="success"){
          Toast.success(res.message);
           this.$router.push({path:'/user/index'})
        }
      });
    }
  }
};
</script>
<style>
.red .van-van-field__control {
  color: red !important;
}
</style>
