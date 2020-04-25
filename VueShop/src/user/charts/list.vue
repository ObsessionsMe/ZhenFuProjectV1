<template>
    <div>
        <headerNav title="龙虎榜"/>
          <div class="infinite-list" style="overflow:auto">
                <ul
                class="list"
                infinite-scroll-disabled="disabled">
                 <li v-for="item in userList" :key="item.id" class="infinite-list-item">
                    <span :style="{'background-color':(item.color?item.color:'#7dbcfc')}" 
                     class="c-index  c-index-hot1 c-gap-icon-right-small" style="margin-left: 3em">{{item.id}}</span>
                    <span class="opr-toplist1-cut" style="margin-left:1%">{{item.name}}</span>
                </li>
                </ul>
            </div>
        </div>
</template>

<script>
  export default {
    data () {
      return {
        count: 50,
        loading: false,
        userList:[
            {
              id:1,
              name:"张三",
              color:'#f54545'
            },
            {
              id:2,
              name:"李四",
              color:'#ff8547'
            },{
              id:3,
              name:"王二",
              color:'#ffac38'
            },
            {
              id:4,
              name:"小涨"
            },
            {
              id:5,
              name:"小无人"
            },{
              id:6,
              name:"北城"
            },{
              id:7,
              name:"jacks"
            },
            {
              id:8,
              name:"Massp"
            },{
              id:9,
              name:"SassPO"
            }
        ]
      }
    },
    computed: {
    },
    methods: {
      
    }
  }
</script>
<style>
.infinite-list {
    padding: 0;
    margin: 0;
    list-style: none
}

.infinite-list .infinite-list-item {
    display: flex;
    align-items: center;
    justify-content: left;
    height: 40px;
    margin: 10px;
    color: #7dbcfc;
    font-size:14px;
    border-bottom: 1px solid WHITESMOKE
}

.opr-toplist1-table .c-index {
    min-width: 14px;
    width: auto;
}
.c-index-hot, .c-index-hot1 {
    background-color: #f54545;
}
.c-index {
    display: inline-block;
    padding: 1px 0;
    color: #fff;
    width: 14px;
    line-height: 100%;
    font-size: 12px;
    text-align: center;
    background-color: #8eb9f5;
}
.c-gap-icon-right-small {
    margin-right: 5px;
}
.opr-toplist1-cut {
    white-space: nowrap;
    text-overflow: ellipsis;
    overflow: hidden;
    vertical-align: middle;
    display: inline-block;
}
</style>
