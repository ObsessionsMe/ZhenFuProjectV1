<template>
  <div>
    <headerNav :title="name" />
    <el-main>
      <!-- <van-field readonly clickable name="picker" v-model="entity.GoodsName" label="产品" placeholder="请选择" @click="showGoodsPicker = true" />
      <van-popup v-model="showGoodsPicker" position="bottom">
        <van-picker value-key="goodsName" show-toolbar :columns="productList" @confirm="onGoodsConfirm" @cancel="showGoodsPicker = false" />
     
      </van-popup>-->

      <!-- <van-field  readonly clickable name="picker" v-model="entity.payTypeName" label="兑现方式" placeholder="请选择" @click="showPayPicker = true" />
      <van-popup v-model="showPayPicker" position="bottom">
        <van-picker value-key="name" show-toolbar :columns="pays" @confirm="onPayConfirm" @cancel="showPayPicker = false" />
      </van-popup>-->
      <template v-if="entity.payTypeName=='银行卡'">
        <van-field
          readonly
          clickable
          name="picker"
          v-model="entity.bankTypeName"
          label="所属银行"
          placeholder="请选择"
          @click="showBankPicker = true"
        />

        <van-field label="开户人姓名" placeholder="请输入开户人姓名" v-model="entity.bankUserName" />

        <van-field
          readonly
          clickable
          name="picker"
          :value="entity.provinceName+entity.cityName"
          label="开户地"
          placeholder="请选择"
          @click="showAreaPicker = true"
        />
        <van-popup v-model="showAreaPicker" position="bottom">
          <van-area
            :area-list="areaList"
            :columns-num="2"
            @confirm="onAreaConfirm"
            @cancel="showAreaPicker = false"
          />
        </van-popup>

        <van-popup v-model="showBankPicker" position="bottom">
          <van-picker
            value-key="name"
            show-toolbar
            :columns="banks"
            @confirm="onBankConfirm"
            @cancel="showBankPicker = false"
          />
        </van-popup>
      </template>
      <van-field :placeholder="'请输入'+entity.payTypeName+'账号'" v-model="entity.account" label="账号" />
      <van-field label="可用福豆" v-model="entity.integral" disabled />
      <van-field label="可兑现比例" class="red" :value="(entity.deductRate*100)+'%'" disabled />
      <van-cell title="兑现福豆">
        <template #input>
          <van-stepper
            id="deduct"
            :min="min"
            :max="maxDeduct()"
            v-model="entity.deduct"
            :step="100"
          />
        </template>
      </van-cell>
    </el-main>
    <el-footer>
      <!-- <el-button
        style="width:100%;"
        type="success"
        @click="onSubmit"
        round
      >提交</el-button> -->
      <!-- <el-button
        v-if="!IsDisabled"
        style="width:100%;"
        type="info"
        disabled
        round
      >提现时间为{{beginHour}}:00-{{endHour}}:00</el-button>   :disabled="isNotSubmit"  v-if="IsDisabled"-->
      <!-- <el-button style="width:100%;" type="success" @click="onSubmit" round>提交</el-button> -->
      <el-button :disabled="isNotSubmit" v-if="IsDisabled" style="width:100%;" type="success" @click="onSubmit" round>提交</el-button>
      <el-button v-if="!IsDisabled" style="width:100%;" type="info" disabled round>提现时间为{{beginHour}}:00-{{endHour}}:00</el-button>
    </el-footer>
  </div>
</template>
<script>
import areaList from "@/data/area";
import { submitCash, getCashDetail, recentCash } from "@/api/cash.js";
import { GetGoodsList } from "@/api/goods.js";
import {
  Area,
  Form,
  Field,
  Stepper,
  Picker,
  Button,
  Slider,
  CellGroup,
  Cell,
  Toast,
  Dialog,
} from "vant";

export default {
  components: {
    [Area.name]: Area,
  },
  data() {
    // var pays = ["支付宝", "微信", "银行卡"];
    var pays = [
      { id: 10001, name: "支付宝" },
      { id: 10002, name: "微信" },
      { id: 10003, name: "银行卡" },
    ];
    var banks = [
      { id: 10001, name: "工商银行" },
      { id: 10002, name: "交通银行卡" },
      { id: 10003, name: "农业银行卡" },
      { id: 10004, name: "建设银行卡" },
      { id: 10005, name: "中国银行卡" },
      { id: 10006, name: "招商银行卡" },
      { id: 10007, name: "邮政储蓄银行卡" },
    ];
    return {
      id: 0,
      name: "",
      IsDisabled: true,
      beginHour: 8,
      endHour: 21,
      showPayPicker: false,
      showBankPicker: false,
      showGoodsPicker: false,
      showAreaPicker: false,
      areaList,
      productList: [],
      pays: pays,
      banks: banks,
      isNotSubmit: false,
      min: 0,
      entity: {
        Type: 0,
        GoodsId: "",
        GoodsName: "",
        payType: 10003,
        payTypeName: "银行卡",
        bankType: 0,
        bankTypeName: "",
        bankUserName: "",
        provinceCode: "",
        provinceName: "",
        cityCode: "",
        cityName: "",
        account: "",
        integral: 0,
        deductRate: 0,
        deduct: 0,
        poundageRate: 0.0,
      },
    };
  },
  created() {
    const that = this;
    this.entity.Type = parseInt(this.$route.query.type);
    this.name = this.$route.query.name;

    this.init();
  },
  mounted() {
    const that = this;
    document.querySelector("#deduct input").onfocus = function () {
      that.isNotSubmit = true;
    };
    document.querySelector("#deduct input").onblur = function () {
      that.entity.deduct = parseInt(that.entity.deduct / 100) * 100;
      that.isNotSubmit = false;
    };
  },
  methods: {
    maxDeduct() {
      return (
        parseInt((this.entity.integral * this.entity.deductRate) / 100) * 100
      );
    },
    SetDisabled() {
      const hour = new Date().getHours();
      this.IsDisabled = hour >= this.beginHour && hour < this.endHour;
    },
    init() {
      this.SetDisabled();
      // this.$nextTick(function() {
      //   setInterval(function() {
      //     this.SetDisabled();
      //   }, 30000);
      // });

      // GetGoodsList().then(response => {
      //   if (response.state == "success") {
      //     console.log("response.data.shopDataList", response.data.shopDataList);
      //     this.productList = response.data.shopDataList;
      //     if (this.productList.length > 0) {
      //       this.onGoodsConfirm(this.productList[0]);
      //     }
      //   }
      // });
      //加载上一次兑现的账号信息
      recentCash().then((response) => {
        if (response.state == "success") {
          Object.assign(this.entity, response.data);
        }
      });
      getCashDetail(this.entity.Type, "0001").then((res) => {
        if (res.state == "success") {
          this.entity.integral = res.data.integral;
          this.entity.deductRate = res.data.deductRate;
          this.entity.poundageRate = res.data.poundageRate;
        }
      });
    },
    onGoodsConfirm(data) {
      this.entity.GoodsId = data.goodsId;
      this.entity.GoodsName = data.goodsName;
      this.showGoodsPicker = false;
      getCashDetail(this.entity.Type, data.goodsId).then((res) => {
        if (res.state == "success") {
          this.entity.integral = res.data.integral;
          this.entity.deductRate = res.data.deductRate;
          this.entity.poundageRate = res.data.poundageRate;
        }
      });
    },
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
    onAreaConfirm(data) {
      const province = data[0];
      const city = data[1];
      this.entity.provinceCode = province.code;
      this.entity.provinceName = province.name;
      this.entity.cityCode = city.code;
      this.entity.cityName = city.name;
      this.showAreaPicker = false;
    },
    Submit() {
      var that = this;
      that.isNotSubmit = true;

      getCashDetail(this.entity.Type, "0001").then((res) => {
        if (res.state == "success") {
          var checkData = res.data;
          // this.entity.integral = res.data.integral;
          // this.entity.deductRate = res.data.deductRate;
          // this.entity.poundageRate = res.data.poundageRate;
          if (
            that.entity.deduct >
            ((checkData.integral * checkData.deductRate) / 100) * 100
          ) {
            Toast.success("积分不足，兑现失败!");
            this.$router.push({ path: "/user/index" });
          } else {
            submitCash(that.entity).then((res) => {
              if (res.state == "success") {
                Toast.success(res.message);
                that.$router.push({ path: "/user/index" });
              } else {
                Toast.success(res.message);
              }
              that.isNotSubmit = false;
            });
          }
        }
      });
    },
    onSubmit() {
      if (this.entity.bankTypeName.length < 1) {
        Toast.fail("所属银行不能为空!");
        return;
      }

      if (this.entity.bankUserName.length < 1) {
        Toast.fail("开户人姓名不能为空!");
        return;
      }

      if (this.entity.account.length < 1) {
        Toast.fail("账号不能为空!");
        return;
      }

      if (this.entity.deduct == 0) {
        Toast.fail("提现福豆必须大于0!");
        return;
      }

      if (this.entity.Type > 0) {
        Dialog.confirm({
          title: "温馨提示",
          message:
            "福豆提现将会收取" +
            this.entity.poundageRate * 100 +
            "%的手续费，实际提现" +
            this.entity.deduct * (1 - this.entity.poundageRate) +
            "，请确认是否要进行兑现？",
        })
          .then(() => {
            this.Submit();
          })
          .catch(() => {});
      } else {
        this.Submit();
      }
    },
  },
};
</script>
<style>
.van-picker__cancel {
  font-size: 24px;
}

.van-picker__confirm {
  font-size: 24px;
}

.red .van-van-field__control {
  color: red !important;
}
</style>
