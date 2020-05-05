<template>
    <div>
      <headerNav title="修改密码"/>
      <div style="background:url() center 18px no-repeat;background-size:161px;">
       <div style="background:url() center 18px no-repeat;background-size:161px;">
          <div style="padding-top: 70px;">
            <van-cell-group>
                <van-field
                    placeholder="请输入手机号"
                    type="number"
                />
                <van-field
                    placeholder="请输入新密码"
                    type="password"
                />
                <van-field
                    type="password"
                    placeholder="请再次输入新密码"
                />
            </van-cell-group>
            <div style="margin: 10px;">
                <van-button size="large" type="primary" style="height: 45px;line-height:45px;">保存</van-button>
            </div>
          </div>
      </div>
    </div>
   </div> 
</template>

<script>
export default {

}
</script>

<style>

</style>
