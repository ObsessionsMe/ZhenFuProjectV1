<template>
    <div class="container">
        <section class="toolBar">
            <el-button-group>
                <el-button type="primary" icon="el-icon-refresh-right" @click="restore">还原</el-button>
                <el-button type="primary" icon="el-icon-close" @click="realDelFile">彻底删除</el-button>
                <el-button type="primary" icon="el-icon-delete" @click="delAll">清空回收站</el-button>
            </el-button-group>

        </section>
        <section class="content">
            <h5>回收站</h5>
            <br />
        </section>
    </div>
</template>

<script>
    //此处引入
    export default {
        data(){
            return{
                tableData: [],
                multipleSelection: [],
                isUsing: false,
                total: 0,
                pageIndex: 1,
                pageSize: 10,
                FileEntity:{},
                isSelectRow: false,
            }
        },
        created() {
            //页面初始化
        },
        methods:{}
    }
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
    .container {
        width: 100%;
        height: 100%;
        display: flex;
        flex-direction: column;
        overflow:auto;
    }

    .el-button-group {
        margin-right: 10px;
    }

    .toolBar {
        height: 60px;
        justify-content: flex-start;
        align-items: center;
        display: flex;
    }

    .content {
        text-align: left;
        flex: 1
    }

    .file-name-wrapper{
        display: flex;
        align-items: center;
    }
</style>
