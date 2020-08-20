<template>
  <div>
    <headerNav title="我的地址" />
    <!-- <van-address-list
      v-model="chosenAddressId"
      :class="isSelect?'':'hideselect'"
      :list="list"
      @add="onAdd"
      @edit="onEdit"
      @select="onSelect"
    /> -->
    <van-address-list
      v-model="chosenAddressId"
      :class="isSelect?'':'hideselect'"
      :list="list"
      show-delete
      @add="onAdd"
      @edit="onEdit"
      @select="onSelect"
    />
  </div>
</template>

<script>
import { GetUserAllAddress } from "../../../api/address.js";
import { AddressList } from "vant";
export default {
  components: {
    [AddressList.name]: AddressList
  },
  data() {
    return {
      chosenAddressId: "1",
      isSelect: false,
      list: []
    };
  },

  methods: {
    onAdd() {
      this.$router.push("/user/address/edit");
    },
    onEdit(item, index) {
      this.$router.push("/user/address/edit?id=" + item.id);
    },
    onSelect(item, index) {
      if (!this.isSelect) {
        return;
      }
      this.$emit("selectAddress", item);
      this.$router.go(-1);
    },
  },
  created: function() {
    //   receiveEntity.ReceiveUser = data.name;
    //   receiveEntity.DetailsAddress = data.addressDetail;
    //   receiveEntity.ReceiveTelephone = data.tel;
    //   receiveEntity.ReceiveProvinceName = data.province;
    //   receiveEntity.ReceiveCityName = data.city;
    //   receiveEntity.ReceiveAreaName = data.county;
    //   receiveEntity.ReceiveAreaCode = data.areaCode;
    //   receiveEntity.isDefalut = data.isDefalut == true ? "Y" : "N";

    this.chosenAddressId = this.$route.query.id;
    this.isSelect = this.$route.query.id > 0;
    GetUserAllAddress().then(response => {
      var addressList = response.data;
      for (var i = 0; i < addressList.length; i++) {
        var item = {};
        item.id = addressList[i].id;
        item.name = addressList[i].receiveUser;
        item.tel = addressList[i].receiveTelephone;
        item.addressDetail = addressList[i].detailsAddress;
        this.list.push(item);
      }
      console.log(this.list);
    });
  }
};
</script>

<style lang="less">
.hideselect {
  .van-radio__input {
    display: none;
  }
}
</style>
