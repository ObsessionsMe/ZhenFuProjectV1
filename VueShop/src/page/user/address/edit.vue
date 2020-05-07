<template>
  <div>
    <headerNav title="修改地址" />
    <!-- <van-address-edit
      :area-list="areaList"
      :showDelete="showDelete"
      show-set-default
      @save="onSave"
      @delete="onDelete"
      :addressInfo="info"
    />-->
    <van-address-edit
      :area-list="areaList"
      :showDelete="showDelete"
      show-set-default
      :addressInfo="info"    
      @save="onSave"
      @delete="onDelete"
    />
  </div>
</template>

<script>
import areaList from "../../../data/area";
import { SaveAddress, DelAddress } from "../../../api/user.js";
import { SubmitAddress, GetAddressById } from "../../../api/address.js";

import { AddressEdit } from "vant";
export default {
  components: {
    [AddressEdit.name]: AddressEdit
  },
  data() {
    return {
      areaList,
      showDelete: false,
      info: {},
      infos:[]
    };
  },

  methods: {
    onSave(data) {
      var receiveEntity = {};
      console.log(data);
      var UserId = this.$store.state.userInfo.userId;
      if (UserId == null || UserId == "") {
        return;
      }
      receiveEntity.UserId = UserId;
      receiveEntity.ReceiveUser = data.name;
      receiveEntity.DetailsAddress = data.addressDetail;
      receiveEntity.ReceiveTelephone = data.tel;
      receiveEntity.ReceiveProvinceName = data.province;
      receiveEntity.ReceiveCityName = data.city;
      receiveEntity.ReceiveAreaName = data.county;
      receiveEntity.ReceiveAreaCode = data.areaCode;
      receiveEntity.isDefalut = data.isDefault == true ? "Y" : "N";
      console.log("receiveEntity", receiveEntity);
      SubmitAddress(receiveEntity).then(response => {
        //this.$toast('保存成功');
        //this.$router.go(-1);
      });
    },
    onDelete(data) {
      // eslint-disable-next-line no-unused-vars
      // DelAddress(data).then(response=>{
      //   this.$toast('删除成功');
      //   this.$router.go(-1);
      // })
    }
  },
  created: function() {
    var id = this.$route.query.id;
    if (id > 0) {
      this.showDelete = true;
      GetAddressById(id).then(response => {
        //console.log(response);
        if (response.state == "success") {
          var receiveEntity = response.data;
          console.log(receiveEntity);
          this.info.name= receiveEntity.receiveUser;
          this.info.addressDetail= receiveEntity.retailsAddress; 
          this.info.tel=  receiveEntity.receiveTelephone;
          this.info.province= receiveEntity.receiveProvinceName;
          this.info.city = receiveEntity.receiveCityName; 
          this.info.county= receiveEntity.receiveAreaName;
          this.info.areaCode = receiveEntity.receiveAreaCode;
          this.info.isDefalut = receiveEntity.isDefalut == "Y" ? true : false;
          console.log(this.info);
        }
      });
    }
  }
};
</script>

<style>
.van-picker__toolbar {
  font-size: 16px;
}
</style>
