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
import { SaveAddress} from "../../../api/user.js";
import { SubmitAddress, GetAddressById,DelAddress } from "../../../api/address.js";

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
      infos:[],
      AddressId:null,
      Id:0
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
      receiveEntity.AddressId = this.AddressId;
      receiveEntity.Id = parseInt(this.Id);
      console.log("receiveEntity", receiveEntity);
      SubmitAddress(receiveEntity).then(response => {
        this.$toast('收货地址保存成功');
        this.$router.go(-1);
      });
    },
    onDelete() {
      // eslint-disable-next-line no-unused-vars
      var id = this.$route.query.id;
      DelAddress(id).then(response=>{
        if (response.state == "success") {
          this.$toast('删除成功');
          this.$router.go(-1);
        }
      })
    }
  },
  mounted(){
    var id = this.$route.query.id;
    if (id > 0) {
      this.Id = id;
      this.showDelete = true;
      GetAddressById(id).then(response => {
        //console.log(response);
        if (response.state == "success") {
          var info={};
          var receiveEntity = response.data;
          this.AddressId = receiveEntity.addressId;
          console.log("receiveEntity",receiveEntity);
          info.name = receiveEntity.receiveUser;
          info.addressDetail= receiveEntity.detailsAddress; 
          info.tel =  receiveEntity.receiveTelephone;
          info.province = receiveEntity.receiveProvinceName;
          info.city = receiveEntity.receiveCityName; 
          info.county= receiveEntity.receiveAreaName;
          info.areaCode = receiveEntity.receiveAreaCode;
          info.isDefault = receiveEntity.isDefalut == "Y" ? true : false;
          this.info = info;
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
